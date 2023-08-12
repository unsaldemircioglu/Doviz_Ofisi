using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;



namespace Doviz_Ofisi
{
    public partial class DovizOfisi : Form
    {
        public void Eror()
        {
            if (txtKur.Text == "")
            {
                MessageBox.Show("Kur seçiniz dolar veya euro ya yanına gelen üç noktadan kur seçebilirsiniz");
            }
            if (txtMiktar.Text == "")
            {
                MessageBox.Show("Lütfen ne kadar alacağınızı seçin");
            }
        }
        public DovizOfisi()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                txtTutar.Enabled = false;
                txtKalan.Enabled = false;
                string bugun = "https://www.tcmb.gov.tr/kurlar/today.xml"; // XML
                var xmldosya = new XmlDocument();
                xmldosya.Load(bugun);

                string dolaralis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod = 'USD']/BanknoteBuying").InnerXml;
                lblDolarAlis.Text = dolaralis;

                string dolarSatis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
                lblDolarSatis.Text = dolarSatis;

                string euroalis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
                lblEuroAlis.Text = euroalis;

                string eurosatis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
                lblEuroSatis.Text = eurosatis;

            }
            catch
            {
                MessageBox.Show("Bir Sorunla karşılaştık :( Lütfen intarnet bağlantısı kontrol edin ,", "Döviz Ofisi", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnSatisYap_Click(object sender, EventArgs e)
        {
            try {
                double kur, miktar, tutar;
                kur = Convert.ToDouble(txtKur.Text);
                miktar = Convert.ToDouble(txtMiktar.Text);
                tutar = kur * miktar;
                txtTutar.Text = tutar.ToString();
                listboxNotDefteri.Items.Add("-----------------------------------------------------");
                listboxNotDefteri.Items.Add("Anlık Tarih: " + DateTime.Now.ToString());
                listboxNotDefteri.Items.Add("Anlık Kur: " + " " + kur + " " + "Miktar: " + " " + miktar + "  " + "Tutar: " + " " + tutar);
                listboxNotDefteri.Items.Add("Xml => https://www.tcmb.gov.tr/kurlar/today.xml");
                listboxNotDefteri.Items.Add("-----------------------------------------------------");
            }
            catch
            {
                MessageBox.Show("Özel Durum İşlendi ,","Döviz Ofisi",MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
            }
        }

        private void btnDolarAlis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblDolarAlis.Text;
        }

        private void btnEuroAlis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblDolarSatis.Text;
        }

        private void btnDolarSatis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblEuroAlis.Text;
        }

        private void btnEuroSatis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblEuroSatis.Text;
        }

        private void txtKur_TextChanged(object sender, EventArgs e)
        {
            txtKur.Text = txtKur.Text.Replace(".", ","); // noktayı virüglle değiştir anlamında
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                double kur = Convert.ToDouble(txtKur.Text);
                int miktar = Convert.ToInt32(txtMiktar.Text);
                int tutar = Convert.ToInt32(miktar / kur);
                txtKalan.Text = tutar.ToString();
                int kalan;
                kalan = miktar % tutar;
                txtKalan.Text = kalan.ToString();
            }
            catch
            {
                MessageBox.Show("Özel Durum İşlendi ,", "Döviz Ofisi", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                
            }
        }

        private void txtMiktar_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listboxNotDefteri.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

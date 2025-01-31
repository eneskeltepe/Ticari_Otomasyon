using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.DataProcessing.InMemoryDataProcessor;

namespace Ticari_Otomasyon
{
    public partial class btnYenile : Form
    {
        public btnYenile()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void stoklar()
        {
            // Azalan Stoklar kısmına 20'den az stoklu ürünleri yazdırıyoruz.
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select URUNAD, SUM(ADET) as 'Adet' from TBL_URUNLER group by URUNAD having Sum(Adet)<=20 order by sum(Adet)", bgl.baglanti());
            da.Fill(dt);
            gridControlStoklar.DataSource = dt;
        }

        void ajanda()
        {
            // Ajanda kısmına son 12 toplantıyı yazdırıyoruz.
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select top 12 TARIH,SAAT,BASLIK,HITAP,DETAY from TBL_NOTLAR order by ID desc", bgl.baglanti());
            da.Fill(dt);
            gridControlAjanda.DataSource = dt;
        }

        void firmahareketleri()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute FirmaHareketleri2", bgl.baglanti());
            da.Fill(dt);
            gridControlFirmaHareket.DataSource = dt;
        }

        void musterifihrist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select (AD + ' ' + SOYAD) as 'AD SOYAD',TELEFON,MAIL from TBL_MUSTERILER", bgl.baglanti());
            da.Fill(dt);
            gridControlFihrist.DataSource = dt;
        }

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();
            ajanda();
            firmahareketleri();
            musterifihrist();
            webBrowserDovizKurları.Navigate("https://tcmb.gov.tr/kurlar/today.xml");
        }

        private void Yenile_Click(object sender, EventArgs e)
        {
            stoklar();
            ajanda();
            firmahareketleri();
            musterifihrist();
        }
    }
}

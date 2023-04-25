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
using DevExpress.Charts;

namespace Ticari_Otomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void musterihareketlistele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute MusteriHareketleri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void firmahareketlistele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute FirmaHareketleri", bgl.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }

        void giderlistele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_GIDERLER", bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        void kasagirishareketleri()
        {
            // Toplam tutarı hesaplama
            SqlCommand komut1 = new SqlCommand("Select Sum(Tutar) From TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblToplamTutar.Text = dr1[0].ToString() + " TL";
            }
            bgl.baglanti().Close();
            SqlConnection.ClearPool(bgl.baglanti());

            // Son ayın faturaları
            SqlCommand komut2 = new SqlCommand("Select (ELEKTRIK+SU+DOGALGAZ+INTERNET+MAASLAR) from TBL_GIDERLER", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblOdemeler.Text = dr2[0].ToString() + " TL";
            }
            bgl.baglanti().Close();
            SqlConnection.ClearPool(bgl.baglanti());

            // Son ayın personel maaşları
            SqlCommand komut3 = new SqlCommand("Select MAASLAR from TBL_GIDERLER", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblPersonelMaaslari.Text = dr3[0].ToString() + " TL";
            }
            bgl.baglanti().Close();
            SqlConnection.ClearPool(bgl.baglanti());

            // Müşteri sayısı
            SqlCommand komut4 = new SqlCommand("Select Count(*) from TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblMusteriSayisi.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();
            SqlConnection.ClearPool(bgl.baglanti());

            // Firma sayısı
            SqlCommand komut5 = new SqlCommand("Select Count(*) from TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblFirmaSayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();
            SqlConnection.ClearPool(bgl.baglanti());

            // Firma - Şehir sayısı
            SqlCommand komut6 = new SqlCommand("Select Count(Distinct(IL)) from TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblFirmaSehirSayisi.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();
            SqlConnection.ClearPool(bgl.baglanti());

            // Müşteri - Şehir sayısı
            SqlCommand komut7 = new SqlCommand("Select Count(Distinct(IL)) from TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                lblMusteriSehirSayisi.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();
            SqlConnection.ClearPool(bgl.baglanti());

            // Personel sayısı
            SqlCommand komut8 = new SqlCommand("Select Count(*) from TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                lblPersonelSayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();
            SqlConnection.ClearPool(bgl.baglanti());

            // Toplam ürün sayısı
            SqlCommand komut9 = new SqlCommand("Select Sum(ADET) from TBL_URUNLER", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                lblStokSayisi.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();
            SqlConnection.ClearPool(bgl.baglanti());

            // Aktif Kullanıcı
            // Ana modulu köprü olarak kullandık
            lblAktifKullanici.Text = ad;
        }

        public string ad;

        private void FrmKasa_Load(object sender, EventArgs e)
        {
            musterihareketlistele();
            firmahareketlistele();
            giderlistele();
            kasagirishareketleri();        }

        int sayac;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;

            // 1.Chart controle son 4 ayın elektrik faturasını listeleme
            if (sayac >= 0 && sayac <= 5)
            {
                groupControl10.Text = "Son 4 ayın elektrik faturası";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select Top 4 AY,ELEKTRIK from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            // 1.Chart controle son 4 ayın su faturasını listeleme
            if (sayac > 5 && sayac <= 10)
            {
                groupControl10.Text = "Son 4 ayın su faturası";
                chartControl1.Series["Aylar"].Points.Clear();
                
                SqlCommand komut10 = new SqlCommand("Select Top 4 AY,SU from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            // 1.Chart controle son 4 ayın doğalgaz faturasını listeleme
            if (sayac > 10 && sayac <= 15)
            {
                groupControl10.Text = "Son 4 ayın doğalgaz faturası";
                chartControl1.Series["Aylar"].Points.Clear();

                SqlCommand komut10 = new SqlCommand("Select Top 4 AY,DOGALGAZ from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            // 1.Chart controle son 4 ayın doğalgaz faturasını listeleme
            if (sayac > 15 && sayac <= 20)
            {
                groupControl10.Text = "Son 4 ayın internet faturası";
                chartControl1.Series["Aylar"].Points.Clear();

                SqlCommand komut10 = new SqlCommand("Select Top 4 AY,INTERNET from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            // 1.Chart controle son 4 ayın ekstra giderlerini listeleme
            if (sayac > 20 && sayac <= 25)
            {
                groupControl10.Text = "Son 4 ayın ekstra giderleri";
                chartControl1.Series["Aylar"].Points.Clear();

                SqlCommand komut10 = new SqlCommand("Select Top 4 AY,EKSTRA from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
                SqlConnection.ClearPool(bgl.baglanti());
            }

            // Sayaç 26 olduğunda sayacı sıfırlayıp döngüye alan kod
            if(sayac == 26)
            {
                sayac = 0;
            }
        }

        int sayac2;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;

            // 2.Chart controle son 4 ayın elektrik faturasını listeleme
            if (sayac2 >= 0 && sayac2 <= 5)
            {

                groupControl11.Text = "Son 4 ayın elektrik faturası";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,ELEKTRIK from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            // 2.Chart controle son 4 ayın su faturasını listeleme
            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl11.Text = "Son 4 ayın su faturası";
                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,SU from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            // 2.Chart controle son 4 ayın doğalgaz faturasını listeleme
            if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl11.Text = "Son 4 ayın doğalgaz faturası";
                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,DOGALGAZ from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            // 2.Chart controle son 4 ayın doğalgaz faturasını listeleme
            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl11.Text = "Son 4 ayın internet faturası";
                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,INTERNET from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            // 2.Chart controle son 4 ayın ekstra giderlerini listeleme
            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl11.Text = "Son 4 ayın ekstra giderleri";
                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,EKSTRA from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
                SqlConnection.ClearPool(bgl.baglanti());
            }

            // Sayaç 26 olduğunda sayacı sıfırlayıp döngüye alan kod
            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }

    }
}

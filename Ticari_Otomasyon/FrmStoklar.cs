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

namespace Ticari_Otomasyon
{
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void StokListesi()
        {
            // Ürün - Miktar listele 
            SqlDataAdapter da = new SqlDataAdapter("Select URUNAD as 'ÜRÜN ADI',Sum(ADET) as 'MİKTAR' from TBL_URUNLER group by URUNAD", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            // Firma - Şehir listele
            SqlDataAdapter da2 = new SqlDataAdapter("Select IL,Count(*) as 'SAYI' from TBL_FIRMALAR group by IL", bgl.baglanti());
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;

            // Charta stok miktarını listeleme
            SqlCommand komut = new SqlCommand("Select URUNAD as 'ÜRÜN ADI',Sum(ADET) as 'MİKTAR' from TBL_URUNLER group by URUNAD", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }
            bgl.baglanti().Close();

            // Charta firma şehir sayısını çekme
            SqlCommand komut2 = new SqlCommand("Select IL,Count(*) from TBL_FIRMALAR group by IL", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
            }
            bgl.baglanti().Close();
        }

        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            StokListesi();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay fr = new FrmStokDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.ad = dr["ÜRÜN ADI"].ToString();
            }
            fr.Show();
        }
    }
}

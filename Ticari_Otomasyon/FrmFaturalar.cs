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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FATURABILGI Order By FATURABILGIID asc", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtid.Text = "";
            txtSeri.Text = "";
            txtSıraNo.Text = "";
            mskTarih.Text = "";
            mskSaat.Text = "";
            txtVergiDaire.Text = "";
            txtAlici.Text = "";
            txtTeslimEden.Text = "";
            txtTeslimAlan.Text = "";
            txtUrunid.Text = "";
            txtUrunAd.Text = "";
            txtMiktar.Text = "";
            txtFiyat.Text = "";
            txtTutar.Text = "";
            txtPersonel.Text = "";
            txtFirma.Text = "";
            txtFaturaid.Text = "";
            cmbCariTuru.Text = " ";
        }

        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            if (txtFaturaid.Text == "")
            {
                SqlCommand komut = new SqlCommand("insert into TBL_FATURABILGI (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtSeri.Text);
                komut.Parameters.AddWithValue("@P2", txtSıraNo.Text);
                komut.Parameters.AddWithValue("@P3", mskTarih.Text);
                komut.Parameters.AddWithValue("@P4", mskSaat.Text);
                komut.Parameters.AddWithValue("@P5", txtVergiDaire.Text);
                komut.Parameters.AddWithValue("@P6", txtAlici.Text);
                komut.Parameters.AddWithValue("@P7", txtTeslimEden.Text);
                komut.Parameters.AddWithValue("@P8", txtTeslimAlan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura bilgileri sisteme başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }

            // Firma carisi
            if (txtFaturaid.Text != "" && cmbCariTuru.Text == "Firma")
            {
                double miktar, fiyat, tutar;
                fiyat = Convert.ToDouble(txtFiyat.Text);
                miktar = Convert.ToDouble(txtMiktar.Text);
                tutar = fiyat * miktar;
                txtTutar.Text = tutar.ToString();

                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@P1,@P2,@P3,@P4,@P5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@P1", txtUrunAd.Text);
                komut2.Parameters.AddWithValue("@P2", txtMiktar.Text);
                komut2.Parameters.AddWithValue("@P3", decimal.Parse(txtFiyat.Text));
                komut2.Parameters.AddWithValue("@P4", decimal.Parse(txtTutar.Text));
                komut2.Parameters.AddWithValue("@P5", txtFaturaid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                // Hareket tablosuna veri girişi
                SqlCommand komut3 = new SqlCommand("insert into TBL_FIRMAHAREKETLER (URUNID,ADET,PERSONEL,FIRMA,FIYAT,TOPLAM,FATURAID,TARIH) values(@H1,@H2,@H3,@H4,@H5,@H6,@H7,@H8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@H1", txtUrunid.Text);
                komut3.Parameters.AddWithValue("@H2", txtMiktar.Text);
                komut3.Parameters.AddWithValue("@H3", txtPersonel.Text);
                komut3.Parameters.AddWithValue("@H4", txtFirma.Text);
                komut3.Parameters.AddWithValue("@H5", decimal.Parse(txtFiyat.Text));
                komut3.Parameters.AddWithValue("@H6", decimal.Parse(txtTutar.Text));
                komut3.Parameters.AddWithValue("@H7", txtFaturaid.Text);
                komut3.Parameters.AddWithValue("@H8", mskTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                // Stoktan düşme
                SqlCommand komut4 = new SqlCommand("update TBL_URUNLER set ADET=ADET-@U1 where ID=@U2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@U1", txtMiktar.Text);
                komut4.Parameters.AddWithValue("@U2", txtUrunid.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Faturaya ait ürün sisteme başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }

            // Müşteri carisi
            if (txtFaturaid.Text != "" && cmbCariTuru.Text == "Müşteri")
            {
                double miktar, fiyat, tutar;
                fiyat = Convert.ToDouble(txtFiyat.Text);
                miktar = Convert.ToDouble(txtMiktar.Text);
                tutar = fiyat * miktar;
                txtTutar.Text = tutar.ToString();

                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@P1,@P2,@P3,@P4,@P5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@P1", txtUrunAd.Text);
                komut2.Parameters.AddWithValue("@P2", txtMiktar.Text);
                komut2.Parameters.AddWithValue("@P3", decimal.Parse(txtFiyat.Text));
                komut2.Parameters.AddWithValue("@P4", decimal.Parse(txtTutar.Text));
                komut2.Parameters.AddWithValue("@P5", txtFaturaid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                // Hareket tablosuna veri girişi
                SqlCommand komut3 = new SqlCommand("insert into TBL_MUSTERIHAREKETLER (URUNID,ADET,PERSONEL,MUSTERI,FIYAT,TOPLAM,FATURAID,TARIH) values(@H1,@H2,@H3,@H4,@H5,@H6,@H7,@H8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@H1", txtUrunid.Text);
                komut3.Parameters.AddWithValue("@H2", txtMiktar.Text);
                komut3.Parameters.AddWithValue("@H3", txtPersonel.Text);
                komut3.Parameters.AddWithValue("@H4", txtFirma.Text);
                komut3.Parameters.AddWithValue("@H5", decimal.Parse(txtFiyat.Text));
                komut3.Parameters.AddWithValue("@H6", decimal.Parse(txtTutar.Text));
                komut3.Parameters.AddWithValue("@H7", txtFaturaid.Text);
                komut3.Parameters.AddWithValue("@H8", mskTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                // Stoktan düşme
                SqlCommand komut4 = new SqlCommand("update TBL_URUNLER set ADET=ADET-@U1 where ID=@U2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@U1", txtMiktar.Text);
                komut4.Parameters.AddWithValue("@U2", txtUrunid.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Faturaya ait ürün sisteme başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["FATURABILGIID"].ToString();
                txtSeri.Text = dr["SERI"].ToString();
                txtSıraNo.Text = dr["SIRANO"].ToString();
                mskTarih.Text = dr["TARIH"].ToString();
                mskSaat.Text = dr["SAAT"].ToString();
                txtVergiDaire.Text = dr["VERGIDAIRE"].ToString();
                txtAlici.Text = dr["ALICI"].ToString();
                txtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
                txtTeslimAlan.Text = dr["TESLIMALAN"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Fatura bilgisi sistemden silinecektir.Gerçekten silmek istiyor musunuz?", "Fatura Bilgisi Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from TBL_FATURABILGI where FATURABILGIID=@P1", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura bilgisi başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Fatura bilgisi silme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Fatura bilgisi güncellenecektir.Gerçekten güncellemek istiyor musunuz?", "Fatura Bilgisi Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("update TBL_FATURABILGI set SERI=@P1,SIRANO=@P2,TARIH=@P3,SAAT=@P4,VERGIDAIRE=@P5,ALICI=@P6,TESLIMEDEN=@P7,TESLIMALAN=@P8 where FATURABILGIID=@P9 ", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtSeri.Text);
                komut.Parameters.AddWithValue("@P2", txtSıraNo.Text);
                komut.Parameters.AddWithValue("@P3", mskTarih.Text);
                komut.Parameters.AddWithValue("@P4", mskSaat.Text);
                komut.Parameters.AddWithValue("@P5", txtVergiDaire.Text);
                komut.Parameters.AddWithValue("@P6", txtAlici.Text);
                komut.Parameters.AddWithValue("@P7", txtTeslimEden.Text);
                komut.Parameters.AddWithValue("@P8", txtTeslimAlan.Text);
                komut.Parameters.AddWithValue("@P9", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura bilgisi başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Fatura bilgisi güncelleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay fr = new FrmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select URUNAD,SATISFIYAT from TBL_URUNLER where ID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtUrunid.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtUrunAd.Text = dr[0].ToString();
                txtFiyat.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}

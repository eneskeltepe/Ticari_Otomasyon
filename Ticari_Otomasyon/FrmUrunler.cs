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
using System.Security.Cryptography.X509Certificates;

namespace Ticari_Otomasyon
{
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_URUNLER Order By ID asc", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtid.Text = "";
            txtAd.Text = "";
            txtMarka.Text = "";
            txtModel.Text = "";
            mskYil.Text = "";
            nudAdet.Value = 0;
            txtAlisFiyat.Text = "";
            txtSatisFiyat.Text = "";
            rchDetay.Text = "";
        }
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Verileri ekle
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Ürün kaydı sisteme eklenecektir.Gerçekten eklemek istiyor musunuz?", "Ürün Kaydı Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komutekle = new SqlCommand("insert into TBL_URUNLER (URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", bgl.baglanti());
                komutekle.Parameters.AddWithValue("@P1", txtAd.Text);
                komutekle.Parameters.AddWithValue("@P2", txtMarka.Text);
                komutekle.Parameters.AddWithValue("@P3", txtModel.Text);
                komutekle.Parameters.AddWithValue("@P4", mskYil.Text);
                komutekle.Parameters.AddWithValue("@P5", int.Parse((nudAdet.Value).ToString()));
                komutekle.Parameters.AddWithValue("@P6", decimal.Parse(txtAlisFiyat.Text));
                komutekle.Parameters.AddWithValue("@P7", decimal.Parse(txtSatisFiyat.Text));
                komutekle.Parameters.AddWithValue("@P8", rchDetay.Text);
                komutekle.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Ürün kaydı sisteme başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Ürün kaydı ekleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void mskYil_Click(object sender, EventArgs e)
        {
            mskYil.SelectionStart = 0;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // Verileri sil
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Ürün kaydı sistemden silinecektir.Gerçekten silmek istiyor musunuz?", "Ürün Kaydı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komutsil = new SqlCommand("Delete from TBL_URUNLER where ID=@P1", bgl.baglanti());
                komutsil.Parameters.AddWithValue("@P1", txtid.Text);
                komutsil.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Ürün sistemden başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Ürün kaydı silme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = dr["ID"].ToString();
            txtAd.Text = dr["URUNAD"].ToString();
            txtMarka.Text = dr["MARKA"].ToString();
            txtModel.Text = dr["MODEL"].ToString();
            mskYil.Text = dr["YIL"].ToString();
            nudAdet.Value = decimal.Parse(dr["ADET"].ToString());
            txtAlisFiyat.Text = dr["ALISFIYAT"].ToString();
            txtSatisFiyat.Text = dr["SATISFIYAT"].ToString();
            rchDetay.Text = dr["DETAY"].ToString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Ürün kaydı güncellenecektir.Gerçekten güncellemek istiyor musunuz?", "Ürün Kaydı Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komutguncelle = new SqlCommand("Update TBL_URUNLER set URUNAD=@P1,MARKA=@P2,MODEL=@P3,YIL=@P4,ADET=@P5,ALISFIYAT=@P6,SATISFIYAT=@P7,DETAY=@P8 where ID=@P9", bgl.baglanti());
                komutguncelle.Parameters.AddWithValue("@P1", txtAd.Text);
                komutguncelle.Parameters.AddWithValue("@P2", txtMarka.Text);
                komutguncelle.Parameters.AddWithValue("@P3", txtModel.Text);
                komutguncelle.Parameters.AddWithValue("@P4", mskYil.Text);
                komutguncelle.Parameters.AddWithValue("@P5", int.Parse((nudAdet.Value).ToString()));
                komutguncelle.Parameters.AddWithValue("@P6", decimal.Parse(txtAlisFiyat.Text));
                komutguncelle.Parameters.AddWithValue("@P7", decimal.Parse(txtSatisFiyat.Text));
                komutguncelle.Parameters.AddWithValue("@P8", rchDetay.Text);
                komutguncelle.Parameters.AddWithValue("@P9", txtid.Text);
                komutguncelle.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Ürün bilgisi başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Ürün kaydı güncelleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}

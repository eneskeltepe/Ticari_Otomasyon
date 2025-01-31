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
    public partial class FrmPersoneller : Form
    {
        public FrmPersoneller()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void personellistele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_PERSONELLER Order By ID asc", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtid.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            mskTelefon.Text = "";
            mskTC.Text = "";
            txtMail.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            txtGorev.Text = "";
            rchAdres.Text = "";
        }

        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR from TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        private void FrmPersoneller_Load(object sender, EventArgs e)
        {
            personellistele();
            temizle();
            sehirlistesi();
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIlce.Text = "";
            cmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", cmbIl.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Personel kaydı sisteme eklenecektir.Gerçekten eklemek istiyor musunuz?", "Personel Kaydı Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("insert into TBL_PERSONELLER (AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtAd.Text);
                komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@P3", mskTelefon.Text);
                komut.Parameters.AddWithValue("@P4", mskTC.Text);
                komut.Parameters.AddWithValue("@P5", txtMail.Text);
                komut.Parameters.AddWithValue("@P6", cmbIl.Text);
                komut.Parameters.AddWithValue("@P7", cmbIlce.Text);
                komut.Parameters.AddWithValue("@P8", rchAdres.Text);
                komut.Parameters.AddWithValue("@P9", txtGorev.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Personel kaydı sisteme başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                personellistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Personel kaydı ekleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                txtAd.Text = dr["AD"].ToString();
                txtSoyad.Text = dr["SOYAD"].ToString();
                mskTelefon.Text = dr["TELEFON"].ToString();
                mskTC.Text = dr["TC"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                rchAdres.Text = dr["ADRES"].ToString();
                txtGorev.Text = dr["GOREV"].ToString();
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Personel kaydı sistemden silinecektir.Gerçekten silmek istiyor musunuz?", "Personel Kaydı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komutsil = new SqlCommand("Delete from TBL_PERSONELLER where ID=@P1", bgl.baglanti());
                komutsil.Parameters.AddWithValue("@P1", txtid.Text);
                komutsil.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Personel silme işlemi başarıyla gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                personellistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Personel kaydı silme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Personel kaydı güncellenecektir.Gerçekten güncellemek istiyor musunuz?", "Personel Kaydı Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komutguncelle = new SqlCommand("Update TBL_PERSONELLER set AD=@P1,SOYAD=@P2,TELEFON=@P3,TC=@P4,MAIL=@P5,IL=@P6,ILCE=@P7,ADRES=@P8,GOREV=@P9 where ID=@P10", bgl.baglanti());
                komutguncelle.Parameters.AddWithValue("@P1", txtAd.Text);
                komutguncelle.Parameters.AddWithValue("@P2", txtSoyad.Text);
                komutguncelle.Parameters.AddWithValue("@P3", mskTelefon.Text);
                komutguncelle.Parameters.AddWithValue("@P4", mskTC.Text);
                komutguncelle.Parameters.AddWithValue("@P5", txtMail.Text);
                komutguncelle.Parameters.AddWithValue("@P6", cmbIl.Text);
                komutguncelle.Parameters.AddWithValue("@P7", cmbIlce.Text);
                komutguncelle.Parameters.AddWithValue("@P8", rchAdres.Text);
                komutguncelle.Parameters.AddWithValue("@P9", txtGorev.Text);
                komutguncelle.Parameters.AddWithValue("@P10", txtid.Text);
                komutguncelle.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Personel kaydı başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                personellistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Personel güncelleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
    }
}

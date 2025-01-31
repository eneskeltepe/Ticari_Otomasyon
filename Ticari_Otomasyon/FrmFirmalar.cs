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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void firmalistele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FIRMALAR Order By ID asc", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            txtid.Text = "";
            txtAd.Text = "";
            txtKod1.Text = "";
            txtKod2.Text = "";
            txtKod3.Text = "";
            txtMail.Text = "";
            txtSektör.Text = "";
            txtVergiDairesi.Text = "";
            txtYetkili.Text = "";
            txtYetkiliGorev.Text = "";
            mskFax.Text = "";
            mskTelefon1.Text = "";
            mskTelefon2.Text = "";
            mskTelefon3.Text = "";
            mskYetkiliTC.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            rchAdres.Text = "";
            txtKod1.Text = "";
            txtKod2.Text = "";
            txtKod3.Text = "";
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

        void carikodaciklamalar()
        {
            SqlCommand komut = new SqlCommand("Select FIRMAKOD1 from TBL_KODLAR", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                rchKod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistele();
            sehirlistesi();
            temizle();
            carikodaciklamalar();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                txtAd.Text = dr["AD"].ToString();
                txtYetkiliGorev.Text = dr["YETKILISTATU"].ToString();
                txtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                mskYetkiliTC.Text = dr["YETKILITC"].ToString();
                txtSektör.Text = dr["SEKTOR"].ToString();
                mskTelefon1.Text = dr["TELEFON1"].ToString();
                mskTelefon2.Text = dr["TELEFON2"].ToString();
                mskTelefon3.Text = dr["TELEFON3"].ToString();
                mskFax.Text = dr["FAX"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                txtVergiDairesi.Text = dr["VERGIDAIRE"].ToString();
                rchAdres.Text = dr["ADRES"].ToString();
                txtKod1.Text = dr["OZELKOD1"].ToString();
                txtKod2.Text = dr["OZELKOD2"].ToString();
                txtKod3.Text = dr["OZELKOD3"].ToString();
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Firma kaydı sisteme eklenecektir.Gerçekten eklemek istiyor musunuz?", "Firma Kaydı Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("insert into TBL_FIRMALAR (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,FAX,MAIL,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtAd.Text);
                komut.Parameters.AddWithValue("@P2", txtYetkiliGorev.Text);
                komut.Parameters.AddWithValue("@P3", txtYetkili.Text);
                komut.Parameters.AddWithValue("@P4", mskYetkiliTC.Text);
                komut.Parameters.AddWithValue("@P5", txtSektör.Text);
                komut.Parameters.AddWithValue("@P6", mskTelefon1.Text);
                komut.Parameters.AddWithValue("@P7", mskTelefon2.Text);
                komut.Parameters.AddWithValue("@P8", mskTelefon3.Text);
                komut.Parameters.AddWithValue("@P9", mskFax.Text);
                komut.Parameters.AddWithValue("@P10", txtMail.Text);
                komut.Parameters.AddWithValue("@P11", cmbIl.Text);
                komut.Parameters.AddWithValue("@P12", cmbIlce.Text);
                komut.Parameters.AddWithValue("@P13", txtVergiDairesi.Text);
                komut.Parameters.AddWithValue("@P14", rchAdres.Text);
                komut.Parameters.AddWithValue("@P15", txtKod1.Text);
                komut.Parameters.AddWithValue("@P16", txtKod2.Text);
                komut.Parameters.AddWithValue("@P17", txtKod3.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Firma kaydı sisteme başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                firmalistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Firma kaydı ekleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
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

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Firma kaydı sistemden silinecektir.Gerçekten silmek istiyor musunuz?", "Firma Kaydı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from TBL_FIRMALAR where ID=@P1", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Firma kaydı sistemden başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                firmalistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Ürün kaydı silme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Firma kaydı güncellenecektir.Gerçekten güncellemek istiyor musunuz?", "Firma Kaydı Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Update TBL_FIRMALAR set AD=@P1,YETKILISTATU=@P2,YETKILIADSOYAD=@P3,YETKILITC=@P4,SEKTOR=@P5,TELEFON1=@P6,TELEFON2=@P7,TELEFON3=@P8,FAX=@P9,MAIL=@P10,IL=@P11,ILCE=@P12,VERGIDAIRE=@P13,ADRES=@P14,OZELKOD1=@P15,OZELKOD2=@P16,OZELKOD3=@P17 where ID=@P18", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtAd.Text);
                komut.Parameters.AddWithValue("@P2", txtYetkiliGorev.Text);
                komut.Parameters.AddWithValue("@P3", txtYetkili.Text);
                komut.Parameters.AddWithValue("@P4", mskYetkiliTC.Text);
                komut.Parameters.AddWithValue("@P5", txtSektör.Text);
                komut.Parameters.AddWithValue("@P6", mskTelefon1.Text);
                komut.Parameters.AddWithValue("@P7", mskTelefon2.Text);
                komut.Parameters.AddWithValue("@P8", mskTelefon3.Text);
                komut.Parameters.AddWithValue("@P9", mskFax.Text);
                komut.Parameters.AddWithValue("@P10", txtMail.Text);
                komut.Parameters.AddWithValue("@P11", cmbIl.Text);
                komut.Parameters.AddWithValue("@P12", cmbIlce.Text);
                komut.Parameters.AddWithValue("@P13", txtVergiDairesi.Text);
                komut.Parameters.AddWithValue("@P14", rchAdres.Text);
                komut.Parameters.AddWithValue("@P15", txtKod1.Text);
                komut.Parameters.AddWithValue("@P16", txtKod2.Text);
                komut.Parameters.AddWithValue("@P17", txtKod3.Text);
                komut.Parameters.AddWithValue("@P18", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Firma kaydı başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                firmalistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Firma güncelleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

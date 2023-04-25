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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_ADMIN", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtid.Text = "";
            txtKullaniciAdi.Text = "";
            txtSifre.Text = "";
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                txtKullaniciAdi.Text = dr["KULLANICIADI"].ToString();
                txtSifre.Text = dr["SIFRE"].ToString();
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Admin bilgisi sisteme eklenecektir.Gerçekten eklemek istiyor musunuz?", "Admin Bilgisi Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("insert into TBL_ADMIN values (@P1,@P2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtKullaniciAdi.Text);
                komut.Parameters.AddWithValue("@P2", txtSifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Admin bilgisi sisteme başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Admin bilgisi ekleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Admin bilgisi sistemden silinecektir.Gerçekten silmek istiyor musunuz?", "Admin Bilgisi Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from TBL_ADMIN where KULLANICIADI=@P1", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtKullaniciAdi.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Admin bilgisi sistemden başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Admin bilgisi silme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Admin bilgisi güncellenecektir.Gerçekten güncellemek istiyor musunuz?", "Admin Bilgisi Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Update TBL_ADMIN set KULLANICIADI=@P1,SIFRE=@P2 where ID=@P3", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtKullaniciAdi.Text);
                komut.Parameters.AddWithValue("@P2", txtSifre.Text);
                komut.Parameters.AddWithValue("@P3", txtid.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Admin bilgisi başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Admin bilgisi güncelleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}

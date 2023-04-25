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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void notlistele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_NOTLAR Order By ID asc", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtid.Text = "";
            mskTarih.Text = "";
            mskSaat.Text = "";
            txtBaslik.Text = "";
            txtOlusturan.Text = "";
            txtHitap.Text = "";
            rchDetay.Text = "";
        }

        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            notlistele();
            temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Not kaydı sisteme eklenecektir.Gerçekten eklemek istiyor musunuz?", "Not Kaydı Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("insert into TBL_NOTLAR (TARIH,SAAT,BASLIK,OLUSTURAN,HITAP,DETAY) values (@P1,@P2,@P3,@P4,@P5,@P6)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", mskTarih.Text);
                komut.Parameters.AddWithValue("@P2", mskSaat.Text);
                komut.Parameters.AddWithValue("@P3", txtBaslik.Text);
                komut.Parameters.AddWithValue("@P4", txtOlusturan.Text);
                komut.Parameters.AddWithValue("@P5", txtHitap.Text);
                komut.Parameters.AddWithValue("@P6", rchDetay.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Not kaydı sisteme başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                notlistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Not kaydı ekleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                mskTarih.Text = dr["TARIH"].ToString();
                mskSaat.Text = dr["SAAT"].ToString();
                txtBaslik.Text = dr["BASLIK"].ToString();
                txtOlusturan.Text = dr["OLUSTURAN"].ToString();
                txtHitap.Text = dr["HITAP"].ToString();
                rchDetay.Text = dr["DETAY"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Not kaydı sistemden silinecektir.Gerçekten silmek istiyor musunuz?", "Not Kaydı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("delete from TBL_NOTLAR where ID=@P1", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Not kaydı sistemden silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                notlistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Not kaydı silme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Not kaydı güncellenecektir.Gerçekten güncellemek istiyor musunuz?", "Not Kaydı Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Update TBL_NOTLAR set TARIH=@P1,SAAT=@P2,BASLIK=@P3,OLUSTURAN=@P4,HITAP=@P5,DETAY=@P6 where ID=@P7", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", mskTarih.Text);
                komut.Parameters.AddWithValue("@P2", mskSaat.Text);
                komut.Parameters.AddWithValue("@P3", txtBaslik.Text);
                komut.Parameters.AddWithValue("@P4", txtOlusturan.Text);
                komut.Parameters.AddWithValue("@P5", txtHitap.Text);
                komut.Parameters.AddWithValue("@P6", rchDetay.Text);
                komut.Parameters.AddWithValue("@P7", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Not kaydı başarıyla güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                notlistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Not kaydı güncelleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay fr = new FrmNotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.metin = dr["DETAY"].ToString();
                string ID = dr["ID"].ToString();
                SqlCommand cmd = new SqlCommand("Update TBL_NOTLAR set DURUM=@DURUM Where ID=@ID", bgl.baglanti());
                cmd.Parameters.AddWithValue("@ID", txtid.Text);
                cmd.Parameters.AddWithValue("@DURUM", true);
                cmd.ExecuteNonQuery();
                bgl.baglanti().Close();
                fr.Show();
                notlistele();
            }
        }
    }
}

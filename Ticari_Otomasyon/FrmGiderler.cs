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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void giderlistele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_GIDERLER Order By ID asc", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            txtid.Text = "";
            cmbAy.Text = "";
            cmbYil.Text = "";
            txtElektrik.Text = "";
            txtSu.Text = "";
            txtDogalgaz.Text = "";
            txtInternet.Text = "";
            txtMaaslar.Text = "";
            txtEkstra.Text = "";
            rchNotlar.Text = "";
        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            giderlistele();
            temizle();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Gider bilgisi sisteme eklenecektir.Gerçekten eklemek istiyor musunuz?", "Gider Bilgisi Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("insert into TBL_GIDERLER (AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", cmbAy.Text);
                komut.Parameters.AddWithValue("@P2", cmbYil.Text);
                komut.Parameters.AddWithValue("@P3", decimal.Parse(txtElektrik.Text));
                komut.Parameters.AddWithValue("@P4", decimal.Parse(txtSu.Text));
                komut.Parameters.AddWithValue("@P5", decimal.Parse(txtDogalgaz.Text));
                komut.Parameters.AddWithValue("@P6", decimal.Parse(txtInternet.Text));
                komut.Parameters.AddWithValue("@P7", decimal.Parse(txtMaaslar.Text));
                komut.Parameters.AddWithValue("@P8", decimal.Parse(txtEkstra.Text));
                komut.Parameters.AddWithValue("@P9", rchNotlar.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Gider bilgisi ekleme işlemi başarıyla gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                giderlistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Gider bilgisi ekleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                cmbAy.Text = dr["AY"].ToString();
                cmbYil.Text = dr["YIL"].ToString();
                txtElektrik.Text = dr["ELEKTRIK"].ToString();
                txtSu.Text = dr["SU"].ToString();
                txtDogalgaz.Text = dr["DOGALGAZ"].ToString();
                txtInternet.Text = dr["INTERNET"].ToString();
                txtMaaslar.Text = dr["MAASLAR"].ToString();
                txtEkstra.Text = dr["EKSTRA"].ToString();
                rchNotlar.Text = dr["NOTLAR"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Gider bilgisi sistemden silinecektir.Gerçekten silmek istiyor musunuz?", "Gider Bilgisi Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komutsil = new SqlCommand("Delete from TBL_GIDERLER where ID=@P1", bgl.baglanti());
                komutsil.Parameters.AddWithValue("@P1", txtid.Text);
                komutsil.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Gider bilgisi silme işlemi başarıyla gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                giderlistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Gider bilgisi silme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Gider bilgisi güncellenecektir.Gerçekten güncellemek istiyor musunuz?", "Gider Bilgisi Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Update TBL_GIDERLER set AY=@P1,YIL=@P2,ELEKTRIK=@P3,SU=@P4,DOGALGAZ=@P5,INTERNET=@P6,MAASLAR=@P7,EKSTRA=@P8,NOTLAR=@P9 where ID=@P10", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", cmbAy.Text);
                komut.Parameters.AddWithValue("@P2", cmbYil.Text);
                komut.Parameters.AddWithValue("@P3", decimal.Parse(txtElektrik.Text));
                komut.Parameters.AddWithValue("@P4", decimal.Parse(txtSu.Text));
                komut.Parameters.AddWithValue("@P5", decimal.Parse(txtDogalgaz.Text));
                komut.Parameters.AddWithValue("@P6", decimal.Parse(txtInternet.Text));
                komut.Parameters.AddWithValue("@P7", decimal.Parse(txtMaaslar.Text));
                komut.Parameters.AddWithValue("@P8", decimal.Parse(txtEkstra.Text));
                komut.Parameters.AddWithValue("@P9", rchNotlar.Text);
                komut.Parameters.AddWithValue("@P10", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Gider bilgisi güncelleme işlemi başarıyla gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                giderlistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Gider bilgisi güncelleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}

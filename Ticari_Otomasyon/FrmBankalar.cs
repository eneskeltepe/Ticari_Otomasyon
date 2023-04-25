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
using DevExpress.Data.Linq.Helpers;

namespace Ticari_Otomasyon
{
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void bankalistele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute BankaBilgileri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            txtid.Text = "";
            txtBankaAdi.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            txtSube.Text = "";
            mskIBAN.Text = "";
            mskHesapNo.Text = "";
            txtYetkili.Text = "";
            mskTelefon.Text = "";
            mskTarih.Text = "";
            txtHesapTuru.Text = "";
        }

        void sehirlistele()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR from TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        void firmalistele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select ID,AD from TBL_FIRMALAR", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD";
            lookUpEdit1.Properties.DataSource = dt;
        }

        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            bankalistele();
            sehirlistele();
            firmalistele();
            temizle();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Banka bilgisi sisteme eklenecektir.Gerçekten eklemek istiyor musunuz?", "Banka Bilgisi Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("insert into TBL_BANKALAR (BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtBankaAdi.Text);
                komut.Parameters.AddWithValue("@P2", cmbIl.Text);
                komut.Parameters.AddWithValue("@P3", cmbIlce.Text);
                komut.Parameters.AddWithValue("@P4", txtSube.Text);
                komut.Parameters.AddWithValue("@P5", mskIBAN.Text);
                komut.Parameters.AddWithValue("@P6", mskHesapNo.Text);
                komut.Parameters.AddWithValue("@P7", txtYetkili.Text);
                komut.Parameters.AddWithValue("@P8", mskTelefon.Text);
                komut.Parameters.AddWithValue("@P9", mskTarih.Text);
                komut.Parameters.AddWithValue("@P10", txtHesapTuru.Text);
                komut.Parameters.AddWithValue("@P11", lookUpEdit1.EditValue);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Banka bilgisi sisteme başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bankalistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Banka bilgisi ekleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                txtBankaAdi.Text = dr["BANKAADI"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                txtSube.Text = dr["SUBE"].ToString();
                mskIBAN.Text = dr["IBAN"].ToString();
                mskHesapNo.Text = dr["HESAPNO"].ToString();
                txtYetkili.Text = dr["YETKILI"].ToString();
                mskTelefon.Text = dr["TELEFON"].ToString();
                mskTarih.Text = dr["TARIH"].ToString();
                txtHesapTuru.Text = dr["HESAPTURU"].ToString();
                lookUpEdit1.Text = dr["AD"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Banka bilgisi sistemden silinecektir.Gerçekten silmek istiyor musunuz?", "Banka Bilgisi Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from TBL_BANKALAR where ID=@P1", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Banka bilgisi sistemden başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bankalistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Banka bilgisi silme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Banka bilgisi güncellenecektir.Gerçekten güncellemek istiyor musunuz?", "Banka Bilgisi Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Update TBL_BANKALAR set BANKAADI=@P1,IL=@P2,ILCE=@P3,SUBE=@P4,IBAN=@P5,HESAPNO=@P6,YETKILI=@P7,TELEFON=@P8,TARIH=@P9,HESAPTURU=@P10,FIRMAID=@P11 where ID=@P12", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtBankaAdi.Text);
                komut.Parameters.AddWithValue("@P2", cmbIl.Text);
                komut.Parameters.AddWithValue("@P3", cmbIlce.Text);
                komut.Parameters.AddWithValue("@P4", txtSube.Text);
                komut.Parameters.AddWithValue("@P5", mskIBAN.Text);
                komut.Parameters.AddWithValue("@P6", mskHesapNo.Text);
                komut.Parameters.AddWithValue("@P7", txtYetkili.Text);
                komut.Parameters.AddWithValue("@P8", mskTelefon.Text);
                komut.Parameters.AddWithValue("@P9", mskTarih.Text);
                komut.Parameters.AddWithValue("@P10", txtHesapTuru.Text);
                komut.Parameters.AddWithValue("@P11", lookUpEdit1.EditValue);
                komut.Parameters.AddWithValue("@P12", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Banka bilgisi başarıyla güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bankalistele();
                temizle();
            }
            else
            {
                MessageBox.Show("Banka bilgisi güncelleme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

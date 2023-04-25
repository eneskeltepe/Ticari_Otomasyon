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
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void musterilistele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select AD,SOYAD,TELEFON as 'TELEFON NUMARASI 1',TELEFON2 as 'TELEFON NUMARASI 2',MAIL as 'MAİL ADRESİ' from  TBL_MUSTERILER Order By ID asc", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void firmalistele()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select AD as 'FİRMA ADI',YETKILIADSOYAD as 'YETKİLİ AD SOYAD',TELEFON1 as 'TELEFON NUMARASI 1',TELEFON2 as 'TELEFON NUMARASI 2',TELEFON3 as 'TELEFON NUMARASI 3',MAIL  as 'MAİL ADRESİ',FAX as 'FAX NUMARASI' from TBL_FIRMALAR Order By ID asc", bgl.baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;
        }

        private void FrmRehber_Load(object sender, EventArgs e)
        {
            musterilistele();
            firmalistele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frm = new FrmMail();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                frm.mail = dr["MAİL ADRESİ"].ToString();
            }
            frm.Show();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frm = new FrmMail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                frm.mail = dr["MAİL ADRESİ"].ToString();
            }
            frm.Show();
        }
    }
}

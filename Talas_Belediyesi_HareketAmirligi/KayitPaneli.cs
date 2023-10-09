using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Talas_Belediyesi_HareketAmirligi
{
    public partial class FrmKayitPaneli : Form
    {
        public FrmKayitPaneli()
        {
            InitializeComponent();
        }

        BaglantiSinifi bgl = new BaglantiSinifi();

        void Listele()
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From Tbl_HareketAmirligi", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void FrmKayitPaneli_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmAnaMenu fr = new FrmAnaMenu();
            fr.Show();
            this.Hide();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (
                  txtSoforAdi.Text == "" || dTPTarih.Text == "" || txtPlaka.Text == "" || txtSoforHizmeti.Text == "" || mskCikisSaat.Text == "" || mskGelisSaat.Text == "" || txtAciklama.Text == "" ||
                  txtSoforAdi.Text == string.Empty || dTPTarih.Text == string.Empty || txtPlaka.Text == string.Empty || txtSoforHizmeti.Text == string.Empty || mskCikisSaat.Text == string.Empty || mskGelisSaat.Text == string.Empty || txtAciklama.Text == string.Empty
               )
            {
                txtSoforAdi.BackColor = Color.Yellow;
                dTPTarih.BackColor = Color.Yellow;
                txtPlaka.BackColor = Color.Yellow;
                txtSoforHizmeti.BackColor = Color.Yellow;
                mskCikisSaat.BackColor = Color.Yellow;
                mskGelisSaat.BackColor = Color.Yellow;
                txtAciklama.BackColor = Color.Yellow;
                MessageBox.Show("Sarı Rekli Alanları Boş Geçemezsiniz", "Boş Alan Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlConnection baglanti = new SqlConnection(bgl.Adres);
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into Tbl_HareketAmirligi (TARIH,KM,PLAKA,SOFORHIZMETI,SOFORADI,CIKISSAAT,GELİSSAAT,ACIKLAMA) " +
                                                  "VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", baglanti);
                komut.Parameters.AddWithValue("@p1", dTPTarih.Text);
                komut.Parameters.AddWithValue("@p2", txtKM.Text);
                komut.Parameters.AddWithValue("@p3", txtPlaka.Text);
                komut.Parameters.AddWithValue("@p4", txtSoforHizmeti.Text);
                komut.Parameters.AddWithValue("@p5", txtSoforAdi.Text);
                komut.Parameters.AddWithValue("@p6", mskCikisSaat.Text);
                komut.Parameters.AddWithValue("@p7", mskGelisSaat.Text);
                komut.Parameters.AddWithValue("@p8", txtAciklama.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                Listele();
                MessageBox.Show("Sisteme Kaydedildi", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);

                baglanti.Open();
                SqlCommand komut1 = new SqlCommand("insert into Tbl_HareketAmirligiHizmetler (TARIH,HIZMETTURLERI) VALUES (@P1,@P2)", baglanti);
                komut1.Parameters.AddWithValue("@p1", dTPTarih.Text);
                komut1.Parameters.AddWithValue("@p2", txtSoforHizmeti.Text);
                komut1.ExecuteNonQuery();
                baglanti.Close();

            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete from Tbl_HareketAmirligi where ID=@P1", baglanti);
            komut.Parameters.AddWithValue("@P1", txtID.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Sistemden Silindi", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("Delete from Tbl_HareketAmirligiHizmetler where ID=@P1", baglanti);
            komut1.Parameters.AddWithValue("@P1", txtID.Text);
            komut1.ExecuteNonQuery();           
            Listele();
            baglanti.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Tbl_HareketAmirligi set TARIH=@P1, KM=@P2, PLAKA=@P3, SOFORHIZMETI=@P4, SOFORADI=@P5, CIKISSAAT=@P6, GELİSSAAT=@P7, ACIKLAMA=@P8 WHERE ID=@P9 ", baglanti);
            komut.Parameters.AddWithValue("@p1", dTPTarih.Text);
            komut.Parameters.AddWithValue("@p2", txtKM.Text);
            komut.Parameters.AddWithValue("@p3", txtPlaka.Text);
            komut.Parameters.AddWithValue("@p4", txtSoforHizmeti.Text);
            komut.Parameters.AddWithValue("@p5", txtSoforAdi.Text);
            komut.Parameters.AddWithValue("@p6", mskCikisSaat.Text);
            komut.Parameters.AddWithValue("@p7", mskGelisSaat.Text);
            komut.Parameters.AddWithValue("@p8", txtAciklama.Text);
            komut.Parameters.AddWithValue("@p9", txtID.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Sistem Güncellendi", "Güncelle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("update Tbl_HareketAmirligiHizmetler set TARIH=@P1, HIZMETTURLERI=@P2 WHERE ID=@P3 ", baglanti);
            komut1.Parameters.AddWithValue("@p1", dTPTarih.Text);
            komut1.Parameters.AddWithValue("@p2", txtSoforHizmeti.Text);
            komut1.Parameters.AddWithValue("@p3", txtID.Text);
            komut1.ExecuteNonQuery();
            MessageBox.Show("Sistem Güncellendi", "Güncelle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            dTPTarih.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtKM.Text= dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPlaka.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSoforHizmeti.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtSoforAdi.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            mskCikisSaat.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            mskGelisSaat.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtAciklama.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From Tbl_HareketAmirligi where SOFORHIZMETI like '%" + txtAra.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);
        private void FrmKayitPaneli_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void dTPTarih_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
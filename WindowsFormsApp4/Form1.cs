using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        UrunDal _urunDal = new UrunDal();
        private void Form1_Load(object sender, EventArgs e)
        {
            urunlistele();
            kategori();
            kategori2();

        }
        private void kategori2()
        {
            var kategori = _urunDal.KategoriGetAll();
            comboBox2.DataSource = kategori;
            comboBox2.DisplayMember = "KatagoriAdi";
            comboBox2.ValueMember = "Id";

        }
        private void kategori()
        {
            var kategori = _urunDal.KategoriGetAll();
            comboBox1.DataSource = kategori;
            comboBox1.DisplayMember = "KatagoriAdi";
            comboBox1.ValueMember = "Id";
       
        }

        private void urunlistele()
        {
            dataGridView1.DataSource = _urunDal.GetAll();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            var kategoriAdi = comboBox1.Text;
            var KategoriId = _urunDal.kategoriId(kategoriAdi);
            if (KategoriId != -1)
            {
                _urunDal.Addurun(new tblUrun
                {
                    UrunAdi = txtAdı.Text,
                    Fiyat = Convert.ToDecimal(txtfiyat.Text),
                    Stok = Convert.ToInt32(txtstok.Text),
                    KategoriId = KategoriId,
                });
                MessageBox.Show("Başarlı Bir Şekilde Eklendi");
                urunlistele();
            }
            else
            {
                MessageBox.Show("Böyle Bir Kategori Yok");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var kategori = _urunDal.KategoriGetAll();
            comboBox1.DataSource = kategori;
            comboBox1.DisplayMember = "KatagoriAdi";
            comboBox1.ValueMember = "Id";


        }


        
        private void btnguncelle_Click_1(object sender, EventArgs e)
        {
            using ( AA_OdevEntities _e = new AA_OdevEntities())
            {
                tblUrun tbl_Urunler = _e.tblUrun.Find(dataGridView1.CurrentRow.Cells[0].Value);
                if (tbl_Urunler != null)
                {
                    tbl_Urunler.UrunAdi = txtadgnc.Text;
                    tbl_Urunler.Fiyat = Convert.ToDecimal(txtfiyatgnc.Text);
                    tbl_Urunler.Stok = Convert.ToInt32(txtstokgnc.Text);
                    tbl_Urunler.KategoriId = Convert.ToInt32(comboBox2.SelectedValue);

                }
                MessageBox.Show("Ürün güncellendi.");
                _e.Entry(tbl_Urunler).State = EntityState.Modified;

                _e.SaveChanges();

                urunlistele();


            }
        }


        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtadgnc.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtfiyatgnc.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtstokgnc.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

        }
    }
}






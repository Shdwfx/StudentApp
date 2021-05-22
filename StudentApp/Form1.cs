using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace StudentApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
            dataGridView2.Rows[0].Selected = true;
            int displayIndex = 0 + 1;
            textBox5.Text = "Kayıt: " + displayIndex + " / " + dataGridView2.RowCount;

        }

        public void Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox6.Clear();
            comboBox1.SelectedIndex = -1;
            maskedTextBox1.Clear();

        }
        


            public void DataGetir()
        {
            SQLiteConnection con = new SQLiteConnection(@"data source=\studentapp.db");
            con.Open();


            string query = "SELECT* from ogrenci";
            SQLiteCommand cmd = new SQLiteCommand(query, con);

            DataTable dt = new DataTable();

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

            adapter.Fill(dt);

            dataGridView2.DataSource = dt;
            dataGridView2.Rows[0].Selected = true;
            int displayIndex = 1;
            textBox5.Text = "Kayıt: " + displayIndex + " / " + dataGridView2.RowCount;


            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            DataGetir();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Kaydı Eklemek İstiyor musunuz?", "Ekleme İşlemi", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                SQLiteConnection con = new SQLiteConnection(@"data source=\studentapp.db");
                con.Open();
                string query = "INSERT INTO ogrenci (Ad,Soyad,Cinsiyet,DogumTarihi,Adres) VALUES (" + "'" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox1.SelectedItem + "','" + maskedTextBox1.Text + "','" + textBox4.Text.Replace(Environment.NewLine, "") + "')";
                SQLiteCommand cmd = new SQLiteCommand(query, con);

                DataTable dt = new DataTable();

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                adapter.Fill(dt);


                DataGetir();
                Temizle();
            }
            else
            {
                MessageBox.Show("İşlem İptal edildi");
            }
            DataGetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Kaydı Silmek İstiyor musunuz?", "Ekleme İşlemi", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                string secilenOkulNo = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                SQLiteConnection con = new SQLiteConnection(@"data source=\studentapp.db");
                con.Open();
                string query = "DELETE FROM ogrenci WHERE OkulNo = " + secilenOkulNo;
                SQLiteCommand cmd = new SQLiteCommand(query, con);

                DataTable dt = new DataTable();

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                adapter.Fill(dt);
                DataGetir();
            }
            else
            {
                MessageBox.Show("İşlem İptal edildi");
            }
            DataGetir();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
            int getirilecekIndex = dataGridView2.SelectedRows[0].Cells[3].RowIndex;
            comboBox1.SelectedIndex = getirilecekIndex - 1;
            maskedTextBox1.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Kaydı Güncellemek İstiyor musunuz?", "Ekleme İşlemi", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                string secilenOkulNo = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                SQLiteConnection con = new SQLiteConnection(@"data source=\studentapp.db");
                con.Open();
                string query = "DELETE FROM ogrenci WHERE OkulNo = " + secilenOkulNo;
                SQLiteCommand cmd = new SQLiteCommand(query, con);

                DataTable dt = new DataTable();

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                adapter.Fill(dt);


                query = "INSERT INTO ogrenci (Ad,Soyad,Cinsiyet,DogumTarihi,Adres) VALUES (" + "'" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox1.SelectedItem + "','" + maskedTextBox1.Text + "','" + textBox4.Text.Replace(Environment.NewLine, "") + "')";
                cmd = new SQLiteCommand(query, con);

                dt = new DataTable();

                adapter = new SQLiteDataAdapter(cmd);

                adapter.Fill(dt);


                DataGetir();
                Temizle();
            }
            else
            {
                MessageBox.Show("İşlem İptal edildi");
            }
            DataGetir();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            int currentRow = dataGridView2.SelectedRows[0].Index;
            if (currentRow < dataGridView2.RowCount - 1)
            {
                dataGridView2.Rows[currentRow].Cells[0].Selected = false;
                dataGridView2.Rows[++currentRow].Cells[0].Selected = true;
                int displayIndex = currentRow + 1;
                textBox5.Text = "Kayıt: " + displayIndex + " / " + dataGridView2.RowCount;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int currentRow = dataGridView2.SelectedRows[0].Index;
            if (currentRow > 0)
            {
                dataGridView2.Rows[currentRow].Cells[0].Selected = false;
                dataGridView2.Rows[--currentRow].Cells[0].Selected = true;
                int displayIndex = currentRow + 1;
                textBox5.Text = "Kayıt: " + displayIndex + " / " + dataGridView2.RowCount;

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
            dataGridView2.Rows[dataGridView2.RowCount - 1].Selected = true;
            int displayIndex = dataGridView2.RowCount;
            textBox5.Text = "Kayıt: " + displayIndex + " / " + dataGridView2.RowCount;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = string.Format("Ad LIKE '{0}%'", textBox6.Text);


        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

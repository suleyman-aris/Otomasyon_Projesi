using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Form_Dosyası
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            listView1.FullRowSelect = true;     //burda listviewde herhangibir satıra tıklanıldığında o satırın seçilmesini sağlar
            listView3.FullRowSelect = true;
        }

        int Hesap;      // müsterinin satın aldığı ürünlerin fiyatını hesaplamaada kullanılır

        //kabaca, seçilen dosyadaki verileri listview1 ekler
        private void Form1_Load(object sender, EventArgs e)
        {    
            timer1.Start();             
            progressBar1.Value = 0;
            openFileDialog1.ShowDialog();       //okuma yazma yapacağımız dosyayı seçmemize yarar
            string[] satır = File.ReadAllLines(openFileDialog1.FileName), ekle;         //SEçilen dosyadaki tüm verileri satı adlı değişkene ekler 
            foreach (string s in satır)  //burda satırları tek tek okur
            {
                ekle = s.Split(',');        // okunan satırların arasına virgül koyar bu şekilde listviewe eklemek kolaylaşır
                var ekle1 = new ListViewItem(ekle);
                listView1.Items.Add(ekle1);     // ve burada da listviewe ekler
            }
        }
        //Panel 1 in görünümünü sağlar
        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;                                                 
            panel1.Visible = true;
        }
        //Ürü listesine yeni ürün ekler
        private void Ekle(String Ürün_ismi, String Fiyat, String Birim, String Stok)
        {
            String[] row = { Ürün_ismi, Fiyat, Birim, Stok };           //4 adet değişkenli dizi olşturulur
            ListViewItem item = new ListViewItem(row);
            listView1.Items.Add(item);                  //burada eklenir

            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = "";
        }
        //Girilen Ürünleri güncellediğimiz alan
        private void Güncelle()
        {
            listView1.SelectedItems[0].SubItems[0].Text = textBox1.Text;
            listView1.SelectedItems[0].SubItems[1].Text = textBox2.Text;
            listView1.SelectedItems[0].SubItems[2].Text = textBox3.Text;
            listView1.SelectedItems[0].SubItems[3].Text = textBox4.Text;

            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = "";
        }
        //checkBox1.Checked == true ifadesinin doğru olması halinde seçilen satırı siler
        private void Sil()
        {
            if (checkBox1.Checked == true)
            {
                listView1.Items.RemoveAt(listView1.SelectedIndices[0]);
            }
            else
            {
                MessageBox.Show("Gerçekten Silmek istediğinizden eminseniz yan alanı işaretleyin");
            }
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = "";
            checkBox1.Checked = false;
        }
        //Butona tıklandığında "Ekle" fonksiyonuna gider
        private void button4_Click(object sender, EventArgs e)
        {
            Ekle(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
        }
        //Butona tıklandığında "Güncelle" fonksyonuna gider
        private void button5_Click(object sender, EventArgs e)
        {
            Güncelle();
        }
        //Butona tıklandığında "Sil" fonksyonuna gider
        private void button6_Click(object sender, EventArgs e)
        {
            Sil();
        }
        //Butona tıklandığında listview1deki bütün satırları siler
        private void button7_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                listView1.Items.Clear();
                radioButton1.Checked = false;
            }
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = "";
        }
        //listviewdeki herhangi satıra tıklandıında o satırdaki verileri textboxlara gönderir
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;

            label7.Text = textBox1.Text;
            label9.Text = textBox2.Text;
            label10.Text = textBox3.Text;
        }
        //Butona tıklandığında Panel 2 açılır
        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }
        //Butona tıklandığında Ekle1 fonksiyonuna parantez içindeki değerleri gönderir
        private void button11_Click(object sender, EventArgs e)
        {
            Ekle1(label7.Text, Convert.ToInt32(label9.Text), numericUpDown1.Text, label10.Text);
        }
        //Kendisine gelen değerleri işler ve listviewe ekler
        private void Ekle1(String Ad, int Miktar, String Total, String Birim)
        {
            int fiyat = Convert.ToInt32(Miktar); //Alınan değerleri fiyat değikenine atar
            int adet = Convert.ToInt32(Total);  //Alınan değerleri fiyat değikenine atar
            Total = Convert.ToString(fiyat * adet);  //Miktar ile fiyati çarpar ve sonucu değişkene atar
            String[] row = { Ad, label9.Text, Total, Birim };   //diziye değerleri alır 
            ListViewItem item1 = new ListViewItem(row);         //aldiği değerleri listviewe alır
            listView3.Items.Add(item1);                        //listview3 e ekler

            numericUpDown1.Text = "";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Value = 100;
            int a1 = listView1.Items.Count;         //listView1 satır sayısını tutmak için kullanılır
            int a = listView3.Items.Count;          //listView3 satır sayısını tutmak için kullanılır
            for (int j = 0; j < a; j++)
            {
                Hesap += Convert.ToInt32(listView3.Items[j].SubItems[2].Text); //Ürünlerin toplam fiyatını hesaplar
                for (int i = 0; i < a1; i++)
                {
                    if (listView1.Items[i].SubItems[0].Text == listView3.Items[j].SubItems[0].Text)
                    {
                        int adet = Convert.ToInt32(listView3.Items[j].SubItems[2].Text) / Convert.ToInt32(listView3.Items[j].SubItems[1].Text); //ürünlerin stok sayılarından düşmek için kullanırlır
                        listView1.Items[i].SubItems[3].Text = Convert.ToString(Convert.ToInt32(listView1.Items[i].SubItems[3].Text) - adet);
                    }
                }
            }

            if (Hesap > 100)
            {
                int indirim = Hesap / 10;  // %10 indirim uygularmak için kullanılır
                Hesap -= indirim;
            }
            label11.Text = Convert.ToString(Hesap);
            Hesap = 0;
        }
        //Butona tıklandığında "Güncelle1" fonksiyonuna gider
        private void button10_Click(object sender, EventArgs e)
        {
            Güncelle1(Convert.ToInt32(numericUpDown1.Text), Convert.ToInt32(label9.Text));
        }
        //Satın Alınan ürünü günceller
        private void Güncelle1(int adet, int fiyat)
        {
            listView3.SelectedItems[0].SubItems[1].Text = label9.Text;
            listView3.SelectedItems[0].SubItems[2].Text = Convert.ToString(adet * fiyat);
        }
        //Tıklanan satırdaki verileri gerekli yerlere yazdırır
        private void listView3_MouseClick(object sender, MouseEventArgs e)
        {
            label7.Text = listView3.SelectedItems[0].SubItems[0].Text;
            label9.Text = listView3.SelectedItems[0].SubItems[1].Text;
            int adet = Convert.ToInt32(listView3.SelectedItems[0].SubItems[2].Text) / Convert.ToInt32(listView3.SelectedItems[0].SubItems[1].Text); //gerekli bölme işlemiyle kaç adet alındığını değşkene atar
            label10.Text = listView3.SelectedItems[0].SubItems[3].Text;

            numericUpDown1.Text = Convert.ToString(adet);
        }
        //Butona tıkalndığında "Sil1" fonksiyonuna gider
        private void button9_Click(object sender, EventArgs e)
        {
            Sil1();
        }
        //Seçilen satırı siler
        private void Sil1()
        {
            listView3.Items.RemoveAt(listView3.SelectedIndices[0]);
            numericUpDown1.Text = "";
        }
        //Tıklandığında bütün listview deki satırlar temizler
        private void button8_Click(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            numericUpDown1.Text = "";
        }
        //Gerçek zamanlı saatı labele bastırır
        private void timer1_Tick(object sender, EventArgs e)
        {
            label13.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        //Bu kısımda kapatırken en başta seçilen txt. dosyasına verileri kaydeder kısaca burada eski verileri silme ve yenilenen verileri güncelleme ve yeni verileri eklme işlemleri gerçekleşir
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.WriteAllText(openFileDialog1.FileName, "");
            int a1 = listView1.Items.Count;  //listView1'in satır sayısını değişkene atar

            for (int i = 0; i < a1; i++)
                File.AppendAllText(openFileDialog1.FileName, listView1.Items[i].SubItems[0].Text + "," + listView1.Items[i].SubItems[1].Text + "," + listView1.Items[i].SubItems[2].Text + "," + listView1.Items[i].SubItems[3].Text + "\n");
        }
    }
}

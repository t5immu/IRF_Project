using IRF_T5IMMU.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRF_T5IMMU
{
    public partial class Form2 : Form
    {
        
        bool nagyobb = true;
        string[] fejlecek = new string[6];
        BindingList<Adatok> _2019Q3 = new BindingList<Adatok>();
        BindingList<Adatok> _2020Q3 = new BindingList<Adatok>();
        public Form2()
        {
            InitializeComponent();
            fejlecek_betoltese();
            listBox1.DataSource = fejlecek;

            Adatbetoltes1();
            Adatbetoltes2();
        }
        void fejlecek_betoltese()
        {
            fejlecek[0]="utszam";
            fejlecek[1] = "eltnap";
            fejlecek[2] = "koltes";
            fejlecek[3] = "tartnap";
            fejlecek[4] = "napikoltes";
        }

        void lekerdezes()
        {
            if (nagyobb)
            {
                string st;
                st = listBox1.SelectedItem.ToString();
                var v = (from i in _2019Q3
                         where i.koltes.Equals(int.Parse(textBox1.Text))
                         select i).ToList();
                dataGridView1.DataSource = v;
            }
            else
            {
                string st;
                st = listBox1.SelectedItem.ToString();
                var v = (from i in _2019Q3
                         where i.koltes.Equals(int.Parse(textBox1.Text))
                         select i).ToList();
                dataGridView1.DataSource = v;
            }
        }

        void lekerdezes2()
        {
            if (nagyobb)
            {
                string st;
                st = listBox1.SelectedItem.ToString();
                var v = (from i in _2020Q3
                         where i.koltes.Equals(int.Parse(textBox1.Text))
                         select i).ToList();
                dataGridView2.DataSource = v;
            }
            else
            {
                string st;
                st = listBox1.SelectedItem.ToString();
                var v = (from i in _2020Q3
                         where i.koltes.Equals(int.Parse(textBox1.Text))
                         select i).ToList();
                dataGridView2.DataSource = v;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nagyobb = false;
            lekerdezes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nagyobb = true;
            lekerdezes();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lekerdezes();
            lekerdezes2();
        }

        public void Adatbetoltes1()
        {

            using (StreamReader sr = new StreamReader("2019_Q3.csv", Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(';');
                    Adatok a = new Adatok();
                    a.orszag = line[0];
                    a.utszam = int.Parse(line[1]);
                    a.eltnap = int.Parse(line[2]);
                    a.koltes = int.Parse(line[3]);
                    a.tartnap = double.Parse(line[4]);
                    a.napikoltes = double.Parse(line[5]);
                    _2019Q3.Add(a);
                }
            }

        }

        public void Adatbetoltes2()
        {

            using (StreamReader sr = new StreamReader("2020_Q3.csv", Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(';');
                    Adatok a = new Adatok();
                    a.orszag = line[0];
                    a.utszam = int.Parse(line[1]);
                    a.eltnap = int.Parse(line[2]);
                    a.koltes = int.Parse(line[3]);
                    a.tartnap = double.Parse(line[4]);
                    a.napikoltes = double.Parse(line[5]);
                    _2020Q3.Add(a);
                }
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}

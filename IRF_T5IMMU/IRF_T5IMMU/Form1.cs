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
    public partial class Form1 : Form
    {
        
        private List<Adatok> _2020Q3 = new List<Adatok>();

        public Form1()
        {
            InitializeComponent();
            Adatbetoltes1();
            Adatbetoltes2();
        }

        public void Adatbetoltes1()
        {
            List<Adatok> _2019Q3 = new List<Adatok>();

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

        private void Adatbetoltes2()
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }
    }

}

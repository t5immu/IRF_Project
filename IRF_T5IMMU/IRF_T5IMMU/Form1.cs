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
        private List<Adatok> _2019Q3 = new List<Adatok>();
        private List<Adatok> _2020Q3 = new List<Adatok>();

        public Form1()
        {
            InitializeComponent();
            Adatbetoltes1();
            Adatbetoltes2();
        }

        private void Adatbetoltes1()
        {
            _2019Q3.Clear();

            using (StreamReader sr = new StreamReader("2019_Q3.csv", Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(';');

                    Adatok a = new Adatok();
                    a.utszam = int.Parse(line[0]);
                    a.eltnap = int.Parse(line[1]);
                    a.koltes = int.Parse(line[2]);
                    a.tartnap = double.Parse(line[3]);
                    a.napikoltes = double.Parse(line[4]);
                    _2019Q3.Add(a);
                }
            }
        }

        private void Adatbetoltes2()
        {
            _2020Q3.Clear();

            using (StreamReader sr = new StreamReader("2020_Q3.csv", Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(';');

                    Adatok a = new Adatok();
                    a.utszam = int.Parse(line[0]);
                    a.eltnap = int.Parse(line[1]);
                    a.koltes = int.Parse(line[2]);
                    a.tartnap = double.Parse(line[3]);
                    a.napikoltes = double.Parse(line[4]);
                    _2020Q3.Add(a);
                }
            }

        }
    }

}

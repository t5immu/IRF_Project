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
    public partial class Form3 : Form
    {
        BindingList<Adatok> _2019Q3 = new BindingList<Adatok>();
        BindingList<Adatok> _2020Q3 = new BindingList<Adatok>();
        Oszlop o19 = new Oszlop();
        public Form3()
        {
            InitializeComponent();
            Adatbetoltes1();
            Adatbetoltes2();

            orszagok();
            valtas();

        }

        void orszagok()
        {
            string[] orszagok = new string[_2019Q3.Count];
            int i = 0;
            foreach (var a in _2019Q3)
            {
                orszagok[i] = a.orszag;
                i++;
            }
            listBox1.DataSource = orszagok;
        }

        void valtas()
        {
            o19.Left=10;
            o19.Top = 10;
            Controls.Add(o19);

            string st = listBox1.SelectedItem.ToString(); ;
            var v = (from i in _2019Q3
                     where i.orszag == st
                     select i.eltnap);
            MessageBox.Show(v.ToString());

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
    }
}

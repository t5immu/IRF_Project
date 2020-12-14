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
        Oszlop2 o20 = new Oszlop2();
        int m19;
        int m20;
        Label l19 = new Label();
        Label l20 = new Label();
        string[] fejlecek = new string[3];
        public Form3()
        {
            InitializeComponent();
            Adatbetoltes1();
            Adatbetoltes2();
            fejlecek_betoltese();

            orszagok();
            o19.Left = 280;
            o19.Top = this.Height-180;
            o19.Height = 100;
            l19.Left = 280;
            l19.Top = this.Height - 70;
            l19.Width = 50;
            l19.Text = "0";
            Controls.Add(o19) ;
            Controls.Add(l19);

            o20.Left = 340;
            o20.Top = this.Height - 180;
            o20.Height = 100;
            o20.BackColor = Color.Red;
            l20.Left = 340;
            l20.Top = this.Height - 70;
            l20.Text = "0";
            Controls.Add(o20);
            Controls.Add(l20);

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

        void fejlecek_betoltese()
        {
            fejlecek[0] = "utazások";
            fejlecek[1] = "eltöltött napok";
            fejlecek[2] = "költes";

            listBox2.DataSource = fejlecek;
        }

        void valtas()
        {
            int oszto;
            string orszag;
            orszag = listBox1.SelectedItem.ToString();
            string st;
            st = listBox2.SelectedItem.ToString();
            if (st.Equals(fejlecek[0]))
            {
                oszto = 16;
                m19 = (from i in _2019Q3
                       where i.orszag == orszag
                       select i.utszam).First();

                o19.Height = m19 / oszto;
                o19.Top = this.Height - 80 - m19 / oszto;
                l19.Text = m19.ToString();

                m20 = (from i in _2020Q3
                       where i.orszag == orszag
                       select i.utszam).First();

                o20.Height = m20 / oszto;
                o20.Top = this.Height - 80 - m20 / oszto;
                l20.Text = m20.ToString();
            }
            else if (st.Equals(fejlecek[1]))
            {
                oszto = 30;

                m19 = (from i in _2019Q3
                       where i.orszag == orszag
                       select i.eltnap).First();

                o19.Height = m19 / oszto;
                o19.Top = this.Height - 80 - m19 / oszto;
                l19.Text = m19.ToString();

                m20 = (from i in _2020Q3
                       where i.orszag == orszag
                       select i.eltnap).First();

                o20.Height = m20 / oszto;
                o20.Top = this.Height - 80 - m20 / oszto;
                l20.Text = m20.ToString();
            }
            else if (st.Equals(fejlecek[2]))
            {
                oszto = 420;
                m19 = (from i in _2019Q3
                       where i.orszag == orszag
                       select i.koltes).First();

                o19.Height = m19 / oszto;
                o19.Top = this.Height - 80 - m19 / oszto;
                l19.Text = m19.ToString();

                m20 = (from i in _2020Q3
                       where i.orszag == orszag
                       select i.koltes).First();

                o20.Height = m20 / oszto;
                o20.Top = this.Height - 80 - m20 / oszto;
                l20.Text = m20.ToString();
            }
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            valtas();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //valtas();
        }
    }
}

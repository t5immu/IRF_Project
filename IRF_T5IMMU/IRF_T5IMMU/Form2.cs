using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public Form2()
        {
            InitializeComponent();
            fejlecek_betoltese();
            listBox1.DataSource = fejlecek;

            lekerdezes();
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
            ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nagyobb = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nagyobb = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nagyobb)
            {
                string st;
                st = listBox1.SelectedItem.ToString();
                var v = (from i in _2019Q3
                         where i.st.Equals(int.Parse(textBox1.Text))
                         select i).ToList();
                dataGridView1.DataSource = v;
            }
            else
            {
                string st;
                st = listBox1.SelectedItem.ToString();
                var v = (from i in _2019Q3
                         where i.st.Equals(int.Parse(textBox1.Text))
                         select i).ToList();
                dataGridView1.DataSource = v;
            }
        }
    }
}

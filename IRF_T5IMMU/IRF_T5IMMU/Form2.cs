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
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;



namespace IRF_T5IMMU
{
    public partial class Form2 : Form
    {
        bool nagyobb = true;
        string[] fejlecek = new string[6];
        BindingList<Adatok> _2019Q3 = new BindingList<Adatok>();
        BindingList<Adatok> _2020Q3 = new BindingList<Adatok>();
        List<Adatok> szurt = new List<Adatok>();
        List<Adatok> szurt2 = new List<Adatok>();

        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;
        Excel.Worksheet xlSheet2;

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
            fejlecek[0]= "utazások";
            fejlecek[1] = "eltöltött napok";
            fejlecek[2] = "költes";
            fejlecek[3] = "átl. tartózkodási idő";
            fejlecek[4] = "napi átl. költés";
        }

        void lekerdezes()
        {
            List<Adatok> v = null;
            string st;
            st = listBox1.SelectedItem.ToString();
            if (st.Equals(fejlecek[0]))
            {
                v = (from i in _2019Q3
                     where (nagyobb && i.utszam > (int.Parse(textBox1.Text))) || (!nagyobb && i.utszam < (int.Parse(textBox1.Text)))
                     select i).ToList();
            }
            else if (st.Equals(fejlecek[1]))
            {
                v = (from i in _2019Q3
                     where (nagyobb && i.eltnap > (int.Parse(textBox1.Text))) || (!nagyobb && i.eltnap < (int.Parse(textBox1.Text)))
                     select i).ToList();
            }
            else if (st.Equals(fejlecek[2]))
            {
                v = (from i in _2019Q3
                     where (nagyobb && i.koltes > (int.Parse(textBox1.Text))) || (!nagyobb && i.koltes < (int.Parse(textBox1.Text)))
                     select i).ToList();
            }
            else if (st.Equals(fejlecek[3]))
            {
                v = (from i in _2019Q3
                     where (nagyobb && i.tartnap > (int.Parse(textBox1.Text))) || (!nagyobb && i.tartnap < (int.Parse(textBox1.Text)))
                     select i).ToList();
            }
            else if (st.Equals(fejlecek[4]))
            {
                v = (from i in _2019Q3
                     where (nagyobb && i.napikoltes > (int.Parse(textBox1.Text))) || (!nagyobb && i.napikoltes < (int.Parse(textBox1.Text)))
                     select i).ToList();
            }
            dataGridView1.DataSource = v;
            szurt = v;
        }

        void lekerdezes2()
        {
            List<Adatok> v2 = null;

            string st;
            st = listBox1.SelectedItem.ToString();
            if (st.Equals(fejlecek[0]))
            {
                v2 = (from i in _2020Q3
                     where (nagyobb && i.utszam > (int.Parse(textBox1.Text))) || (!nagyobb && i.utszam < (int.Parse(textBox1.Text)))
                     select i).ToList();
            }
            else if (st.Equals(fejlecek[1]))
            {
                v2 = (from i in _2020Q3
                     where (nagyobb && i.eltnap > (int.Parse(textBox1.Text))) || (!nagyobb && i.eltnap < (int.Parse(textBox1.Text)))
                     select i).ToList();
            }
            else if (st.Equals(fejlecek[2]))
            {
                v2 = (from i in _2020Q3
                     where (nagyobb && i.koltes > (int.Parse(textBox1.Text))) || (!nagyobb && i.koltes < (int.Parse(textBox1.Text)))
                     select i).ToList();
            }
            else if (st.Equals(fejlecek[3]))
            {
                v2 = (from i in _2020Q3
                     where (nagyobb && i.tartnap > (int.Parse(textBox1.Text))) || (!nagyobb && i.tartnap < (int.Parse(textBox1.Text)))
                     select i).ToList();
            }
            else if (st.Equals(fejlecek[4]))
            {
                v2 = (from i in _2020Q3
                     where (nagyobb && i.napikoltes > (int.Parse(textBox1.Text))) || (!nagyobb && i.napikoltes < (int.Parse(textBox1.Text)))
                     select i).ToList();
            }
            dataGridView2.DataSource = v2;
            szurt2 = v2;
        }

        void CreateTable2020()
        {
            xlSheet.Name = "2020Q3";
            string[] headers = new string[] {
                "Országok",
                "Utazások száma, ezer út",
                "Eltöltött napok száma, ezer nap",
                "Költés, millió Ft",
                "Átlagos tartózkodási idő, nap",
                "Egy fő egy napjára jutó költés, ezer Ft"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, (i + 1)] = headers[i];
            }
            object[,] values = new object[szurt2.Count, headers.Length];

            int counter = 0;
            foreach (Adatok a in szurt2)
            {
                values[counter, 0] = a.orszag;
                values[counter, 1] = a.utszam;
                values[counter, 2] = a.eltnap;
                values[counter, 3] = a.koltes;
                values[counter, 4] = a.tartnap;
                values[counter, 5] = a.napikoltes;
                counter++;
            }

            xlSheet.get_Range(
             GetCell(2, 1),
             GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

            xlSheet.Cells[2 + values.GetLength(0), 1] = "Összesen:";
            xlSheet.Cells[2 + values.GetLength(0), 2] = ("=SZUM(B2:B"+(1 + values.GetLength(0))+")") ;
            xlSheet.Cells[2 + values.GetLength(0), 3] = ("=SZUM(C2:C" + (1 + values.GetLength(0)) + ")");
            xlSheet.Cells[2 + values.GetLength(0), 4] = ("=SZUM(D2:D" + (1 + values.GetLength(0)) + ")");
            xlSheet.Cells[2 + values.GetLength(0), 5] = ("=ÁTLAG(E2:E" + (1 + values.GetLength(0)) + ")");
            xlSheet.Cells[2 + values.GetLength(0), 6] = "=D" + (2 + values.GetLength(0)) + "/C" + (2 + values.GetLength(0));

            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.WrapText = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 70;
            headerRange.Interior.Color = Color.Yellow;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin);
           
            Excel.Range orszagRange = xlSheet.get_Range(GetCell(2, 1), GetCell(2 + values.GetLength(0),1));
            orszagRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            orszagRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline);

            Excel.Range osszesenRange = xlSheet.get_Range(GetCell(2 + values.GetLength(0), 1), GetCell(2 + values.GetLength(0), headers.Length));
            osszesenRange.Font.Bold = true;
            osszesenRange.Font.Color = Color.Red;
        }

        void CreateTable2019()
        {
            xlSheet2.Name = "2019Q3";

            string[] headers = new string[] {
                "Országok",
                "Utazások száma, ezer út",
                "Eltöltött napok száma, ezer nap",
                "Költés, millió Ft",
                "Átlagos tartózkodási idő, nap",
                "Egy fő egy napjára jutó költés, ezer Ft"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet2.Cells[1, (i + 1)] = headers[i];
            }
            object[,] values = new object[szurt.Count, headers.Length];

            int counter = 0;
            foreach (Adatok a in szurt)
            {
                values[counter, 0] = a.orszag;
                values[counter, 1] = a.utszam;
                values[counter, 2] = a.eltnap;
                values[counter, 3] = a.koltes;
                values[counter, 4] = a.tartnap;
                values[counter, 5] = a.napikoltes;
                counter++;
            }

            xlSheet2.get_Range(
             GetCell(2, 1),
             GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

            xlSheet2.Cells[2 + values.GetLength(0), 1] = "Összesen:";
            xlSheet2.Cells[2 + values.GetLength(0), 2] = ("=SZUM(B2:B" + (1 + values.GetLength(0)) + ")");
            xlSheet2.Cells[2 + values.GetLength(0), 3] = ("=SZUM(C2:C" + (1 + values.GetLength(0)) + ")");
            xlSheet2.Cells[2 + values.GetLength(0), 4] = ("=SZUM(D2:D" + (1 + values.GetLength(0)) + ")");
            xlSheet2.Cells[2 + values.GetLength(0), 5] = ("=ÁTLAG(E2:E" + (1 + values.GetLength(0)) + ")");
            xlSheet2.Cells[2 + values.GetLength(0), 6] = "=D" + (2 + values.GetLength(0)) + "/C" + (2 + values.GetLength(0));

            Excel.Range headerRange = xlSheet2.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.WrapText = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 70;
            headerRange.Interior.Color = Color.Yellow;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin);

            Excel.Range orszagRange = xlSheet2.get_Range(GetCell(2, 1), GetCell(2 + values.GetLength(0), 1));
            orszagRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            orszagRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline);

            Excel.Range osszesenRange = xlSheet2.get_Range(GetCell(2 + values.GetLength(0), 1), GetCell(2 + values.GetLength(0), headers.Length));
            osszesenRange.Font.Bold = true;
            osszesenRange.Font.Color = Color.Red;
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

        private void button3_Click(object sender, EventArgs e)
        {
            CreateExcel();
        }

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }

        void CreateExcel()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                xlSheet = xlWB.ActiveSheet;
                CreateTable2020();

                xlWB.Worksheets.Add();
                xlSheet2 = xlWB.Worksheets[1];
                CreateTable2019();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }

        }
    }
}

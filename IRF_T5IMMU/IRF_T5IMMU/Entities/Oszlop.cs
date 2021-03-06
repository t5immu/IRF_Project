﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRF_T5IMMU.Entities
{
    class Oszlop : Label
    {
        public Oszlop()
        {
            Width = 40;
            Paint += Oszlop_Paint;
        }

        private void Oszlop_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);
        }

        protected void DrawImage(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Blue), 0, 0, Width, Height);
        }

    }
}

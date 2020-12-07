using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRF_T5IMMU.Entities
{
    public class Adatok
    {
        public int utszam { get; set; } //utazások száma, ezer út
        public int eltnap { get; set; }//eltöltött napok száma, ezer nap
        public int koltes { get; set; } //költés, millió Ft
        public double tartnap { get; set; } //átlagos tartózkodási idő, nap
        public double napikoltes { get; set; } //egy fő egy napjára jutó költés, ezer Ft
    }
}

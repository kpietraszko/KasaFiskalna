using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasaFiskalna
{
    class Koszyk
    {
        public Dictionary<Produkt,int> Zakupy;
        public Koszyk()
        {
            Zakupy = new Dictionary<Produkt, int>();
        }
    }
}

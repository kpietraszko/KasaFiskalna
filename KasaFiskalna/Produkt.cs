using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasaFiskalna
{
    class Produkt :ICloneable
    {
        string _Nazwa;
        public string Nazwa { get { return _Nazwa; } }
        decimal _CenaJednostkowa;
        public decimal CenaJednostkowa { get { return _CenaJednostkowa; } }
        //ilosc podana dopiero w koszyku
        public Produkt(string nazwa, decimal cenaJednostkowa)
        {
            _Nazwa = nazwa;
            _CenaJednostkowa = cenaJednostkowa;
        }
        public Object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

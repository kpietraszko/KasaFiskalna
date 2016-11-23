using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KasaFiskalna
{
    class Aplikacja
    {
        ConsoleKey Klawisz;
        Koszyk ObecnyKoszyk;
        List<Produkt> WszystkieProdukty = new List<Produkt>();
        public Aplikacja()
        {
            WszystkieProdukty.Add(new Produkt("ziemniaki", 0.80m));
            WszystkieProdukty.Add(new Produkt("cebula", 1.20m));
            WszystkieProdukty.Add(new Produkt("jabłka", 2.40m));
        }
        public void WczytajKlawisz()
        {
            Console.WriteLine(@"Symulator kasy fiskalnej
Dzień dobry!
Co chcesz zrobić? Nacisnij odpowiedni Klawisz.
P - Dodaj produkt do koszyka
K - Skopiuj ostatnio wprowadzony produkt
Z - Wyswietl zawartosc koszyka
S - Wyswietl sume do zapłaty
X - Skasuj produkt z koszyka
W - Wydrukuj paragon
N - Dodaj nowy koszyk
E - Zakończ program");

            Klawisz = Console.ReadKey().Key;
            Console.Clear();
            WykonajDzialanie();
        }
        public void WykonajDzialanie()
        {
            switch (Klawisz)
            {
                case ConsoleKey.P:
                    if (KoszykIstnieje())
                        WyborProduktu();
                    break;
                case ConsoleKey.K:
                    if (KoszykIstnieje())
                    {
                        if (ObecnyKoszyk.Zakupy.Any())
                        {
                            ObecnyKoszyk.Zakupy.Add((Produkt)ObecnyKoszyk.Zakupy.Last().Key.Clone(),
                                ObecnyKoszyk.Zakupy.Last().Value);
                            Console.WriteLine("Skopiowano " + ObecnyKoszyk.Zakupy.Last().Key.Nazwa);
                        }
                        else { Console.WriteLine("Nie ma czego kopiowac"); }
                        Console.ReadKey();
                    }
                    break;
                case ConsoleKey.Z:
                    Console.WriteLine("{0,-10} {1,-6} {2,-9} {3,-6}", "Nazwa", "Ilosc", "Cena jedn.", "Lacznie");
                    foreach (var element in ObecnyKoszyk.Zakupy)
                        Console.WriteLine("{0,-10} {1,-6} {2,-9} {3,-6}", element.Key.Nazwa, element.Value,
                            element.Key.CenaJednostkowa, element.Value * element.Key.CenaJednostkowa);
                    Console.ReadKey();
                    break;
                case ConsoleKey.S:
                    decimal suma = 0.00m;
                    if (KoszykIstnieje())
                        foreach (var element in ObecnyKoszyk.Zakupy)
                            suma += element.Key.CenaJednostkowa * element.Value;
                    Console.WriteLine("Suma do zapłaty: " + suma);
                    Console.ReadKey();
                    break;
                case ConsoleKey.X:
                    if (KoszykIstnieje())
                    {
                        Console.WriteLine("Podaj indeks produktu do usuniecia");
                        int indeks = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            ObecnyKoszyk.Zakupy.Remove(ObecnyKoszyk.Zakupy.ElementAt(indeks).Key);
                            Console.WriteLine("Usunieto");
                        }
                        catch { Console.WriteLine("Usuwanie nie powiodlo sie"); }
                        Console.ReadKey();
                    }
                    break;
                case ConsoleKey.W:
                    if (KoszykIstnieje())
                    {
                        string wydruk = "";
                        wydruk += String.Format("{0,-10} {1,-6} {2,-9} {3,-6}", "Nazwa", "Ilosc", "Cena jedn.", "Lacznie");
                        foreach (var element in ObecnyKoszyk.Zakupy)
                            wydruk += String.Format("{0,-10} {1,-6} {2,-9} {3,-6}", element.Key.Nazwa, element.Value,
                                element.Key.CenaJednostkowa, element.Value * element.Key.CenaJednostkowa);
                        decimal sumaKoszyka = 0.00m;
                        foreach (var element in ObecnyKoszyk.Zakupy)
                            sumaKoszyka += element.Key.CenaJednostkowa * element.Value;
                        wydruk += "Suma do zapłaty: " + sumaKoszyka;
                        File.CreateText(DateTime.Now.ToString("DDMMYYYYGGHHSS")).Close();
                        Console.WriteLine("Zapisano koszyk w pliku tekstowym");
                        File.WriteAllText("DDMMYYYYGGHHSS", wydruk);
                    }
                    Console.ReadKey();
                    break;
                case ConsoleKey.N:
                    ObecnyKoszyk = new Koszyk();
                    Console.WriteLine("Dodano koszyk.");
                    Console.ReadKey();
                    break;
                case ConsoleKey.E:
                    Environment.Exit(0);
                    break;
            }
            Console.Clear();
            WczytajKlawisz();
        }
        void WyborProduktu()
        {
            Console.WriteLine("Wybierz produkt wpisujac odpowiednia liczbe");
            for (int i = 0; i < WszystkieProdukty.Count; i++)
            {
                Console.WriteLine(i + " " + WszystkieProdukty[i].Nazwa);
            }
            int NrWybranego = Convert.ToInt32(Console.ReadLine());
            Produkt wybrany;
            int ilosc;
            if (NrWybranego >= 0 && NrWybranego < WszystkieProdukty.Count)
            {
                wybrany = WszystkieProdukty[NrWybranego];
                Console.WriteLine("Podaj ilosc (calkowita)");
                try { ilosc = Convert.ToInt32(Console.ReadLine()); }
                catch
                {
                    Console.WriteLine("To nie jest liczba calkowita, powrot do menu");
                    Console.ReadKey();
                    return;
                }
                ObecnyKoszyk.Zakupy.Add(wybrany, ilosc);
                Console.WriteLine("Dodano " + wybrany.Nazwa + " do koszyka");
            }
            else
            {
                Console.WriteLine("Produkt o podanym numerze nie istnieje");
                Console.ReadKey();
            }
        }
        bool KoszykIstnieje()
        {
            if (ObecnyKoszyk == null)
            {
                Console.WriteLine("Najpierw dodaj koszyk!");
                Console.ReadKey();
                return false;
            }
            return true;
        }

    }
}

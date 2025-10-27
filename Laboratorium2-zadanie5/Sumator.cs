using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Laboratorium2_zadanie5
{

    /// <summary>
    /// 
    /// Autor: Maksymilian Tołpa
    /// Werjsa: 1.0
    /// 
    /// Zadanie: Laboratorium 2 - Zadanie 5
    /// 
    /// Polecenie:
    /// Stwórz klasę Sumator z:
    /// • publicznym polem Liczby będącym tablicą liczb
    /// • metodą Suma zwracającą sumę liczb z pola Liczby
    /// • metodę SumaPodziel2 zwracającą sumę liczb z tablicy, które są podzielne przez 2
    /// Zmień widoczność pola Liczby na private oraz dodaj konstruktor.
    /// Dodaj metodę: int IleElementów() zwracającej liczbę elementów na w tablicy
    /// Dodaj metodę wypisującą wszystkie elementy tablicy
    /// Dodaj metodę przyjmującą dwa parametry: lowIndex oraz highIndex, która wypisze elementy o
    /// indeksach >= lowIndex oraz <= highIndex.Metoda powinna zadziałać poprawnie, gdy lowIndex lub
    /// highIndex wykraczają poza zakres tablicy(pominąć te elementy).
    /// 
    /// </summary>

    internal class Sumator
    {

        private int[] Liczby { get; set; }

        public Sumator(int[] liczby)
        {
            Liczby = liczby;
        }

        public int Suma()
        {

            int sumaLiczb = 0;

            for (int i = 0; i < Liczby.Length; i++)
            {
               sumaLiczb += Liczby[i];
            }
            return sumaLiczb; 
        }

        public int SumaPodziel2()
        {
            int sumaLiczb = 0;
            for (int i = 0; i < Liczby.Length; i++)
            {
                if (Liczby[i] % 2 == 0)
                {
                    sumaLiczb += Liczby[i];
                }
            }
            return sumaLiczb;
        }

        public int IleElementow()
        {
            return Liczby.Length;
        }

        public void WypiszElementy()
        {
            for (int i = 0; i < Liczby.Length; i++)
            {
                Console.WriteLine(Liczby[i]);
            }
        }

        public void WypiszElementyZakres(int lowIndex, int highIndex)
        {
            for (int i = lowIndex; i <= highIndex; i++)
            {
                if (i >= 0 && i < Liczby.Length)
                {
                    Console.WriteLine(Liczby[i]);
                }
            }
        }




    }
}

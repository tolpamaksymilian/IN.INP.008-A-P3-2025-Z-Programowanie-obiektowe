using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// Autor: Maksymilian Tołpa
/// Wersja: 1.0
/// Zadanie: Laboratorium 2, Zadanie 3
/// 
/// Polecenie:
/// Napisz klasę Student, która będzie przechowywała imię, nazwisko i tablicę ocen.
///• Zaimplementuj właściwość SredniaOcen, która obliczy i zwróci średnią ocen.
///• Dodaj metodę DodajOcene(int ocena), która doda nową ocenę do tablicy.
///• Zaimplementuj konstruktor inicjujący imię i nazwisko studenta.
/// </summary>

namespace Laboratorium2_zadanie3
{
    internal class Student
    {

        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        private List<int> oceny;

        public Student(string imie, string nazwisko)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            oceny = new List<int>();
        }
        public double SredniaOcen
        {
            get
            {
                if (oceny.Count == 0)
                    return 0;
                return oceny.Average();
            }
        }
        public void DodajOcene(int ocena)
        {
            oceny.Add(ocena);
        }

    }
}

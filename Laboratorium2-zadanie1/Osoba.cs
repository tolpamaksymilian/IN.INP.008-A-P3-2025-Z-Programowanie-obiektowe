using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///Napisz klasę Osoba, która będzie przechowywać informacje o imieniu, nazwisku oraz wieku osoby.
///• Zaimplementuj konstruktor, który będzie przyjmował wszystkie trzy wartości.
///• Użyj właściwości Imie, Nazwisko, Wiek, z walidacją:
///o Imię i Nazwisko muszą mieć co najmniej 2 znaki.
///o Wiek musi być liczbą dodatnią.
///• Zaimplementuj metodę WyswietlInformacje(), która wyświetli informacje o osobie.

namespace Laboratorium2_zadanie1
{
    internal class Osoba
    {
        
        private string imie, nazwisko;
        private int wiek;

        public string Imie
        {
            get
            {
                return imie;
            }
            set
            {
                if (value.Length >= 2)
                {
                    imie = value;
                }
                else
                {
                    throw new ArgumentException("Imię musi mieć co najmniej 2 znaki.");
                }   
            }
        }

        public string Nazwisko
        {
            get
            {
                return nazwisko;
            }
            set
            {
                if (value.Length >= 2)
                {
                    nazwisko = value;
                }
                else
                {
                    throw new ArgumentException("Nazwisko musi mieć co najmniej 2 znaki.");
                }
            }
        }   

        public int Wiek
        {
            get
            {
                return wiek;
            }
            set
            {
                if (value > 0)
                {
                    wiek = value;
                }
                else
                {
                    throw new ArgumentException("Wiek musi być liczbą dodatnią.");
                }
            }
        }

        public Osoba(string imie, string nazwisko, int wiek)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
        }

        public void WyswietlInformacje()
        {
            Console.WriteLine($"Imię: {Imie}, Nazwisko: {Nazwisko}, Wiek: {Wiek}");
        }

    }
}

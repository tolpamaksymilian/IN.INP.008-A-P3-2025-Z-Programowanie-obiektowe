using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorium2_zadanie4
{

    /// <summary>
    /// 
    /// Autor: Maksymilian Tołpa
    /// Wersja: 1.0
    /// 
    /// Zadanie: Zadanie 4 - Laboratorium 2
    /// 
    /// Polecenie:
    /// Stwórz klasę Licz z:
    /// • publicznym polem value przechowującym wartość liczbową.
    /// • metodą Dodaj przyjmującą jeden parametr i dodającą przekazaną wartość do wartości
    /// trzymanej w polu value.
    /// • analogiczną operację odejmij
    /// W Main utwórz kilka obiektów klasy Licz i wykonaj różne operacje.
    /// Do klasy Licz dodaj konstruktor z jednym parametrem - który inicjuje pole wartość na liczbę przekazaną
    /// w parametrze.
    /// Zmień widoczność pola na private i dodaj funkcję wypisującą stan obiektu(pole value).
    /// 
    /// </summary>

    internal class Licz
    {

        private int value;
        public Licz(int initialValue)
        {
            value = initialValue;
        }
        public void Dodaj(int toAdd)
        {
            value += toAdd;
        }
        public void Odejmij(int toSubtract)
        {
            value -= toSubtract;
        }
        public void WypiszStan()
        {
            Console.WriteLine("Aktualna wartość: " + value);
        }


    }
}

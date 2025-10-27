// See https://aka.ms/new-console-template for more information

using Laboratorium2_zadanie2;

BankAccount konto = new BankAccount("Jan Kowalski", 1000);
konto.Wplata(500);
konto.Wyplata(200);
konto.wyswietlSaldo();

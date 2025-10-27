// See https://aka.ms/new-console-template for more information

using Laboratorium2_zadanie5;

Sumator sumator = new Sumator(new int[] { 11,4,57,31,587,32,11,8,14,5,2,56,102,2,8 });

Console.WriteLine("Liczba elementów w tablicy: ");
sumator.IleElementow();

Console.WriteLine("\nWypisanie elementów tablicy: ");
sumator.WypiszElementy();

Console.WriteLine("\nWypisanie elementów tablicy w zakresie od 3 do 7: ");
sumator.WypiszElementyZakres(3, 7);

Console.WriteLine("\nSuma liczb podzielnych przez 2: "+sumator.SumaPodziel2());


Console.WriteLine("\nSuma wszystkich liczb: "+sumator.Suma());


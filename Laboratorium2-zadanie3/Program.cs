// See https://aka.ms/new-console-template for more information

using Laboratorium2_zadanie3;

Student student = new Student("Jan", "Kowalski");
student.DodajOcene(4);
student.DodajOcene(5);
student.DodajOcene(3);
student.DodajOcene(3);
student.DodajOcene(1);
student.DodajOcene(2);
Console.WriteLine($"Student: {student.Imie} {student.Nazwisko}\nŚrednia jego ocen wynosi: {student.SredniaOcen}");
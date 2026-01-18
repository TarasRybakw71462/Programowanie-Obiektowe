using System;
using System.Collections.Generic;

namespace Projekt1
{
    class Program
    {
        static StudentService studentService = new StudentService();
        static PrzedmiotService przedmiotService = new PrzedmiotService();
        static OcenaService ocenaService;

        static void Main()
        {
            ocenaService = new OcenaService(studentService, przedmiotService);
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n=== SYSTEM STUDENCKI ===");
                Console.WriteLine("1. Studenci");
                Console.WriteLine("2. Przedmioty");
                Console.WriteLine("3. Oceny");
                Console.WriteLine("4. Statystyki");
                Console.WriteLine("0. Wyjscie");
                Console.Write("Wybor: ");

                string wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1": MenuStudentow(); break;
                    case "2": MenuPrzedmiotow(); break;
                    case "3": MenuOcen(); break;
                    case "4": PokazStatystyki(); break;
                    case "0": return;
                }
            }
        }

        static void MenuStudentow()
        {
            while (true)
            {
                Console.WriteLine("\n--- STUDENCI ---");
                Console.WriteLine("1. Dodawanie");
                Console.WriteLine("2. Lista");
                Console.WriteLine("3. Wyszukiwanie");
                Console.WriteLine("4. Edytowanie");
                Console.WriteLine("5. Usuwanie");
                Console.WriteLine("0. Powrot");
                Console.Write("Wybor: ");

                string opcja = Console.ReadLine();

                if (opcja == "1")
                {
                    Console.Write("Imie: ");
                    string imie = Console.ReadLine();
                    Console.Write("Nazwisko: ");
                    string nazwisko = Console.ReadLine();
                    Console.Write("Indeks: ");
                    string indeks = Console.ReadLine();
                    Console.Write("Kierunek: ");
                    string kierunek = Console.ReadLine();
                    Console.Write("Rok: ");
                    int rok = int.Parse(Console.ReadLine());

                    studentService.Dodawanie(imie, nazwisko, indeks, kierunek, rok);
                    Console.WriteLine("Dodano studenta.");
                }
                else if (opcja == "2")
                {
                    var studenci = studentService.PobieranieWszystkich();
                    foreach (var s in studenci)
                    {
                        Console.WriteLine($"{s.Id}. {s.Imie} {s.Nazwisko} ({s.Indeks}) - {s.Kierunek}, rok {s.Rok}");
                    }
                }
                else if (opcja == "3")
                {
                    Console.Write("ID studenta: ");
                    int id = int.Parse(Console.ReadLine());
                    var student = studentService.Znajdowanie(id);
                    if (student != null)
                        Console.WriteLine($"Znaleziono: {student.Imie} {student.Nazwisko}");
                    else
                        Console.WriteLine("Nie znaleziono.");
                }
                else if (opcja == "4")
                {
                    Console.Write("ID do edycji: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Nowe imie: ");
                    string imie = Console.ReadLine();
                    Console.Write("Nowe nazwisko: ");
                    string nazwisko = Console.ReadLine();
                    Console.Write("Nowy kierunek: ");
                    string kierunek = Console.ReadLine();
                    Console.Write("Nowy rok: ");
                    int rok = int.Parse(Console.ReadLine());

                    if (studentService.Aktualizowanie(id, imie, nazwisko, kierunek, rok))
                        Console.WriteLine("Zaktualizowano.");
                    else
                        Console.WriteLine("Nie znaleziono studenta.");
                }
                else if (opcja == "5")
                {
                    Console.Write("ID do usuniecia: ");
                    int id = int.Parse(Console.ReadLine());
                    if (studentService.Usuwanie(id))
                        Console.WriteLine("Usunieto.");
                    else
                        Console.WriteLine("Nie znaleziono.");
                }
                else if (opcja == "0")
                {
                    break;
                }
            }
        }

        static void MenuPrzedmiotow()
        {
            while (true)
            {
                Console.WriteLine("\n--- PRZEDMIOTY ---");
                Console.WriteLine("1. Dodawanie");
                Console.WriteLine("2. Lista");
                Console.WriteLine("0. Powrot");
                Console.Write("Wybor: ");

                string opcja = Console.ReadLine();

                if (opcja == "1")
                {
                    Console.Write("Nazwa: ");
                    string nazwa = Console.ReadLine();
                    Console.Write("Prowadzacy: ");
                    string prowadzacy = Console.ReadLine();
                    Console.Write("ECTS: ");
                    int ects = int.Parse(Console.ReadLine());

                    przedmiotService.Dodawanie(nazwa, prowadzacy, ects);
                    Console.WriteLine("Dodano przedmiot.");
                }
                else if (opcja == "2")
                {
                    var przedmioty = przedmiotService.PobieranieWszystkich();
                    foreach (var p in przedmioty)
                    {
                        Console.WriteLine($"{p.Id}. {p.Nazwa} - {p.Prowadzacy}, {p.Ects} ECTS");
                    }
                }
                else if (opcja == "0")
                {
                    break;
                }
            }
        }

        static void MenuOcen()
        {
            while (true)
            {
                Console.WriteLine("\n--- OCENY ---");
                Console.WriteLine("1. Dodawanie");
                Console.WriteLine("2. Lista");
                Console.WriteLine("3. Oceny studenta");
                Console.WriteLine("0. Powrot");
                Console.Write("Wybor: ");

                string opcja = Console.ReadLine();

                if (opcja == "1")
                {
                    Console.Write("ID studenta: ");
                    int studentId = int.Parse(Console.ReadLine());
                    Console.Write("ID przedmiotu: ");
                    int przedmiotId = int.Parse(Console.ReadLine());
                    Console.Write("Ocena (0-100): ");
                    int wartosc = int.Parse(Console.ReadLine());

                    if (ocenaService.Dodawanie(studentId, przedmiotId, wartosc))
                        Console.WriteLine("Dodano ocene.");
                    else
                        Console.WriteLine("Blad: Nieprawidlowe dane.");
                }
                else if (opcja == "2")
                {
                    var oceny = ocenaService.PobieranieWszystkich();
                    foreach (var o in oceny)
                    {
                        Console.WriteLine($"ID:{o.Id} Student:{o.StudentId} Przedmiot:{o.PrzedmiotId} Ocena:{o.Wartosc}");
                    }
                }
                else if (opcja == "3")
                {
                    Console.Write("ID studenta: ");
                    int studentId = int.Parse(Console.ReadLine());
                    var oceny = ocenaService.PobieranieOcenStudenta(studentId);
                    foreach (var o in oceny)
                    {
                        Console.WriteLine($"Przedmiot:{o.PrzedmiotId} Ocena:{o.Wartosc}");
                    }
                    Console.WriteLine($"Srednia: {ocenaService.ObliczanieSredniejStudenta(studentId):F1}");
                }
                else if (opcja == "0")
                {
                    break;
                }
            }
        }

        static void PokazStatystyki()
        {
            Console.WriteLine("\n--- STATYSTYKI ---");

            var studenci = studentService.PobieranieWszystkich();
            var przedmioty = przedmiotService.PobieranieWszystkich();
            var oceny = ocenaService.PobieranieWszystkich();

            Console.WriteLine($"Liczba studentow: {studenci.Count}");
            Console.WriteLine($"Liczba przedmiotow: {przedmioty.Count}");
            Console.WriteLine($"Liczba ocen: {oceny.Count}");

            Console.WriteLine("\nSrednie ocen studentow:");
            foreach (var student in studenci)
            {
                double srednia = ocenaService.ObliczanieSredniejStudenta(student.Id);
                if (srednia > 0)
                    Console.WriteLine($"{student.Imie} {student.Nazwisko}: {srednia:F1}");
            }
        }
    }
}
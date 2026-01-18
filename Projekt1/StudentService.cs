using System;
using System.Collections.Generic;
using System.Linq;

namespace Projekt1
{
    public class StudentService
    {
        private List<Student> studenci = new List<Student>();
        private int id = 1;

        public StudentService()
        {
            // Dane testowe
            studenci.Add(new Student { Id = id++, Imie = "Jan", Nazwisko = "Kowalski", Indeks = "INZ-001", Kierunek = "Informatyka", Rok = 2 });
            studenci.Add(new Student { Id = id++, Imie = "Anna", Nazwisko = "Nowak", Indeks = "INZ-002", Kierunek = "Informatyka", Rok = 1 });
        }

        // Dodawanie
        public void Dodawanie(string imie, string nazwisko, string indeks, string kierunek, int rok)
        {
            studenci.Add(new Student { Id = id++, Imie = imie, Nazwisko = nazwisko, Indeks = indeks, Kierunek = kierunek, Rok = rok });
        }

        // Pobieranie wszystkich
        public List<Student> PobieranieWszystkich()
        {
            return studenci;
        }

        // Znajdowanie po ID
        public Student Znajdowanie(int id)
        {
            return studenci.FirstOrDefault(s => s.Id == id);
        }

        // Aktualizowanie
        public bool Aktualizowanie(int id, string imie, string nazwisko, string kierunek, int rok)
        {
            var student = Znajdowanie(id);
            if (student == null) return false;

            student.Imie = imie;
            student.Nazwisko = nazwisko;
            student.Kierunek = kierunek;
            student.Rok = rok;
            return true;
        }

        // Usuwanie
        public bool Usuwanie(int id)
        {
            var student = Znajdowanie(id);
            if (student == null) return false;

            studenci.Remove(student);
            return true;
        }
    }
}
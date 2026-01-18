using System;
using System.Collections.Generic;
using System.Linq;

namespace Projekt1
{
    public class OcenaService
    {
        private List<Ocena> oceny = new List<Ocena>();
        private int id = 1;
        private StudentService studentService;
        private PrzedmiotService przedmiotService;

        public OcenaService(StudentService ss, PrzedmiotService ps)
        {
            studentService = ss;
            przedmiotService = ps;

            // Dane testowe
            oceny.Add(new Ocena { Id = id++, StudentId = 1, PrzedmiotId = 1, Wartosc = 85 });
            oceny.Add(new Ocena { Id = id++, StudentId = 1, PrzedmiotId = 2, Wartosc = 90 });
        }

        public bool Dodawanie(int studentId, int przedmiotId, int wartosc)
        {
            if (studentService.Znajdowanie(studentId) == null) return false;
            if (przedmiotService.Znajdowanie(przedmiotId) == null) return false;

            oceny.Add(new Ocena { Id = id++, StudentId = studentId, PrzedmiotId = przedmiotId, Wartosc = wartosc });
            return true;
        }

        public List<Ocena> PobieranieWszystkich()
        {
            return oceny;
        }

        public List<Ocena> PobieranieOcenStudenta(int studentId)
        {
            return oceny.Where(o => o.StudentId == studentId).ToList();
        }

        public double ObliczanieSredniejStudenta(int studentId)
        {
            var ocenyStudenta = PobieranieOcenStudenta(studentId);
            if (ocenyStudenta.Count == 0) return 0;
            return ocenyStudenta.Average(o => o.Wartosc);
        }
    }
}
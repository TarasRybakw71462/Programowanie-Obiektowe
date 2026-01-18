using System;
using System.Collections.Generic;
using System.Linq;

namespace Projekt1
{
    public class PrzedmiotService
    {
        private List<Przedmiot> przedmioty = new List<Przedmiot>();
        private int id = 1;

        public PrzedmiotService()
        {
            przedmioty.Add(new Przedmiot { Id = id++, Nazwa = "Programowanie", Prowadzacy = "Dr Kowalski", Ects = 5 });
            przedmioty.Add(new Przedmiot { Id = id++, Nazwa = "Bazy danych", Prowadzacy = "Dr Nowak", Ects = 4 });
        }

        public void Dodawanie(string nazwa, string prowadzacy, int ects)
        {
            przedmioty.Add(new Przedmiot { Id = id++, Nazwa = nazwa, Prowadzacy = prowadzacy, Ects = ects });
        }

        public List<Przedmiot> PobieranieWszystkich()
        {
            return przedmioty;
        }

        public Przedmiot Znajdowanie(int id)
        {
            return przedmioty.FirstOrDefault(p => p.Id == id);
        }
    }
}
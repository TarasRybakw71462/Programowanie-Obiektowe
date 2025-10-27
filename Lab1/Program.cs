using System;

namespace Lab1
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("To jest cwiczenie 1");

            // Przykładowe użycie klasy Zwierze:
            Zwierze z1 = new Zwierze(); // konstruktor domyślny
            Zwierze z2 = new Zwierze("Mruczek", "Kot", 4); // konstruktor z parametrami
            Zwierze z3 = new Zwierze(z2); // konstruktor kopiujący

            z1.daj_glos();
            z2.daj_glos();
            z3.daj_glos();

            Console.WriteLine($"Liczba zwierząt: {Zwierze.PodajLiczbeZwierzat()}");
        }
    }

    public class Zwierze
    {
        // Pola prywatne
        private string nazwa;
        private string gatunek;
        private int liczbaNog;

        // Pole statyczne - licznik zwierząt
        private static int liczbaZwierzat = 0;

        // Gettery
        public string GetNazwa() => nazwa;
        public string GetGatunek() => gatunek;
        public int GetLiczbaNog() => liczbaNog;

        // Setter
        public void SetNazwa(string nowaNazwa)
        {
            nazwa = nowaNazwa;
        }

        // Konstruktor bezparametrowy
        public Zwierze()
        {
            nazwa = "Rex";
            gatunek = "Pies";
            liczbaNog = 4;
            liczbaZwierzat++;
        }

        // Konstruktor z trzema parametrami
        public Zwierze(string nazwa, string gatunek, int liczbaNog)
        {
            this.nazwa = nazwa;
            this.gatunek = gatunek;
            this.liczbaNog = liczbaNog;
            liczbaZwierzat++;
        }

        // Konstruktor kopiujący
        public Zwierze(Zwierze inne)
        {
            nazwa = inne.nazwa;
            gatunek = inne.gatunek;
            liczbaNog = inne.liczbaNog;
            liczbaZwierzat++;
        }

        // Metoda daj_glos()
        public void daj_glos()
        {
            switch (gatunek.ToLower())
            {
                case "pies":
                    Console.WriteLine($"{nazwa}: Hau hau!");
                    break;
                case "kot":
                    Console.WriteLine($"{nazwa}: Miau!");
                    break;
                case "krowa":
                    Console.WriteLine($"{nazwa}: Muuu!");
                    break;
                default:
                    Console.WriteLine($"{nazwa}: ... (nieznany dźwięk)");
                    break;
            }
        }

        // Metoda statyczna zwracająca liczbę zwierząt
        public static int PodajLiczbeZwierzat()
        {
            return liczbaZwierzat;
        }
    }
}


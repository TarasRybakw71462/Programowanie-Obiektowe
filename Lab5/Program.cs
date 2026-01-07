using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace PlikiCSharp
{
    public class Student
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public List<int> Oceny { get; set; }

        public Student()
        {
            Oceny = new List<int>();
        }
    }

    public class IrisRecord
    {
        public double SepalLength { get; set; }
        public double SepalWidth { get; set; }
        public double PetalLength { get; set; }
        public double PetalWidth { get; set; }
        public string Class { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Ćwiczenia z plikami w C# ===\n");

            while (true)
            {
                Console.WriteLine("\nWybierz zadanie (1-15, 0 - wyjście):");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Zadanie2();
                        break;
                    case "2":
                        Zadanie3();
                        break;
                    case "3":
                        Zadanie4();
                        break;
                    case "6":
                        Zadanie6();
                        break;
                    case "7":
                        Zadanie7();
                        break;
                    case "8":
                        Zadanie8();
                        break;
                    case "9":
                        Zadanie9();
                        break;
                    case "10":
                        Zadanie10();
                        break;
                    case "11":
                        Zadanie11();
                        break;
                    case "12":
                        Zadanie12();
                        break;
                    case "13":
                        Zadanie13();
                        break;
                    case "14":
                        Zadanie14();
                        break;
                    case "15":
                        Zadanie15();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór!");
                        break;
                }
            }
        }

        // Zadanie 2: Zapis linii do pliku
        static void Zadanie2()
        {
            Console.WriteLine("\n=== Zadanie 2 ===");
            Console.WriteLine("Wprowadź linie tekstu (wpisz 'koniec', aby zakończyć):");

            List<string> lines = new List<string>();

            while (true)
            {
                Console.Write("> ");
                string line = Console.ReadLine();

                if (line.ToLower() == "koniec")
                    break;

                lines.Add(line);
            }

            File.WriteAllLines("dane_uzytkownika.txt", lines);
            Console.WriteLine($"Zapisano {lines.Count} linii do pliku.");
        }

        // Zadanie 3: Odczyt linii z pliku
        static void Zadanie3()
        {
            Console.WriteLine("\n=== Zadanie 3 ===");

            if (!File.Exists("dane_uzytkownika.txt"))
            {
                Console.WriteLine("Plik nie istnieje!");
                return;
            }

            string[] lines = File.ReadAllLines("dane_uzytkownika.txt");

            Console.WriteLine($"Odczytano {lines.Length} linii:");
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {lines[i]}");
            }
        }

        // Zadanie 4: Dopisywanie linii do pliku
        static void Zadanie4()
        {
            Console.WriteLine("\n=== Zadanie 4 ===");
            Console.WriteLine("Wprowadź linie do dopisania (wpisz 'koniec', aby zakończyć):");

            List<string> newLines = new List<string>();

            while (true)
            {
                Console.Write("> ");
                string line = Console.ReadLine();

                if (line.ToLower() == "koniec")
                    break;

                newLines.Add(line);
            }

            File.AppendAllLines("dane_uzytkownika.txt", newLines);
            Console.WriteLine($"Dopisano {newLines.Count} linii do pliku.");
        }

        // Zadanie 6: Serializacja Studentów do JSON
        static void Zadanie6()
        {
            Console.WriteLine("\n=== Zadanie 6 ===");

            List<Student> students = new List<Student>
            {
                new Student { Imie = "Anna", Nazwisko = "Kowalska", Oceny = new List<int> { 5, 4, 3, 5 } },
                new Student { Imie = "Jan", Nazwisko = "Nowak", Oceny = new List<int> { 3, 4, 4, 5 } },
                new Student { Imie = "Piotr", Nazwisko = "Wiśniewski", Oceny = new List<int> { 5, 5, 4, 5 } }
            };

            string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("studenci.json", json);
            Console.WriteLine("Zapisano studentów do pliku JSON.");
        }

        // Zadanie 7: Deserializacja Studentów z JSON
        static void Zadanie7()
        {
            Console.WriteLine("\n=== Zadanie 7 ===");

            if (!File.Exists("studenci.json"))
            {
                Console.WriteLine("Plik JSON nie istnieje!");
                return;
            }

            string json = File.ReadAllText("studenci.json");
            List<Student> students = JsonSerializer.Deserialize<List<Student>>(json);

            Console.WriteLine("Lista studentów:");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Imie} {student.Nazwisko}");
                Console.WriteLine($"Oceny: {string.Join(", ", student.Oceny)}");
            }
        }

        // Zadanie 8: Serializacja Studentów do XML
        static void Zadanie8()
        {
            Console.WriteLine("\n=== Zadanie 8 ===");

            List<Student> students = new List<Student>
            {
                new Student { Imie = "Katarzyna", Nazwisko = "Lewandowska", Oceny = new List<int> { 4, 5, 4, 3 } },
                new Student { Imie = "Michał", Nazwisko = "Kamiński", Oceny = new List<int> { 5, 5, 5, 5 } },
                new Student { Imie = "Alicja", Nazwisko = "Zielińska", Oceny = new List<int> { 3, 4, 4, 4 } }
            };

            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
            using (StreamWriter writer = new StreamWriter("studenci.xml"))
            {
                serializer.Serialize(writer, students);
            }

            Console.WriteLine("Zapisano studentów do pliku XML.");
        }

        // Zadanie 9: Deserializacja Studentów z XML
        static void Zadanie9()
        {
            Console.WriteLine("\n=== Zadanie 9 ===");

            if (!File.Exists("studenci.xml"))
            {
                Console.WriteLine("Plik XML nie istnieje!");
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
            List<Student> students;

            using (StreamReader reader = new StreamReader("studenci.xml"))
            {
                students = (List<Student>)serializer.Deserialize(reader);
            }

            Console.WriteLine("Lista studentów z XML:");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Imie} {student.Nazwisko}");
                Console.WriteLine($"Oceny: {string.Join(", ", student.Oceny)}");
            }
        }

        // Zadanie 10: Odczyt pliku iris.csv
        static void Zadanie10()
        {
            Console.WriteLine("\n=== Zadanie 10 ===");

            if (!File.Exists("iris.csv"))
            {
                Console.WriteLine("Plik iris.csv nie istnieje!");
                return;
            }

            string[] lines = File.ReadAllLines("iris.csv");

            Console.WriteLine("Pierwsze 10 wierszy z pliku iris.csv:");
            for (int i = 0; i < Math.Min(10, lines.Length); i++)
            {
                Console.WriteLine(lines[i]);
            }
        }

        // Zadanie 11: Statystyki z iris.csv
        static void Zadanie11()
        {
            Console.WriteLine("\n=== Zadanie 11 ===");

            if (!File.Exists("iris.csv"))
            {
                Console.WriteLine("Plik iris.csv nie istnieje!");
                return;
            }

            List<IrisRecord> records = new List<IrisRecord>();
            string[] lines = File.ReadAllLines("iris.csv");

            // Pomijamy nagłówek
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length >= 5)
                {
                    records.Add(new IrisRecord
                    {
                        SepalLength = double.Parse(parts[0]),
                        SepalWidth = double.Parse(parts[1]),
                        PetalLength = double.Parse(parts[2]),
                        PetalWidth = double.Parse(parts[3]),
                        Class = parts[4]
                    });
                }
            }

            double avgSepalLength = records.Average(r => r.SepalLength);
            double avgSepalWidth = records.Average(r => r.SepalWidth);
            double avgPetalLength = records.Average(r => r.PetalLength);
            double avgPetalWidth = records.Average(r => r.PetalWidth);

            Console.WriteLine("Średnie wartości kolumn numerycznych:");
            Console.WriteLine($"sepal length: {avgSepalLength:F2}");
            Console.WriteLine($"sepal width:  {avgSepalWidth:F2}");
            Console.WriteLine($"petal length: {avgPetalLength:F2}");
            Console.WriteLine($"petal width:  {avgPetalWidth:F2}");
        }

        // Zadanie 12: Filtrowanie i zapis iris.csv
        static void Zadanie12()
        {
            Console.WriteLine("\n=== Zadanie 12 ===");

            if (!File.Exists("iris.csv"))
            {
                Console.WriteLine("Plik iris.csv nie istnieje!");
                return;
            }

            List<string> filteredLines = new List<string>();
            string[] lines = File.ReadAllLines("iris.csv");

            // Nagłówek
            filteredLines.Add("sepal length,sepal width,class");

            // Przetwarzanie danych
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length >= 5)
                {
                    double sepalLength = double.Parse(parts[0]);
                    if (sepalLength < 5.0)
                    {
                        filteredLines.Add($"{parts[0]},{parts[1]},{parts[4]}");
                    }
                }
            }

            File.WriteAllLines("iris_filtered.csv", filteredLines);
            Console.WriteLine($"Zapisano {filteredLines.Count - 1} rekordów do iris_filtered.csv");
        }

        // Zadanie 13: Informacje o formatach .ini i YAML
        static void Zadanie13()
        {
            Console.WriteLine("\n=== Zadanie 13 ===");
            Console.WriteLine("Format .ini:");
            Console.WriteLine("- Prosty format konfiguracyjny z sekcjami [Section]");
            Console.WriteLine("- Klucz=Wartość w każdej linii");
            Console.WriteLine("- Używany w Windows i prostych aplikacjach");
            Console.WriteLine("\nFormat YAML:");
            Console.WriteLine("- Czytelny format serializacji danych");
            Console.WriteLine("- Używa wcięć zamiast nawiasów");
            Console.WriteLine("- Obsługuje listy, słowniki, typy podstawowe");
            Console.WriteLine("- Popularny w konfiguracjach Docker, Kubernetes");
        }

        // Zadanie 14: Informacje o plikach binarnych
        static void Zadanie14()
        {
            Console.WriteLine("\n=== Zadanie 14 ===");
            Console.WriteLine("Pliki binarne w C#:");
            Console.WriteLine("- BinaryReader/BinaryWriter - do odczytu/zapisu typów podstawowych");
            Console.WriteLine("- FileStream z trybem FileMode - do operacji na bajtach");
            Console.WriteLine("- Image.FromFile() - do odczytu obrazów (System.Drawing)");
            Console.WriteLine("- Serializacja binarna - BinaryFormatter (przestarzały)");
            Console.WriteLine("- Wymagają znajomości struktury pliku");
        }

        // Zadanie 15: Informacje o Avro i Parquet
        static void Zadanie15()
        {
            Console.WriteLine("\n=== Zadanie 15 ===");
            Console.WriteLine("Apache Avro:");
            Console.WriteLine("- Format serializacji danych z kompresją");
            Console.WriteLine("- Schema w formacie JSON dołączony do danych");
            Console.WriteLine("- Wydajny dla big data, używany w Hadoop");
            Console.WriteLine("- Obsługuje RPC (Remote Procedure Call)");
            Console.WriteLine("\nApache Parquet:");
            Console.WriteLine("- Kolumnowy format przechowywania danych");
            Console.WriteLine("- Zoptymalizowany dla zapytań analitycznych");
            Console.WriteLine("- Skuteczna kompresja, metadane na końcu pliku");
            Console.WriteLine("- Używany w Spark, Hive, AWS Athena");
        }
    }
}
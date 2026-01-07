using Microsoft.Data.SqlClient;
using System.Data;

// Klasy modelujące dane
public class Student
{
    public int StudentId { get; set; }
    public string Imie { get; set; } = "";
    public string Nazwisko { get; set; } = "";
    public List<Ocena> Oceny { get; set; } = new();
}

public class Ocena
{
    public int OcenaId { get; set; }
    public double Wartosc { get; set; }
    public string Przedmiot { get; set; } = "";
    public int StudentId { get; set; }
}

public class Program
{
    public static void Main()
    {
        string connectionString =
            "Data Source=10.200.2.28;" +
            "Initial Catalog=studenci_71462;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True";

        try
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Połączono z bazą danych.");

            // Zadanie 4 - wyświetlanie studentów
            WyswietlStudentow(connection);
            Console.WriteLine();

            // Zadanie 5 - znajdź studenta po ID
            WyswietlStudentaPoId(connection, 1);
            Console.WriteLine();

            // Zadanie 6 - pobierz i wyświetl studentów z ocenami
            var studenci = PobierzStudentowZOcenami(connection);
            WyswietlStudentowZOcenami(studenci);
            Console.WriteLine();

            // Zadanie 7 - dodaj nowego studenta
            var nowyStudent = new Student
            {
                Imie = "Adam",
                Nazwisko = "Nowak"
            };
            DodajStudenta(connection, nowyStudent);
            Console.WriteLine();

            // Zadanie 8 - dodaj nową ocenę
            var nowaOcena = new Ocena
            {
                Wartosc = 4.5,
                Przedmiot = "Matematyka",
                StudentId = 1
            };
            if (DodajOcene(connection, nowaOcena))
            {
                Console.WriteLine("Dodano nową ocenę.");
            }
            Console.WriteLine();

            // Zadanie 9 - usuń oceny z Geografii
            UsunOcenyZPrzedmiotu(connection, "Geografia");
            Console.WriteLine();

            // Zadanie 10 - zaktualizuj ocenę
            if (AktualizujOcene(connection, 1, 5.0))
            {
                Console.WriteLine("Zaktualizowano ocenę.");
            }

            Console.WriteLine("\n=== Przykład dodatkowych funkcji ===");
            // Przykład SqlDataAdapter
            PrzykladDataAdapter(connection);

            Console.WriteLine("\n=== Test asynchroniczny ===");
            // Uruchom asynchroniczne połączenie
            PolaczenieAsynchroniczne().Wait();

            connection.Close();
        }
        catch (Exception exc)
        {
            Console.WriteLine("Wystąpił błąd: " + exc.Message);
        }
    }

    // Zadanie 4 - wyświetlanie studentów
    public static void WyswietlStudentow(SqlConnection connection)
    {
        Console.WriteLine("=== Zadanie 4: Lista studentów ===");
        string query = "SELECT student_id, imie, nazwisko FROM student";

        using SqlCommand command = new SqlCommand(query, connection);
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["student_id"]}, " +
                $"Imię: {reader["imie"]}, " +
                $"Nazwisko: {reader["nazwisko"]}");
        }
        reader.Close();
    }

    // Zadanie 5 - znajdź studenta po ID
    public static void WyswietlStudentaPoId(SqlConnection connection, int studentId)
    {
        Console.WriteLine($"=== Zadanie 5: Student o ID {studentId} ===");
        string query = "SELECT imie, nazwisko FROM student WHERE student_id = @studentId";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@studentId", studentId);

        using SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            Console.WriteLine($"Student: {reader["imie"]} {reader["nazwisko"]}");
        }
        else
        {
            Console.WriteLine($"Nie znaleziono studenta o ID: {studentId}");
        }
        reader.Close();
    }

    // Zadanie 6 - pobierz studentów z ocenami
    public static List<Student> PobierzStudentowZOcenami(SqlConnection connection)
    {
        Console.WriteLine("=== Zadanie 6: Pobieranie studentów z ocenami ===");
        var studenci = new List<Student>();

        string query = @"
            SELECT s.student_id, s.imie, s.nazwisko, 
                   o.ocena_id, o.wartosc, o.przedmiot, o.student_id as ocena_student_id
            FROM student s
            LEFT JOIN ocena o ON s.student_id = o.student_id
            ORDER BY s.student_id, o.ocena_id";

        using SqlCommand command = new SqlCommand(query, connection);
        using SqlDataReader reader = command.ExecuteReader();

        Student currentStudent = null;
        int lastStudentId = -1;

        while (reader.Read())
        {
            int studentId = Convert.ToInt32(reader["student_id"]);

            if (studentId != lastStudentId)
            {
                currentStudent = new Student
                {
                    StudentId = studentId,
                    Imie = reader["imie"].ToString(),
                    Nazwisko = reader["nazwisko"].ToString()
                };
                studenci.Add(currentStudent);
                lastStudentId = studentId;
            }

            // Dodaj ocenę jeśli istnieje
            if (!reader.IsDBNull(reader.GetOrdinal("ocena_id")))
            {
                var ocena = new Ocena
                {
                    OcenaId = Convert.ToInt32(reader["ocena_id"]),
                    Wartosc = Convert.ToDouble(reader["wartosc"]),
                    Przedmiot = reader["przedmiot"].ToString(),
                    StudentId = Convert.ToInt32(reader["ocena_student_id"])
                };
                currentStudent?.Oceny.Add(ocena);
            }
        }
        reader.Close();

        return studenci;
    }

    // Zadanie 6 - wyświetl studentów z ocenami
    public static void WyswietlStudentowZOcenami(List<Student> studenci)
    {
        Console.WriteLine("Lista studentów z ocenami:");
        foreach (var student in studenci)
        {
            Console.WriteLine($"\nStudent: {student.Imie} {student.Nazwisko}");

            if (student.Oceny.Count > 0)
            {
                Console.WriteLine("Oceny:");
                foreach (var ocena in student.Oceny)
                {
                    Console.WriteLine($"  {ocena.Przedmiot}: {ocena.Wartosc}");
                }
            }
            else
            {
                Console.WriteLine("  Brak ocen.");
            }
        }
    }

    // Zadanie 7 - dodaj nowego studenta
    public static void DodajStudenta(SqlConnection connection, Student student)
    {
        Console.WriteLine("=== Zadanie 7: Dodawanie nowego studenta ===");
        string query = "INSERT INTO student (imie, nazwisko) VALUES (@imie, @nazwisko)";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@imie", student.Imie);
        command.Parameters.AddWithValue("@nazwisko", student.Nazwisko);

        int rowsAffected = command.ExecuteNonQuery();
        Console.WriteLine($"Dodano {rowsAffected} studenta: {student.Imie} {student.Nazwisko}");
    }

    // Zadanie 8 - dodaj nową ocenę (z walidacją)
    public static bool DodajOcene(SqlConnection connection, Ocena ocena)
    {
        Console.WriteLine("=== Zadanie 8: Dodawanie nowej oceny ===");
        Console.WriteLine($"Próba dodania oceny: {ocena.Wartosc} z {ocena.Przedmiot} dla studenta ID: {ocena.StudentId}");

        // Walidacja oceny
        if (!CzyPoprawnaOcena(ocena.Wartosc))
        {
            Console.WriteLine("Niepoprawna wartość oceny!");
            Console.WriteLine("Dozwolone wartości: 2.0, 3.0, 3.5, 4.0, 4.5, 5.0");
            return false;
        }

        // Sprawdzenie czy student istnieje
        string checkQuery = "SELECT COUNT(*) FROM student WHERE student_id = @student_id";
        using SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
        checkCommand.Parameters.AddWithValue("@student_id", ocena.StudentId);

        int studentExists = (int)checkCommand.ExecuteScalar();
        if (studentExists == 0)
        {
            Console.WriteLine($"Student o ID {ocena.StudentId} nie istnieje!");
            return false;
        }

        string query = "INSERT INTO ocena (wartosc, przedmiot, student_id) " +
                      "VALUES (@wartosc, @przedmiot, @student_id)";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@wartosc", ocena.Wartosc);
        command.Parameters.AddWithValue("@przedmiot", ocena.Przedmiot);
        command.Parameters.AddWithValue("@student_id", ocena.StudentId);

        int rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    // Funkcja pomocnicza do walidacji oceny
    private static bool CzyPoprawnaOcena(double wartosc)
    {
        if (wartosc < 2 || wartosc > 5)
            return false;

        // Sprawdzenie czy wartość to 2, 2.5, 3, 3.5, 4, 4.5, 5
        // ale nie 2.5 (zgodnie z zadaniem)
        double[] dozwolone = { 2.0, 3.0, 3.5, 4.0, 4.5, 5.0 };
        return dozwolone.Contains(wartosc);
    }

    // Zadanie 9 - usuń oceny z przedmiotu
    public static void UsunOcenyZPrzedmiotu(SqlConnection connection, string przedmiot)
    {
        Console.WriteLine($"=== Zadanie 9: Usuwanie ocen z przedmiotu '{przedmiot}' ===");

        // Użycie transakcji dla bezpieczeństwa
        SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            // Najpierw sprawdź ile jest ocen do usunięcia
            string countQuery = "SELECT COUNT(*) FROM ocena WHERE przedmiot = @przedmiot";
            using SqlCommand countCommand = new SqlCommand(countQuery, connection, transaction);
            countCommand.Parameters.AddWithValue("@przedmiot", przedmiot);
            int liczbaOcen = (int)countCommand.ExecuteScalar();

            if (liczbaOcen == 0)
            {
                Console.WriteLine($"Brak ocen z przedmiotu '{przedmiot}' do usunięcia.");
                transaction.Commit();
                return;
            }

            string query = "DELETE FROM ocena WHERE przedmiot = @przedmiot";
            using SqlCommand command = new SqlCommand(query, connection, transaction);
            command.Parameters.AddWithValue("@przedmiot", przedmiot);

            int rowsAffected = command.ExecuteNonQuery();
            transaction.Commit();

            Console.WriteLine($"Usunięto {rowsAffected} ocen z przedmiotu '{przedmiot}'.");
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine($"Błąd podczas usuwania ocen: {ex.Message}");
        }
    }

    // Zadanie 10 - aktualizuj ocenę
    public static bool AktualizujOcene(SqlConnection connection, int ocenaId, double nowaWartosc)
    {
        Console.WriteLine($"=== Zadanie 10: Aktualizacja oceny o ID {ocenaId} na {nowaWartosc} ===");

        if (!CzyPoprawnaOcena(nowaWartosc))
        {
            Console.WriteLine("Niepoprawna wartość oceny!");
            return false;
        }

        // Sprawdź czy ocena istnieje
        string checkQuery = "SELECT COUNT(*) FROM ocena WHERE ocena_id = @ocena_id";
        using SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
        checkCommand.Parameters.AddWithValue("@ocena_id", ocenaId);

        int ocenaExists = (int)checkCommand.ExecuteScalar();
        if (ocenaExists == 0)
        {
            Console.WriteLine($"Ocena o ID {ocenaId} nie istnieje!");
            return false;
        }

        string query = "UPDATE ocena SET wartosc = @wartosc WHERE ocena_id = @ocena_id";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@wartosc", nowaWartosc);
        command.Parameters.AddWithValue("@ocena_id", ocenaId);

        int rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    // Przykład użycia SqlDataAdapter i DataSet
    public static void PrzykladDataAdapter(SqlConnection connection)
    {
        Console.WriteLine("Przykład SqlDataAdapter i DataSet:");

        string query = "SELECT * FROM student";
        using SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
        DataSet dataSet = new DataSet();

        adapter.Fill(dataSet, "Student");

        DataTable studentTable = dataSet.Tables["Student"];

        Console.WriteLine($"Liczba studentów w DataSet: {studentTable.Rows.Count}");
        Console.WriteLine("Kolumny: " + string.Join(", ",
            studentTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));

        foreach (DataRow row in studentTable.Rows)
        {
            Console.WriteLine($"ID: {row["student_id"]}, " +
                $"Imię: {row["imie"]}, " +
                $"Nazwisko: {row["nazwisko"]}");
        }
    }

    // Przykład asynchronicznego połączenia
    public static async Task PolaczenieAsynchroniczne()
    {
        string connectionString =
            "Data Source=10.200.2.28;" +
            "Initial Catalog=studenci_71462;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True";

        try
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string query = "SELECT COUNT(*) FROM student";
            using SqlCommand command = new SqlCommand(query, connection);

            int count = (int)await command.ExecuteScalarAsync();
            Console.WriteLine($"Liczba studentów (asynchronicznie): {count}");

            // Przykład asynchronicznego odczytu danych
            query = "SELECT imie, nazwisko FROM student";
            using SqlCommand command2 = new SqlCommand(query, connection);
            using SqlDataReader reader = await command2.ExecuteReaderAsync();

            Console.WriteLine("Studenci (asynchronicznie):");
            while (await reader.ReadAsync())
            {
                Console.WriteLine($"- {reader["imie"]} {reader["nazwisko"]}");
            }

            await connection.CloseAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd w połączeniu asynchronicznym: {ex.Message}");
        }
    }

    // Dodatkowa funkcja: Przykład transakcji
    public static void PrzykladTransakcji(SqlConnection connection)
    {
        Console.WriteLine("\n=== Przykład transakcji ===");

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            // Pierwsze polecenie - dodanie studenta
            using SqlCommand command1 = new SqlCommand(
                "INSERT INTO student (imie, nazwisko) VALUES (@imie1, @nazwisko1)",
                connection,
                transaction);
            command1.Parameters.AddWithValue("@imie1", "Tomasz");
            command1.Parameters.AddWithValue("@nazwisko1", "Kowalski");
            command1.ExecuteNonQuery();
            Console.WriteLine("Dodano studenta Tomasz Kowalski");

            // Drugie polecenie - dodanie oceny dla nowego studenta
            using SqlCommand command2 = new SqlCommand(
                "INSERT INTO ocena (wartosc, przedmiot, student_id) VALUES (4.0, 'Informatyka', SCOPE_IDENTITY())",
                connection,
                transaction);
            command2.ExecuteNonQuery();
            Console.WriteLine("Dodano ocenę z Informatyki");

            // Jeśli wszystko OK - commit
            transaction.Commit();
            Console.WriteLine("Transakcja zakończona pomyślnie.");
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine($"Błąd: {ex.Message}. Transakcja wycofana.");
        }
    }

    // Funkcja demonstrująca SQL Injection i jak go uniknąć
    public static void DemonstracjaSQLInjection(SqlConnection connection)
    {
        Console.WriteLine("\n=== Demonstracja SQL Injection ===");

        // ZŁY PRZYKŁAD - podatny na SQL Injection
        Console.WriteLine("\nZŁY PRZYKŁAD (NIEBEZPIECZNY):");
        string niebezpieczneImie = "Jan'; DROP TABLE student; --";
        string zleZapytanie = $"SELECT * FROM student WHERE imie = '{niebezpieczneImie}'";
        Console.WriteLine($"To zapytanie jest niebezpieczne: {zleZapytanie}");

        // DOBRY PRZYKŁAD - używanie parametrów
        Console.WriteLine("\nDOBRY PRZYKŁAD (BEZPIECZNY):");
        string bezpieczneZapytanie = "SELECT * FROM student WHERE imie = @imie";
        using SqlCommand bezpieczneCommand = new SqlCommand(bezpieczneZapytanie, connection);
        bezpieczneCommand.Parameters.AddWithValue("@imie", niebezpieczneImie);
        Console.WriteLine("To zapytanie jest bezpieczne dzięki parametrom");
    }
}
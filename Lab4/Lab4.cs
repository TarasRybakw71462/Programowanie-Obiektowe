using System;
using System.Collections.Generic;
using System.Linq;

public interface IModular
{
    public double Module();
}

// 1. Rozszerzona klasa ComplexNumber z implementacją IComparable
public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular, IComparable<ComplexNumber>
{
    private double re;
    private double im;

    // Właściwości
    public double Re { get => re; set => re = value; }
    public double Im { get => im; set => im = value; }

    // Konstruktor
    public ComplexNumber(double re, double im)
    {
        this.re = re;
        this.im = im;
    }

    // Metody
    public override string ToString()
    {
        string sign = im >= 0 ? "+" : "-";
        return $"{re} {sign} {Math.Abs(im)}i";
    }

    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        => new ComplexNumber(a.re + b.re, a.im + b.im);

    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        => new ComplexNumber(a.re - b.re, a.im - b.im);

    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        => new ComplexNumber(a.re * b.re - a.im * b.im, a.re * b.im + a.im * b.re);

    public static ComplexNumber operator -(ComplexNumber a)
        => new ComplexNumber(a.re, -a.im);

    public object Clone() => new ComplexNumber(re, im);

    public bool Equals(ComplexNumber other)
    {
        if (other == null) return false;
        return re == other.re && im == other.im;
    }

    public override bool Equals(object obj)
        => obj is ComplexNumber other && Equals(other);

    public override int GetHashCode()
        => HashCode.Combine(re, im);

    public static bool operator ==(ComplexNumber a, ComplexNumber b)
        => a?.Equals(b) ?? b is null;

    public static bool operator !=(ComplexNumber a, ComplexNumber b)
        => !(a == b);

    public double Module()
        => Math.Sqrt(re * re + im * im);

    public int CompareTo(ComplexNumber other)
    {
        if (other == null) return 1; // Jeśli drugi obiekt jest nul bieżący jest większy

        // Porównujemy moduły (Module())
        return this.Module().CompareTo(other.Module());
    }
}

// 4. i 5. Klasa Program do demonstracji operacji
public class Program
{
    public static void Main(string[] args)
    {
        // Ustawienia konsoli
        Console.OutputEncoding = System.Text.Encoding.UTF8;

 

        ComplexNumber cz1 = new ComplexNumber(6.0, 7.0);
        ComplexNumber cz2 = new ComplexNumber(1.0, 2.0);
        ComplexNumber cz3 = new ComplexNumber(6.0, 7.0);
        ComplexNumber cz4 = new ComplexNumber(1.0, -2.0);
        ComplexNumber cz5 = new ComplexNumber(-5.0, 9.0);
        ComplexNumber cz6 = new ComplexNumber(0.0, 1.0);
        ComplexNumber cz7 = new ComplexNumber(10.0, 0.0);

        Console.WriteLine("\n--- 2. Operacje na tablicy ComplexNumber ---");

        // 2. Definicja tablicy
        ComplexNumber[] complexArray = new ComplexNumber[] { cz1, cz4, cz5, cz2, cz6 };

        // 2a. Wypisanie tablicy
        Console.WriteLine("\n2a. Tablica przed sortowaniem:");
        foreach (var z in complexArray)
        {
            Console.WriteLine($"\t{z} (Moduł: {z.Module():F2})");
        }

        // 2b. Sortowanie i ponowne wypisanie
        Array.Sort(complexArray);
        Console.WriteLine("\n2b. Tablica po sortowaniu (wg modułu):");
        foreach (var z in complexArray)
        {
            Console.WriteLine($"\t{z} (Moduł: {z.Module():F2})");
        }

        // 2c. Minimum i maksimum
        Console.WriteLine("\n2c. Minimum i maksimum (wg modułu):");
        var minElement = complexArray.Min();
        var maxElement = complexArray.Max();
        Console.WriteLine($"\tMinimum: {minElement} (Moduł: {minElement.Module():F2})");
        Console.WriteLine($"\tMaksimum: {maxElement} (Moduł: {maxElement.Module():F2})");

        // 2d. Odfiłtrowanie
        var filteredArray = complexArray.Where(z => z.Im >= 0).ToArray();
        Console.WriteLine("\n2d. Odfiltrowane (tylko z Im >= 0):");
        foreach (var z in filteredArray)
        {
            Console.WriteLine($"\t{z}");
        }

        // 3
        Console.WriteLine("\n" + new string('-', 40));
        Console.WriteLine("--- 3. Operacje na liście List<ComplexNumber> ---");

        // 3 Definicja listy
        List<ComplexNumber> complexList = new List<ComplexNumber> { cz1, cz4, cz5, cz2, cz6, cz7 };
        Console.WriteLine("\nPoczątkowa lista:");
        complexList.ForEach(z => Console.WriteLine($"\t{z} (Moduł: {z.Module():F2})"));

        // Sprawdzenie
        // 3b Sortowanie
        complexList.Sort(); // Metoda Sort() listy używa IComparable
        Console.WriteLine("\nLista po sortowaniu (wg modułu):");
        complexList.ForEach(z => Console.WriteLine($"\t{z} (Moduł: {z.Module():F2})"));

        // Minimum i maksimum
        Console.WriteLine("\nMinimum i maksimum (List):");
        Console.WriteLine($"\tMinimum: {complexList.Min()}");
        Console.WriteLine($"\tMaksimum: {complexList.Max()}");

        // Odfiltrowanie
        var filteredList = complexList.Where(z => z.Im < 0).ToList();
        Console.WriteLine("\nOdfiltrowane (tylko z Im < 0):");
        filteredList.ForEach(z => Console.WriteLine($"\t{z}"));

        // 3a
        if (complexList.Count > 1)
        {
            complexList.RemoveAt(1); // Indeks 1 to drugi element
            Console.WriteLine("\n3a. Lista po usunięciu drugiego elementu:");
            complexList.ForEach(z => Console.WriteLine($"\t{z}"));
        }

        // 3b
        var minListElement = complexList.Min();
        complexList.Remove(minListElement);
        Console.WriteLine("\n3b. Lista po usunięciu najmniejszego elementu (Moduł):");
        complexList.ForEach(z => Console.WriteLine($"\t{z}"));

        // 3c
        complexList.Clear();
        Console.WriteLine($"\n3c. Lista po wyczyszczeniu. Liczba elementów: {complexList.Count}");

        // 4
        Console.WriteLine("\n" + new string('-', 40));
        Console.WriteLine("--- 4. Operacje na zbiorze HashSet<ComplexNumber> ---");

        // 4. Definicja zbioru
        HashSet<ComplexNumber> complexSet = new HashSet<ComplexNumber> { cz1, cz2, cz3, cz4, cz5 };

        // 4a. Sprawdzenie zawartości zbioru
        Console.WriteLine("\n4a. Zawartość zbioru (dzięki GetHashCode/Equals, z1 i z3 traktowane są jako jeden element):");
        foreach (var z in complexSet)
        {
            Console.WriteLine($"\t{z} (Moduł: {z.Module():F2})");
        }

        // 4b
        Console.WriteLine("\n4b. Test operacji na HashSet (wymaga użycia LINQ, ponieważ HashSet nie jest indeksowany/sortowalny):");

        // Minimum i Maksimum 
        Console.WriteLine($"\tMinimum: {complexSet.Min()} (Moduł: {complexSet.Min().Module():F2})");
        Console.WriteLine($"\tMaksimum: {complexSet.Max()} (Moduł: {complexSet.Max().Module():F2})");

        // Sortowanie
        var sortedSet = complexSet.OrderBy(z => z.Module()).ToList();
        Console.WriteLine("\tPosortowane (przekształcone do Listy za pomocą LINQ OrderBy):");
        sortedSet.ForEach(z => Console.WriteLine($"\t\t{z} (Moduł: {z.Module():F2})"));

        // Filtrowanie 
        var filteredSet = complexSet.Where(z => z.Im < 0).ToList();
        Console.WriteLine("\tOdfiltrowane (Im < 0):");
        filteredSet.ForEach(z => Console.WriteLine($"\t\t{z}"));

        //  5
        Console.WriteLine("\n" + new string('-', 40));
        Console.WriteLine("--- 5. Operacje na słowniku Dictionary<string, ComplexNumber> ---");

        // 5. Definicja slownika
        Dictionary<string, ComplexNumber> complexDict = new Dictionary<string, ComplexNumber>()
        {
            { "z1", cz1 },
            { "z2", cz2 },
            { "z3", cz3 },
            { "z4", cz4 },
            { "z5", cz5 }
        };

        // 5a
        Console.WriteLine("\n5a. Elementy słownika (Klucz, Wartość):");
        foreach (var kvp in complexDict)
        {
            Console.WriteLine($"\t({kvp.Key}, {kvp.Value})");
        }

        // 5b
        Console.WriteLine("\n5b. Klucze:");
        foreach (var key in complexDict.Keys)
        {
            Console.Write($" {key},");
        }
        Console.WriteLine("\nWartości:");
        foreach (var value in complexDict.Values)
        {
            Console.Write($" {value},");
        }
        Console.WriteLine();

        // 5c
        bool containsZ6 = complexDict.ContainsKey("z6");
        Console.WriteLine($"\n5c. Czy istnieje klucz 'z6'?: {containsZ6}");

        // 5d
        Console.WriteLine("\n5d. Operacje na wartościach słownika (za pomocą LINQ):");

        // Minimum i maksimum (2c)
        var dictMin = complexDict.Values.Min();
        var dictMax = complexDict.Values.Max();
        Console.WriteLine($"\tMinimum (wg modułu): {dictMin}");
        Console.WriteLine($"\tMaksimum (wg modułu): {dictMax}");

        // Filtrowanie (2d)
        var dictFiltered = complexDict.Where(kvp => kvp.Value.Im < 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        Console.WriteLine("\tOdfiltrowane (Im < 0):");
        foreach (var kvp in dictFiltered)
        {
            Console.WriteLine($"\t\t{kvp.Key}: {kvp.Value}");
        }

        // 5e
        complexDict.Remove("z3");
        Console.WriteLine($"\n5e. Słownik po usunięciu klucza 'z3'. Liczba elementów: {complexDict.Count}");

        // 5f
        var keyToRemove = complexDict.Keys.Skip(1).FirstOrDefault(); 
        if (keyToRemove != null)
        {
            complexDict.Remove(keyToRemove);
            Console.WriteLine($"\n5f. Słownik po usunięciu klucza '{keyToRemove}' (był drugim kluczem). Liczba elementów: {complexDict.Count}");
        }

        // 5g
        complexDict.Clear();
        Console.WriteLine($"\n5g. Słownik po wyczyszczeniu. Liczba elementów: {complexDict.Count}");

        
    }
}

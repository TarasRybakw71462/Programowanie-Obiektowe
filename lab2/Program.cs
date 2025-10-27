using System;

// 1. Klasa bazowa Zwierze
public class Zwierze
{
    
    protected string nazwa;

    public Zwierze(string nazwa)
    {
        this.nazwa = nazwa;
    }

    public virtual void daj_glos()
    {
        Console.WriteLine("...");
    }
}

// 2. Klasa Pies dziedzicząca po Zwierze
public class Pies : Zwierze
{
    public Pies(string nazwa) : base(nazwa) { }

    public override void daj_glos()
    {
        Console.WriteLine($"{nazwa} robi woof woof!");
    }
}

// 3. Klasa Kot dziedzicząca po Zwierze
public class Kot : Zwierze
{
    public Kot(string nazwa) : base(nazwa) { }

    public override void daj_glos()
    {
        Console.WriteLine($"{nazwa} robi miau miau!");
    }
}

// 4. 
public class Waz : Zwierze
{
    public Waz(string nazwa) : base(nazwa) { }

    public override void daj_glos()
    {
        Console.WriteLine($"{nazwa} robi ssssssss!");
    }
}

// 6. Globalna metoda powiedz_cos()
public static class Program
{
    public static void powiedz_cos(Zwierze zwierze)
    {
        zwierze.daj_glos();
    }

    public static void Main()
    {
        // Przykładowe użycie:
        Pies pies = new Pies("Reksio");
        Kot kot = new Kot("Filemon");
        Waz waz = new Waz("Python");

        powiedz_cos(pies);
        powiedz_cos(kot);
        powiedz_cos(waz);
    }
}

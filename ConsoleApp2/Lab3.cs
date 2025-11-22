using System;
public interface IModular
{
    double Module();
}

public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular
{
    // Prywatne pola
    private double re; // część rzeczywista
    private double im; // część urojona

    // 1. Publiczne właściwości (Re oraz Im)
    public double Re
    {
        get { return re; }
        set { re = value; }
    }

    public double Im
    {
        get { return im; }
        set { im = value; }
    }

    // Konstruktor
    public ComplexNumber(double re, double im)
    {
        this.re = re;
        this.im = im;
    }

    public override string ToString()
    {
        if (im == 0)
            return re.ToString();
        else if (re == 0)
            return im + "i";
        else if (im > 0)
            return re + " + " + im + "i";
        else 
            return re + " - " + Math.Abs(im) + "i";
    }


    // (+)
    public static ComplexNumber operator +(ComplexNumber z1, ComplexNumber z2)
    {
        return new ComplexNumber(z1.Re + z2.Re, z1.Im + z2.Im);
    }

    // (-)
    public static ComplexNumber operator -(ComplexNumber z1, ComplexNumber z2)
    {
        return new ComplexNumber(z1.Re - z2.Re, z1.Im - z2.Im);
    }

    //  (*)
    // (a+bi)(c+di)=(ac−bd)+(ad+bc)i
    public static ComplexNumber operator *(ComplexNumber z1, ComplexNumber z2)
    {
        double newRe = (z1.Re * z2.Re) - (z1.Im * z2.Im);
        double newIm = (z1.Re * z2.Im) + (z1.Im * z2.Re);
        return new ComplexNumber(newRe, newIm);
    }

    // Unarny operator (-) dla sprzęrzenia licсzby zespolonej: −(a+bi)=a−bi
    public static ComplexNumber operator -(ComplexNumber z)
    {
        return new ComplexNumber(z.Re, -z.Im);
    }

    public object Clone()
    {
        return new ComplexNumber(this.Re, this.Im);
    }

    public bool Equals(ComplexNumber other)
    {
        if (ReferenceEquals(other, null)) return false;
        // Dwie liczby są równe, jeśli mają równe obie częszci
        return this.Re == other.Re && this.Im == other.Im;
    }

    // Przeciążenie ogólnej metody Equals(object)
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(obj, null)) return false;
        if (obj is ComplexNumber other)
        {
            return Equals(other);
        }
        return false;
    }

    // Przeciążenie GetHashCode()
    public override int GetHashCode()
    {
        return this.Re.GetHashCode() ^ this.Im.GetHashCode();
    }

    //  (== oraz !=)
    public static bool operator ==(ComplexNumber z1, ComplexNumber z2)
    {
        if (ReferenceEquals(z1, null))
        {
            return ReferenceEquals(z2, null);
        }
        return z1.Equals(z2);
    }

    public static bool operator !=(ComplexNumber z1, ComplexNumber z2)
    {
        return !(z1 == z2);
    }

    // 3. 
    public double Module()
    {
        // Wskazówka: |Z|=$\sqrt{R e^2 + I m^2}$
        return Math.Sqrt(this.Re * this.Re + this.Im * this.Im);
    }
}

// 4. Klasa Program z metodą Main()
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- Testowanie Klasy ComplexNumber ---");
        Console.WriteLine("--------------------------------------");

        // Tworzenie liczb zespolonych
        ComplexNumber z1 = new ComplexNumber(3.0, 4.0);
        ComplexNumber z2 = new ComplexNumber(1.0, -2.0);
        ComplexNumber z3 = new ComplexNumber(3.0, 4.0);
        ComplexNumber z4 = new ComplexNumber(0.0, 5.5); 

        Console.WriteLine($"Liczby: Z1 = {z1}, Z2 = {z2}, Z3 = {z3}, Z4 = {z4}\n");

        // Test operatorów binarnych
        ComplexNumber sum = z1 + z2;
        ComplexNumber diff = z1 - z2;
        ComplexNumber product = z1 * z2;

        Console.WriteLine($"Dodawanie: {z1} + {z2} = {sum}");
        Console.WriteLine($"Odejmowanie: {z1} - {z2} = {diff}");
        Console.WriteLine($"Mnożenie: {z1} * {z2} = {product}\n");

        // Test operatora unarnego (sprzężenie)
        ComplexNumber conjugate_z1 = -z1;
        Console.WriteLine($"Sprzężenie: -({z1}) = {conjugate_z1}\n");

        // Test modułu (IModular)
        double mod1 = z1.Module();
        Console.WriteLine($"Moduł |Z1| = {mod1}"); 
        Console.WriteLine($"Moduł |Z4| = {z4.Module()}\n"); 

        // Test porównania y
        Console.WriteLine($"Porównanie Z1 == Z3: {z1 == z3}");   
        Console.WriteLine($"Porównanie Z1 != Z2: {z1 != z2}");   
        Console.WriteLine($"Z1.Equals(Z3): {z1.Equals(z3)}\n"); 

        // Test klonowania
        ComplexNumber z1_clone = (ComplexNumber)z1.Clone();
        z1_clone.Re = 100.0;
        Console.WriteLine($"Oryginał Z1: {z1}");
        Console.WriteLine($"Sklonowany Z1 po zmianie: {z1_clone}");
        Console.WriteLine("\n--- Koniec Testów ---");
    }
}

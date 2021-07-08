using System;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;


namespace ConsoleApp6
{
    class MyAccessModifiers
    {
        private int birthYear;
        protected string personalInfo;
        private protected string IdNumber;
        public static byte averageMiddleAge = 50;
        internal string Name { get; set; }
        public string NickName { get; internal set; }
        public int Age => DateTime.Now.Year - birthYear;

        public MyAccessModifiers(int birthYear, string idNumber, string personalInfo)
        {
            this.birthYear = birthYear;
            IdNumber = idNumber;
            this.personalInfo = personalInfo;


        }

        protected internal bool HasLivedHalfOfLife()
        {
            return Age >= 50;
        }

        public static bool operator ==(MyAccessModifiers obj1, MyAccessModifiers obj2)
        {
            if ((obj1.Name == obj2.Name) && (obj1.Age == obj2.Age) && (obj1.personalInfo == obj2.personalInfo))
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(MyAccessModifiers obj1, MyAccessModifiers obj2)
        {
            return !(obj1 == obj2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var second = obj as MyAccessModifiers;

            if (second == null)
            {
                return false;
            }

            if ((this.Name == second.Name) && (this.Age == second.Age) && (this.personalInfo == second.personalInfo))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class Point
    {
        private int x;
        private int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        internal int[] GetXYPair()
        {
            return new int[] {this.x, this.y};
        }

        protected internal double Distance(int x, int y)
        {
            return Math.Sqrt(((this.x - x) * (this.x - x) + (this.y - y) * (this.y - y)));
        }

        protected internal double Distance(Point point)
        {
            return Math.Sqrt(((this.x - point.x) * (this.x - point.x) + (this.y - point.y) * (this.y - point.y)));
        }

        public static explicit operator double(Point point)
        {
            return Math.Sqrt(((0 - point.x) * (0 - point.x) + (0 - point.y) * (0 - point.y)));
        }
    }

    public class Fraction
    {
        private readonly int numerator;
        private readonly int denominator;

        public Fraction(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public static Fraction operator -(Fraction obj1, Fraction obj2)
        {
            return obj1 + (-obj2);
        }
        public static Fraction operator -(Fraction obj1)
        {
            return new Fraction(-obj1.numerator, obj1.denominator);
        }
        public static Fraction operator +(Fraction obj1)
        {
            return new Fraction(obj1.numerator, obj1.denominator);
        }

        public static Fraction operator +(Fraction obj1, Fraction obj2)
        {

            var minDenominator = MinDenominator(obj1, obj2);
            return new Fraction(obj1.numerator * minDenominator / obj1.denominator + obj2.numerator * minDenominator / obj2.denominator, minDenominator);
        }

        public static Fraction operator !(Fraction obj)
        {
            return new Fraction(obj.denominator, obj.numerator);
        }

        public static Fraction operator *(Fraction obj1, Fraction obj2)
        {
            return new Fraction(obj1.numerator * obj2.numerator, obj1.denominator * obj2.denominator);
        }
        public static Fraction operator /(Fraction obj1, Fraction obj2)
        {
            return obj1*!obj2;
        }
        private static int MinDenominator(Fraction obj1, Fraction obj2)
        {
            for (int i = 2; ; i++)
            {
                if (i % obj1.denominator == 0 && i % obj2.denominator == 0)
                {
                    return i;
                }
            }
        }
        private Fraction SimplifyFraction()
        {
            int minFactor = numerator > denominator ? denominator : numerator;
            minFactor = Math.Abs(minFactor);
            if (numerator < 0 && denominator < 0)
            {

              return  new Fraction(-numerator, -denominator).SimplifyFraction();
            }
            if (numerator > 0 && denominator < 0)
            {

                return new Fraction(-numerator, -denominator).SimplifyFraction();
            }
            for (int i = minFactor; i > 2; i--)
            {
                if (numerator % i == 0 && denominator % i == 0)
                {
                    return new Fraction(this.numerator / i, this.denominator / i);
                }
                
            }

            return this;
        }
        
        public override string ToString()
        {
            var simple = SimplifyFraction();
            return $"{simple.numerator} / {simple.denominator}";
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;

            }

            var obj2 = obj as Fraction;
            if (obj2 is null)
            {
                return false;
            }

            var firstFraction = SimplifyFraction();
            var secondFraction = obj2.SimplifyFraction();
            if (firstFraction.numerator==secondFraction.numerator
                && firstFraction.denominator == secondFraction.denominator)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Fraction obj1, Fraction obj2)
        {
            if (!(obj1 is { }) || !(obj2 is { }))
            {
                return false;
            }

            var first = obj1.SimplifyFraction();
            var second = obj2.SimplifyFraction();
            if ((first.numerator == second.numerator) && (first.denominator == second.denominator))
            {
                return true;
            }

            return false;
        }
        public static bool operator !=(Fraction obj1,Fraction obj2)
        {
            return !(obj1 == obj2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public class Person
    { protected readonly int yearOfBirth;
        protected readonly string name;
        protected readonly string healthInfo;

        public string GetHealthStatus()
        {
            return name + ": " + yearOfBirth + ". " + healthInfo;
        }

        public Person(int yearOfBirth, string name, string healthInfo)
        {
            this.yearOfBirth = yearOfBirth;
            this.name = name;
            this.healthInfo = healthInfo;
        }
    }

    public class Child:Person
    {
        private readonly string childIDNumber;

        public Child(int yearOfBirth, string name, string healthInfo, string childIdNumber) : base(yearOfBirth, name, healthInfo)
        {
            childIDNumber = childIdNumber;
        }

        public override string ToString()
        {
            return $"{name} {childIDNumber}";
        }
    }

    public class Adult:Person
    {
       
        private readonly string passportNumber;

        public Adult(int yearOfBirth, string name, string healthInfo, string passportNumber) : base(yearOfBirth, name, healthInfo)
        {
            this.passportNumber = passportNumber;
        }

        public override string ToString()
        {
            return $"{name} {passportNumber}";
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            Fraction f = new Fraction(5, 25);
            Fraction f1 = new Fraction(1, 5);
            Console.WriteLine(f==f1);
           
        }
    }
}

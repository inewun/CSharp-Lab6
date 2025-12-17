using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace Laboratory6.Fraction
{
    public class Fraction : ICloneable, IFraction
    {
        private int _numerator;
        private int _denominator;

        private double? _valueCache;
        private bool _isDirty;

        public int Numerator
        {
            get 
            { 
                return _numerator; 
            } 
            set 
            { 
                _numerator = value; 
            }
        }

        public int Denominator
        {
            get
            {
                return _denominator;
            }
            private set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Знаменатель не может быть нулевым.", nameof(Denominator));
                }

                if (value < 0)
                {
                    _numerator = -_numerator;
                    value = -value;
                }
                
                
                _denominator = value;
                _isDirty = true;
            }
        }

        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
            NormalizeSign();
            Reduce();
        }

        public Fraction(int integer) : this(integer, 1) { }

        private void NormalizeSign()
        {
            if (Denominator < 0)
            {
                Denominator = -Denominator;
                Numerator = -Numerator;
            }
        }

        private void Reduce()
        {
            int gcd = GCD(Numerator, Denominator);
            Numerator /= gcd;
            Denominator /= gcd;
        }

        private static int GCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            return new Fraction
            (
                a.Numerator * b.Denominator + b.Numerator * a.Denominator,
                a.Denominator * b.Denominator
            );
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            return new Fraction
            (
                a.Numerator * b.Denominator - b.Numerator * a.Denominator,
                a.Denominator * b.Denominator
            );
        }

        public static Fraction operator -(Fraction a, int value)
        {
            return a - new Fraction(value);
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction
            (
                a.Numerator * b.Numerator,
                a.Denominator * b.Denominator
            );
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.Numerator == 0)
            {
                throw new DivideByZeroException("Деление на нулевую дробь.");
            }

            return new Fraction
            (
                a.Numerator * b.Denominator,
                a.Denominator * b.Numerator
            );
        } 

        public static implicit operator Fraction(int value)
        {
            return new Fraction(value);
        }

        public Fraction Sum(Fraction other)
        {
            return this + other;
        }

        public Fraction Minus(Fraction other)
        {
            return this - other;
        }

        public Fraction Minus(int value)
        {
            return this - value; 
        }

        public Fraction Mult(Fraction other)
        {
            return this * other;
        }

        public Fraction Div(Fraction other)
        {
            return this / other;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Fraction other)
            {
                return false;
            }

            return Numerator == other.Numerator && Denominator == other.Denominator;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        public int CompareTo(Fraction other)
        {
            long left = (long)Numerator * other.Denominator;
            long right = (long)other.Numerator * Denominator;
            return left.CompareTo(right);
        }

        public static bool operator ==(Fraction a, Fraction b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }

        public object Clone()
        {
            return new Fraction(Numerator, Denominator);
        }

        public double ToDouble()
        {
            if (_isDirty || !_valueCache.HasValue)
            {
                _valueCache = (double)_numerator / _denominator;
                _isDirty = false;
            }
            return _valueCache.Value;
        }

        public void SetNumerator(int value)
        {
            Numerator = value;
            Reduce();
        }
        
        public void SetDenominator(int value)
        {
            Denominator = value;
            NormalizeSign();
            Reduce();
        }

    }
}

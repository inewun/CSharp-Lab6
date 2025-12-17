using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory6.Fraction
{
    internal interface IFraction
    {
        double ToDouble();
        void SetNumerator(int value);
        void SetDenominator(int value);
    }
}

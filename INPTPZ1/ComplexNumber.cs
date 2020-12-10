using System;

namespace INPTPZ1
{
    public class ComplexNumber
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            Real = 0,
            Imaginary = 0
        };

        //public override bool Equals(object input)
        //{
        //    if (input is ComplexNumber)
        //    {
        //        ComplexNumber complexNumber = input as ComplexNumber;
        //        return Math.Pow(Real - complexNumber.Real, Exponent) +
        //                Math.Pow(Imaginary - complexNumber.Imaginary, Exponent) <= RootToleration;
        //    }
        //    return base.Equals(input);
        //}

        public ComplexNumber Multiply(ComplexNumber inputNumber)
        {
            return new ComplexNumber()
            {
                Real = (Real * inputNumber.Real) - (Imaginary * inputNumber.Imaginary),
                Imaginary = (Real * inputNumber.Imaginary) + (Imaginary * inputNumber.Real)
            };
        }
        //public double GetAbS()
        //{
        //    return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        //}

        public ComplexNumber Add(ComplexNumber inputNumber)
        {
            return new ComplexNumber()
            {
                Real = Real + inputNumber.Real,
                Imaginary = Imaginary + inputNumber.Imaginary
            };
        }
        ////public double GetAngleInDegrees()
        ////{
        ////    return Math.Atan(Imaginary / Real);
        ////}
        public ComplexNumber Subtract(ComplexNumber inputNumber)
        {
            return new ComplexNumber()
            {
                Real = Real - inputNumber.Real,
                Imaginary = Imaginary - inputNumber.Imaginary
            };
        }

        internal ComplexNumber Divide(ComplexNumber inputNumber)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            //  bRe*bRe - bIm*bIm*i*i
            var dividend = Multiply(new ComplexNumber() { Real = inputNumber.Real, Imaginary = -inputNumber.Imaginary });
            var divisor = (inputNumber.Real * inputNumber.Real) + (inputNumber.Imaginary * inputNumber.Imaginary);

            return new ComplexNumber()
            {
                Real = dividend.Real / divisor,
                Imaginary = dividend.Imaginary / divisor
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }
    }
}


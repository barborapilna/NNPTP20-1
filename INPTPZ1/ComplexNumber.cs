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

        public ComplexNumber Multiply(ComplexNumber inputNumber)
        {
            return new ComplexNumber()
            {
                Real = (Real * inputNumber.Real) - (Imaginary * inputNumber.Imaginary),
                Imaginary = (Real * inputNumber.Imaginary) + (Imaginary * inputNumber.Real)
            };
        }

        public ComplexNumber Add(ComplexNumber inputNumber)
        {
            return new ComplexNumber()
            {
                Real = Real + inputNumber.Real,
                Imaginary = Imaginary + inputNumber.Imaginary
            };
        }

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


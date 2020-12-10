using System.Collections.Generic;

namespace INPTPZ1
{
    class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public Polynomial Derive()
        {
            Polynomial polynomial = new Polynomial();

            for (int i = 1; i < Coefficients.Count; i++)
            {
                polynomial.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { Real = i }));
            }

            return polynomial;
        }

        public ComplexNumber Evaluate(ComplexNumber input)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber evaluation = input;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        evaluation = evaluation.Multiply(input);

                    coefficient = coefficient.Multiply(evaluation);
                }
                result = result.Add(coefficient);
            }
            return result;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                result += Coefficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        result += "x";
                    }
                }
                result += " + ";
            }
            return result;
        }
    }
}
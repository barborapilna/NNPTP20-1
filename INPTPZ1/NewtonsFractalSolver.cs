using System;
using System.Collections.Generic;
using System.Drawing;

namespace INPTPZ1
{
    public class NewtonsFractalSolver
    {
        private const int maxIterations = 30;
        private const double minValue = 0.0001;
        private const double precision = 0.5;
        private const double tolerance = 0.01;
        private const double minNumberOfArguments = 6;

        private Bitmap image;
        private string fileName;

        private double xMin;
        private double xMax;
        private double yMin;
        private double yMax;
        private double xStep;
        private double yStep;

        private int imageWidth;
        private int imageHeight;

        private Polynomial polynomial;
        private Polynomial derivation;
        private List<ComplexNumber> roots;

        public void ParseArguments(string[] args)
        {
            if (args.Length >= minNumberOfArguments)
            {
                int[] sizeOfImage = new int[2];
                for (int i = 0; i < sizeOfImage.Length; i++)
                {
                    sizeOfImage[i] = int.Parse(args[i]);
                }
                imageWidth = sizeOfImage[0];
                imageHeight = sizeOfImage[1];

                double[] inputParameters = new double[4];
                for (int i = 0; i < inputParameters.Length; i++)
                {
                    inputParameters[i] = double.Parse(args[i + 2]);
                }
                fileName = args[6];
                ProceessParsedArguments(inputParameters);
            }
            else
            {
                throw new ArgumentException("There must be at least " + minNumberOfArguments + " arguments!");
            }
        }

        private void ProceessParsedArguments(double[] inputParameters)
        {
            image = new Bitmap(imageWidth, imageHeight);
            xMin = inputParameters[0];
            xMax = inputParameters[1];
            yMin = inputParameters[2];
            yMax = inputParameters[3];

            xStep = (xMax - xMin) / imageWidth;
            yStep = (yMax - yMin) / imageHeight;

            roots = new List<ComplexNumber>();

            polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
            derivation = polynomial.Derive();
        }

        private ComplexNumber FindPixelCoordinates(int i, int j)
        {
            double x = xMin + j * xStep;
            double y = yMin + i * yStep;

            return new ComplexNumber()
            {
                Real = Math.Max(x, minValue),
                Imaginary = Math.Max(y, minValue)
            };
        }

        private int FindSolutionOfEquation(ref ComplexNumber coordinates)
        {
            int numberOfIteration = 0;
            for (int i = 0; i < maxIterations; i++)
            {
                var complexNumber = polynomial.Evaluate(coordinates).Divide(derivation.Evaluate(coordinates));
                coordinates = coordinates.Subtract(complexNumber);

                if (Math.Pow(complexNumber.Real, 2) + Math.Pow(complexNumber.Imaginary, 2) >= precision)
                {
                    i--;
                }

                numberOfIteration++;
            }

            return numberOfIteration;
        }

        private int FindRootNumber(ComplexNumber coordinates)
        {
            bool known = false;
            int numberOfRoots = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(coordinates.Real - roots[i].Real, 2) + Math.Pow(coordinates.Imaginary - roots[i].Imaginary, 2) <= tolerance)
                {
                    known = true;
                    numberOfRoots = i;
                }
            }

            if (!known)
            {
                roots.Add(coordinates);
                numberOfRoots = roots.Count;
            }

            return numberOfRoots;
        }

        private readonly Color[] colors = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        private Color PickPixelColor(int numberOfIterations, int numberOfRoots)
        {
            Color color = colors[numberOfRoots % colors.Length];

            var rMin = Math.Min(Math.Max(0, color.R - numberOfIterations * 2), 255);
            var gMin = Math.Min(Math.Max(0, color.G - numberOfIterations * 2), 255);
            var bMin = Math.Min(Math.Max(0, color.B - numberOfIterations * 2), 255);

            color = Color.FromArgb(rMin, gMin, bMin);

            return color;
        }

        public void RenderImage()
        {
            for (int i = 0; i < imageWidth; i++)
            {
                for (int j = 0; j < imageHeight; j++)
                {
                    ComplexNumber coordinates = FindPixelCoordinates(i, j);

                    int iterations = FindSolutionOfEquation(ref coordinates);
                    int rootsCount = FindRootNumber(coordinates);

                    Color color = PickPixelColor(iterations, rootsCount);

                    image.SetPixel(j, i, color);
                }
            }
        }

        public void SaveImage()
        {
            if (String.IsNullOrEmpty(fileName))
            {
                fileName = "image.png";
            }

            image.Save(fileName);
        }
    }
}

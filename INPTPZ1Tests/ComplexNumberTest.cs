using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INPTPZ1.Tests
{
    [TestClass()]
    public class ComplexNumberTest
    {
        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber firstNumber = new ComplexNumber()
            {
                Real = 10,
                Imaginary = 20
            };
            ComplexNumber secondNumber = new ComplexNumber()
            {
                Real = 1,
                Imaginary = 2
            };

            ComplexNumber actual = firstNumber.Add(secondNumber);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                Real = 11,
                Imaginary = 22
            };

            Assert.AreEqual(shouldBe, actual);
        }
    }
}



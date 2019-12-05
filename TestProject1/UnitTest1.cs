using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AnEmptyStringReturnsZero()
        {
            var sut = new StringCalculator();
            var result = sut.Calculate("");
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        [TestCase("1", 1)]
        [TestCase("3", 3)]
        public void AnyInputReturnsSameValueAsAnInteger(string input, int expected)
        {
            var sut = new StringCalculator();
            var result = sut.Calculate(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("6,7", 13)]
        [TestCase("1,2", 3)]
        [TestCase("2,3", 5)]
        [TestCase("3,5,6", 14)]
        public void NumbersSeperatedByCommasReturnTheAdditionOfTheNUmbers(string input, int expected)
        {
            var sut = new StringCalculator();
            var result = sut.Calculate(input);
            Assert.That(result, Is.EqualTo(expected));
        }
    }


    public class StringCalculator
    {
        public int Calculate(string input)
        {
            
            if (input == "")
            {
                return 0;
            }

            if (input.Length == 3)
            {
                int x = Convert.ToInt32(input[0].ToString());
                int y = Convert.ToInt32(input[2].ToString());

                return x + y;
            }
            return Convert.ToInt32(input);

        }
    }
}
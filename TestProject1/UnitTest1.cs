using System;
using System.Linq;
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
        [TestCase("1,2,3,4", 10)]
        public void NumbersSeperatedByCommasReturnTheAdditionOfTheNumbers(string input, int expected)
        {
            var sut = new StringCalculator();
            var result = sut.Calculate(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void InputAnyNumberSeperatedByNewLineBreakWIllStillGiveSumOfNumbers()
        {
            var sut = new StringCalculator();
            var result = sut.Calculate("1,2\n7");
            Assert.That(result, Is.EqualTo(10));
        }
    }


    public class StringCalculator
    {
        public int Calculate(string input)
        {

            if (input == "") // FROSD
            {
                return 0;
            }

            string total = input;
            var sum = total.Split(new char[] {',', '\n'})
                .Select(n => int.Parse(n))
                .Sum();
            return sum;
        }

    }
}
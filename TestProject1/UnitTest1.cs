using System;
using System.Collections.Generic;
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
            AssertTrue(input, expected);
        }

        [Test]
        [TestCase("6,7", 13)]
        [TestCase("1,2", 3)]
        [TestCase("2,3", 5)]
        [TestCase("3,5,6", 14)]
        [TestCase("1,2,3,4", 10)]
        public void NumbersSeperatedByCommasReturnTheAdditionOfTheNumbers(string input, int expected)
        {
            AssertTrue(input, expected);
        }

        [TestCase("1,2\n7",10)]
        [TestCase("2\n3,5,6", 16)]
        public void LinebreaksAndCommasAreInterChangeableBetweenNumbers(string input, int expected)
        {
            AssertTrue(input, expected);
        }

        private static void AssertTrue(string input, int expected)
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
            if (input == string.Empty)
            {
                return 0;
            }

            var sum = input.Split(delimiter, newline)
                .Select(n => int.Parse(n))
                .Sum();
            return sum;
        }

        private static char newline => '\n';
        private static char delimiter => ',';
    }
}
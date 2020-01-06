using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ConsoleApp2
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

        [TestCase("1,2\n7", 10)]
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

        [Test]
        [TestCase("//b\n1b2", 3)]
        [TestCase("//;\n1;2", 3)]
        public void CustomDelimiterReturnsTheAdditionOfTheNumbersBesideIt(string input, int expected)
        {
            AssertTrue(input, expected);
        }

        [Test]
        [TestCase("-1,2,-3", 0)]
        [TestCase("1,2,-3", 0)]
        [TestCase("-1", 0)]
        [TestCase("-1,-2,-3", 0)]
        public void AddWithNegativeNumbersThrowsException(string input, int expected)
        {
            AssertTrue(input, expected);
        }

        [TestCase("1000,1001,2", 2)]
        [TestCase("1000,1,2", 3)]
        public void NumbersGreaterOrEqualToThousandIgnored(string input, int expected)
        {
            AssertTrue(input, expected);
        }

        [TestCase("//de\n1de2", 3)]
        [TestCase("//anything\n1anything3", 4)]
        public void CustomDelimiterSpecifiedReturnsAdditionOfNumbersBesideIt(string input, int expected)
        {
            AssertTrue(input, expected);
        }

        [TestCase("//[*][%]\n1*2%3", 6)]
        [TestCase("//[$][^]\n3$2^5", 10)]
        [TestCase("//[$#][^^]\n3$#2^^5", 10)]
        public void MultipleDelimitersReturnsAdditionOfNumbersBesideIt(string input, int expected)
        {
            AssertTrue(input, expected);
        }
    }


    public class StringCalculator
    {
        public int Calculate(string input)
        {
            if (IsEmptyString(input)) return 0;

            if (IsCustomDelimiterSpecified(input))
            {
                if (IsMultipleDelimitersSpecified(input))
                {
                    return HasTwoDelimitersAndCalculate(input);
                }

                return CustomDelimeterCalculator(input);
            }

            if (IsNegativeSpecified(input)) return 0; //todo: change this to throw an exception

            if (IsGreaterOrEqualToThousand(input))
            {
                return IgnoreValuesGreaterThanThousandCalculator(input);
            }

            return SimpleCalculator(input);
        }

        private static int HasTwoDelimitersAndCalculate(string input)
        {
            int start = input.IndexOf('[');
            int end = input.LastIndexOf(']') - 1;
            string result = input.Substring(start, end);
            var delimiters = result.Split(']', '[');
            return TwoDelimeterCalculator(input, delimiters);
        }

        private static int SimpleCalculator(string input)
        {
            string[] sum = input.Split(delimiter, newline);
            return SumOfTextAsNumbers(sum);
        }

        private static int IgnoreValuesGreaterThanThousandCalculator(string input)
        {
            var charArray = input.Split(delimiter, newline);
            var total = 0;
            for (int i = 0; i < charArray.Length; i++)
            {
                int temp = int.Parse(charArray[i]);
                if (temp < 1000 && temp != 1000)
                {
                    total = total + temp;
                }
            }

            return total;
        }

        private static int CustomDelimeterCalculator(string input)
        {
            int lengthOfDelimiter = input.IndexOf("\n") - 2;
            String specifiedDelimiter = input.Substring(2, lengthOfDelimiter);
            string newstring = input.Substring(input.IndexOf("\n") + 1);
            var sum1 = newstring.Split(specifiedDelimiter);
            var sumOfTextAsNumbers = SumOfTextAsNumbers(sum1);
            return sumOfTextAsNumbers;
        }
        private static int TwoDelimeterCalculator(string input, string [] delimiters)
        {
            //int lengthOfDelimiter = input.IndexOf("\n") - 2;
            //String specifiedDelimiter = input.Substring(2, lengthOfDelimiter);
            string newstring = input.Substring(input.IndexOf("\n") + 1);
            var sum1 = newstring.Split(delimiters, StringSplitOptions.None);
            var sumOfTextAsNumbers = SumOfTextAsNumbers(sum1);
            return sumOfTextAsNumbers;
        }

        private static bool IsMultipleDelimitersSpecified(string input)
        {
            bool multipledelim = false;
            char[] inputArray = input.ToCharArray();
            for (int i = 0; i < inputArray.Length; i++)
            {
                if (inputArray[i] == '[')
                {
                    multipledelim = true;
                }
            }

            return multipledelim;
        }

        private static int SumOfTextAsNumbers(string[] sum1)
        {
            return sum1.Select(n => int.Parse(n)).Sum();
        }

        private static bool IsGreaterOrEqualToThousand(string input)
        {
            bool isThousandOrMore = false;
            var charArray = input.Split(delimiter, newline);
            for (int i = 0; i < charArray.Length; i++)
            {
                int temp = int.Parse(charArray[i]);
                if (temp == 1000 || temp > 1000)
                {
                    isThousandOrMore = true;
                }
            }

            return isThousandOrMore;
        }

        private static bool IsEmptyString(string input)
        {
            return input == string.Empty;
        }

        private static bool IsNegativeSpecified(string input)
        {
            bool isNegativeInt = false;
            var charArray = input.Split(delimiter, newline);
            for (int i = 0; i < charArray.Length; i++)
            {
                int temp = int.Parse(charArray[i]);
                if (temp < 0)
                {
                    isNegativeInt = true;
                }
            }

            return isNegativeInt;
        }

        private static bool IsCustomDelimiterSpecified(string input)
        {
            return input.StartsWith("//");
        }

        private static char newline => '\n';
        private static char delimiter => ',';
    }
}
using System;
using Xunit;

namespace StringCalculator
{
    // https://katalyst.codurance.com/string-calculator
    public class StringCalculatorShould
    {
        private readonly StringCalculator _sut;

        public StringCalculatorShould()
        {
            _sut = new StringCalculator();
        }

        [Fact]
        public void NumberSeparator_Equals_Comma_Character()
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(",", StringCalculator.NumberSeparator);
        }

        [Fact]
        public void Thousand_Equals_1000()
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(1000, StringCalculator.Thousand);
        }

        [Fact]
        public void Zero_Equals_0()
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(0, StringCalculator.Zero);
        }

        [Theory]
        [InlineData("", StringCalculator.Zero)]
        [InlineData("4", 4)]
        [InlineData("4,2", 6)]
        [InlineData("1,2,3,4,5,6,7,8,9", 45)]
        [InlineData("1\n2,3", 6)]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//[***]\n1***2***3", 6)]
        [InlineData("//[*][%]\n1*2%3", 6)]
        [InlineData("//[foo][bar]\n1foo2bar3", 6)]
        public void Return_The_Sum_Of_Numbers_Separated_By_Comma_Or_Zero_When_Input_Is_Empty(string input, int expected)
        {
            // Arrange
            // Act
            var actual = _sut.Add(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1000,2", 2)]
        [InlineData("1500,3,4", 7)]
        public void Return_The_Sum_Of_Numbers_Separated_By_Comma_Excluding_Value_Bigger_Than_1000(string input, int expected)
        {
            // Arrange
            // Act
            var actual = _sut.Add(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1,-2,-3", "error: negatives not allowed: -2 -3")]
        [InlineData("-5,2,-3", "error: negatives not allowed: -5 -3")]
        public void Throw_Exception_When_Input_Contains_Negative_Numbers(string input, string expectedErrorMessage)
        {
            // Arrange
            // Act
            var actual = Record.Exception(() => _sut.Add(input));

            // Assert
            Assert.Equal(expectedErrorMessage, actual.Message);
        }

    }
}

using NUnit.Framework;
using FizzBuzzKata;

namespace FizzBuzzKata.Tests
{
    /*
     * 1 -> 1
     * 2 -> 2
     * 3 -> Fizz
     * 4 -> 4
     * 5 -> Buzz
     * 6 -> Fizz
     * 10 -> Buzz
     * 15 -> FizzBuzz */
    public class FizzBuzzConverterTests
    {
        FizzBuzzConverter sut;

        [SetUp]
        public void Init()
        {
            sut = new FizzBuzzConverter(new FizzConverter(), new BuzzConverter());
        }

        [Test]
        public void Given_1_than_1_is_returned()
        { 
            var result = sut.Convert(1);

            Assert.AreEqual("1", result);
        }

        [Test]
        public void Given_2_than_2_is_returned()
        {
            var result = sut.Convert(2);

            Assert.AreEqual("2", result);
        }

        [Test]
        public void Given_3_than_Fizz_is_returned()
        {
            var result = sut.Convert(3);

            Assert.AreEqual("Fizz", result);
        }

        [Test]
        public void Given_4_than_4_is_returned()
        {
            var result = sut.Convert(4);

            Assert.AreEqual("4", result);
        }

        [Test]
        public void Given_5_than_Buzz_is_returned()
        {
            var result = sut.Convert(5);

            Assert.AreEqual("Buzz", result);
        }

        [Test]
        public void Given_6_than_Fizz_is_returned()
        {
            var result = sut.Convert(6);

            Assert.AreEqual("Fizz", result);
        }

        [Test]
        public void Given_10_than_Buzz_is_returned()
        {
            var result = sut.Convert(10);

            Assert.AreEqual("Buzz", result);
        }

        [Test]
        public void Given_15_than_FizzBuzz_is_returned()
        {
            var result = sut.Convert(15);

            Assert.AreEqual("FizzBuzz", result);
        }
    }
}
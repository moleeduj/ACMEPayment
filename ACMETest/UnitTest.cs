using ACMELibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACMETest
{
    [TestClass]
    public class UnitTest
    {
        //Simple testing example using given values in the Email request

        [TestMethod]
        public void Payment_ValidAmount_Example1_IOET()
        {
            //Arrange
            const string input = "RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00";
            const float expected = 215;
            float actual = 0;
            string errorMessage = "";

            //Act
            var calc = new Payments(input);
            actual = calc.GetAmount(ref errorMessage);

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual("", errorMessage);
        }


        [TestMethod]
        public void Payment_ValidAmount_Example2_IOET()
        {
            //Arrange
            const string input = "ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00";
            const float expected = 85;
            float actual = 0;
            string errorMessage = "";

            //Act
            var calc = new Payments(input);
            actual = calc.GetAmount(ref errorMessage);

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual("", errorMessage);
        }


        [TestMethod]
        public void Payment_ValidAmount_Example3_AllDay()
        {
            //Arrange
            const string input = "EDUARDO=MO00:00-00:00";
            const float expected = 480;
            float actual = 0;
            string errorMessage = "";

            //Act
            var calc = new Payments(input);
            actual = calc.GetAmount(ref errorMessage);

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual("", errorMessage);
        }


        [TestMethod]
        public void Payment_ValidAmount_Error1_NoRequest()
        {
            //Arrange
            const string input = "";
            const string expected = "Request error: There is no any string to evaluate.";

            //Act
            var calc = new Payments(input);

            //Assert
            Assert.AreEqual(expected, calc.ErrorMessage);
        }

        [TestMethod]
        public void Payment_ValidAmount_Error2_NoEqualSign()
        {
            //Arrange
            const string input = "SANDRAMO10:00-12:00,TH12:00-14:00,SU20:00-21:00";
            const string expected = "Request error: Missing '=' symbol.";

            //Act
            var calc = new Payments(input);

            //Assert
            Assert.AreEqual(expected, calc.ErrorMessage);
        }

        [TestMethod]
        public void Payment_ValidAmount_Error3_WrongStartTime()
        {
            //Arrange
            const string input = "SANDRA=MO10:00-32:00,TH12:00-14:00,SU20:00-21:00";
            const string expected = "Request error: Invalid end time for item (1).";
            float actual = 0;
            string errorMessage = "";

            //Act
            var calc = new Payments(input);
            actual = calc.GetAmount(ref errorMessage);

            //Assert
            Assert.AreEqual(0, actual);
            Assert.AreEqual(expected, errorMessage);
        }

    }
}

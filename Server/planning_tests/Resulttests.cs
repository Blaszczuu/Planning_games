using Server;

namespace planning_tests
{
    [TestClass]
    public class Programtest
    {
        [TestMethod]
        public void Three_votes()
        {
            Calculator calculator = new Calculator();

            List<int> votes = new List<int> { 1, 3, 3 };

            Assert.AreEqual(2, Calculator.Calculate(votes));
        }


        [TestMethod]
        public void Two_Votes()
        {
            Calculator calculator = new Calculator();

            List<int> votes = new List<int> { 1, 3 };

            Assert.AreEqual(2, Calculator.Calculate(votes));
        }
        [TestMethod]
        public void Three_Votesv2()
        {
            Calculator calculator = new Calculator();

            List<int> votes = new List<int> { 2, 3, 5 };

            Assert.AreEqual(3, Calculator.Calculate(votes));
        }
        [TestMethod]
        public void Two_with_decimal_Value_Votesv2()
        {
            Calculator calculator = new Calculator();

            List<int> votes = new List<int> { 5, 8 };

            Assert.AreEqual(8, Calculator.Calculate(votes));
        }
        [TestMethod]
        public void Foure_with_next_Votesv25()
        {
            Calculator calculator = new Calculator();

            List<int> votes = new List<int> { 5, 8, 13, 21 };

            Assert.AreEqual(13, Calculator.Calculate(votes));
        }
        [TestMethod]
        public void four_with_same_numbers_Votesv21()
        {
            Calculator calculator = new Calculator();

            List<int> votes = new List<int> { 5, 8, 8, 5 };

            Assert.AreEqual(8, Calculator.Calculate(votes));
        }
        [TestMethod]
        public void Three_with_last_Votesv22()
        {
            Calculator calculator = new Calculator();

            List<int> votes = new List<int> { 5, 8, 89 };

            Assert.AreEqual(34, Calculator.Calculate(votes));
        }
        [TestMethod]
        public void Two_with_zero_Votesv23()
        {
            Calculator calculator = new Calculator();

            List<int> votes = new List<int> { 0, 1 };

            Assert.AreEqual(0, Calculator.Calculate(votes));
        }
        [TestMethod]
        public void All_Votesv42()
        {
            Calculator calculator = new Calculator();

            List<int> votes = new List<int> { 0, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 };

            Assert.AreEqual(21, Calculator.Calculate(votes));
        }
        [TestMethod]
        public void Zero_Votesv42()
        {
            Calculator calculator = new Calculator();

            List<int> votes = new List<int> { 0, 0, 0 };

            Assert.AreEqual(0, Calculator.Calculate(votes));
        }


    }
}
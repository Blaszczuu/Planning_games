using Server;

namespace planning_tests
{
    [TestClass]
    public class Programtest
    {
        [TestMethod]
        public void Card_sender_Result()
        {
            //arrange
            
            decimal r = 3m;
            Program.Resultlist.Add(3m);
            Program.PlayersCount++;
            //act
            Program.ResultSend(r);
            //assert
            Assert.AreEqual(3m, r);       
        }
        [TestMethod]
        public void Card_sender_Result1()
        {
            //arrange
            
            decimal r = 13m/2;
            Program.Resultlist.Add(5m);
            Program.Resultlist.Add(8m);
            Program.PlayersCount++;
            Program.PlayersCount++;
            //act
            Program.ResultSend(r);
            //assert
            decimal expected = 8m;
            Assert.AreEqual(expected, r);
        }
    }
}

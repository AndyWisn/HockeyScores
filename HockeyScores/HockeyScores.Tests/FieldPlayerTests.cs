namespace HockeyScores.Tests
{
    public class FieldPlayerTests
    {
        [Test]
        public void WhenFieldPlayerGetScoresAsIntegersShouldReturnCorrectStatistics()
        {
            //arrange
            int[][] PointArray = new int[3][];
            var TestFieldPlayer = new FieldPlayer("Andrzej", "Wiœniewski", "C1234");
            string fileName = ($"{TestFieldPlayer.Licence}" + ".TXT").ToUpper();
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            PointArray[0] = [1, 1, 10];
            PointArray[1] = [2, 2, 20];
            PointArray[2] = [3, 3, 30];
            TestFieldPlayer.AddGamePoints(PointArray[0]);
            TestFieldPlayer.AddGamePoints(PointArray[1]);
            TestFieldPlayer.AddGamePoints(PointArray[2]);

            //act

            ScoringStatistics PlayerStats = TestFieldPlayer.GetScoring();

            //assert

            Assert.That(PlayerStats.TotalGoals, Is.EqualTo(6));
            Assert.That(PlayerStats.TotalAssists, Is.EqualTo(6));
            Assert.That(PlayerStats.TotalGamePlayTime, Is.EqualTo(60));
            Assert.That(PlayerStats.TotalGamesPlayed, Is.EqualTo(3));
            Assert.That(PlayerStats.CanadianScoring, Is.EqualTo(12));
            Assert.That(Math.Round(PlayerStats.Efficiency, 2), Is.EqualTo(4.00));
        }

        [Test]
        public void WhenFieldPlayerGetScoresAsStringShouldReturnCorrectStatistics()
        {    //arrange
            var TestFieldPlayer = new FieldPlayer("Andrzej", "Wiœniewski", "C1234");
            string fileName = ($"{TestFieldPlayer.Licence}" + ".TXT").ToUpper();
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            TestFieldPlayer.AddGamePoints("1, 1, 10");
            TestFieldPlayer.AddGamePoints("2, 2, 20");
            TestFieldPlayer.AddGamePoints("3, 3, 30");

            //act

            ScoringStatistics PlayerStats = TestFieldPlayer.GetScoring();

            //assert

            Assert.That(PlayerStats.TotalGoals, Is.EqualTo(6));
            Assert.That(PlayerStats.TotalAssists, Is.EqualTo(6));
            Assert.That(PlayerStats.TotalGamePlayTime, Is.EqualTo(60));
            Assert.That(PlayerStats.TotalGamesPlayed, Is.EqualTo(3));
            Assert.That(PlayerStats.CanadianScoring, Is.EqualTo(12));
            Assert.That(Math.Round(PlayerStats.Efficiency, 2), Is.EqualTo(4.00));
        }
    }
}
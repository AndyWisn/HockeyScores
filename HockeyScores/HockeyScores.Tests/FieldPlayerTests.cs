namespace HockeyScores.Tests
{
    public class fieldPlayerTests
    {
        [Test]
        public void whenFieldPlayerGetsScoresAsIntegersShouldReturnCorrectStatistics()
        {
            //arrange
            int[][] pointArray = new int[3][];
            var testFieldPlayer = new FieldPlayer("Andrzej", "Wiœniewski", "C1234");
            string fileName = ($"{testFieldPlayer.Licence}" + ".TXT").ToUpper();
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            pointArray[0] = [1, 1, 10];
            pointArray[1] = [2, 2, 20];
            pointArray[2] = [3, 3, 30];
            testFieldPlayer.AddGamePoints(pointArray[0]);
            testFieldPlayer.AddGamePoints(pointArray[1]);
            testFieldPlayer.AddGamePoints(pointArray[2]);

            //act
            ScoringStatistics playerStats = testFieldPlayer.GetScoring();

            //assert
            Assert.That(playerStats.TotalGoals, Is.EqualTo(6));
            Assert.That(playerStats.TotalAssists, Is.EqualTo(6));
            Assert.That(playerStats.TotalGamePlayTime, Is.EqualTo(60));
            Assert.That(playerStats.TotalGamesPlayed, Is.EqualTo(3));
            Assert.That(playerStats.CanadianScoring, Is.EqualTo(12));
            Assert.That(Math.Round(playerStats.Efficiency, 2), Is.EqualTo(4.00));
        }

        [Test]
        public void whenFieldPlayerGetsScoresAsStringShouldReturnCorrectStatistics()
        {    //arrange
            var testFieldPlayer = new FieldPlayer("Andrzej", "Wiœniewski", "C1234");
            string fileName = ($"{testFieldPlayer.Licence}" + ".TXT").ToUpper();
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            testFieldPlayer.AddGamePoints("1, 1, 10");
            testFieldPlayer.AddGamePoints("2, 2, 20");
            testFieldPlayer.AddGamePoints("3, 3, 30");

            //act
            ScoringStatistics playerStats = testFieldPlayer.GetScoring();

            //assert
            Assert.That(playerStats.TotalGoals, Is.EqualTo(6));
            Assert.That(playerStats.TotalAssists, Is.EqualTo(6));
            Assert.That(playerStats.TotalGamePlayTime, Is.EqualTo(60));
            Assert.That(playerStats.TotalGamesPlayed, Is.EqualTo(3));
            Assert.That(playerStats.CanadianScoring, Is.EqualTo(12));
            Assert.That(Math.Round(playerStats.Efficiency, 2), Is.EqualTo(4.00));
        }
    }
}
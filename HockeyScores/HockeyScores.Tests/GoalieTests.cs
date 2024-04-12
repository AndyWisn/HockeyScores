namespace HockeyScores.Tests
{
    public class goalieTests
    {
        [Test]
        public void whenGoalieGetsScoresAsIntegersShouldReturnCorrectStatistics()
        {    //arrange
            int[][] pointArray = new int[3][];
            var testGoalie = new Goalie("Andrzej", "Wiśniewski", "C1234");
            string fileName = ($"{testGoalie.Licence}" + ".TXT").ToUpper();
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            pointArray[0] = [1, 1, 10, 100, 1];
            pointArray[1] = [2, 2, 20, 200, 2];
            pointArray[2] = [3, 3, 30, 300, 3];
            testGoalie.AddGamePoints(pointArray[0]);
            testGoalie.AddGamePoints(pointArray[1]);
            testGoalie.AddGamePoints(pointArray[2]);

            //act
            ScoringStatistics playerStats = testGoalie.GetScoring();

            //assert
            Assert.That(playerStats.TotalGoals, Is.EqualTo(6));
            Assert.That(playerStats.TotalAssists, Is.EqualTo(6));
            Assert.That(playerStats.TotalGamePlayTime, Is.EqualTo(60));
            Assert.That(playerStats.TotalGamesPlayed, Is.EqualTo(3));
            Assert.That(playerStats.CanadianScoring, Is.EqualTo(12));
            Assert.That(playerStats.TotalOnGoalShots, Is.EqualTo(600));
            Assert.That(playerStats.TotalGoalsPassed, Is.EqualTo(6));
            Assert.That(Math.Round(playerStats.Efficiency, 2), Is.EqualTo(4.00));
            Assert.That(Math.Round(playerStats.BlockingEfficiency, 2), Is.EqualTo(0.99));
        }

        [Test]
        public void whenGoalieGetsScoresAsStringShouldReturnCorrectStatistics()
        {
            //arrange
            var testGoalie = new Goalie("Andrzej", "Wiśniewski", "C1234");
            string fileName = ($"{testGoalie.Licence}" + ".TXT").ToUpper();
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            testGoalie.AddGamePoints("1, 1, 10, 100, 1");
            testGoalie.AddGamePoints("2, 2, 20, 200, 2");
            testGoalie.AddGamePoints("3, 3, 30, 300, 3");

            //act
            ScoringStatistics playerStats = testGoalie.GetScoring();

            //assert
            Assert.That(playerStats.TotalGoals, Is.EqualTo(6));
            Assert.That(playerStats.TotalAssists, Is.EqualTo(6));
            Assert.That(playerStats.TotalGamePlayTime, Is.EqualTo(60));
            Assert.That(playerStats.TotalGamesPlayed, Is.EqualTo(3));
            Assert.That(playerStats.CanadianScoring, Is.EqualTo(12));
            Assert.That(playerStats.TotalOnGoalShots, Is.EqualTo(600));
            Assert.That(playerStats.TotalGoalsPassed, Is.EqualTo(6));
            Assert.That(Math.Round(playerStats.Efficiency, 2), Is.EqualTo(4.00));
            Assert.That(Math.Round(playerStats.BlockingEfficiency, 2), Is.EqualTo(0.99));
        }
    }
}
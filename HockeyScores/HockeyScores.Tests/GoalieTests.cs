namespace HockeyScores.Tests
{
    public class GoalieTests
    {
        [Test]
        public void WhenGoalieGetScoresAsIntegersShouldReturnCorrectStatistics()
        {    //arrange
            int[][] PointArray = new int[3][];
            var TestGoalie = new Goalie("Andrzej", "Wiśniewski", "C1234");
            string fileName = ($"{TestGoalie.Licence}" + ".TXT").ToUpper();
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            PointArray[0] = [1, 1, 10, 100, 1];
            PointArray[1] = [2, 2, 20, 200, 2];
            PointArray[2] = [3, 3, 30, 300, 3];
            TestGoalie.AddGamePoints(PointArray[0]);
            TestGoalie.AddGamePoints(PointArray[1]);
            TestGoalie.AddGamePoints(PointArray[2]);

            //act

            ScoringStatistics PlayerStats = TestGoalie.GetScoring();

            //assert

            Assert.That(PlayerStats.TotalGoals, Is.EqualTo(6));
            Assert.That(PlayerStats.TotalAssists, Is.EqualTo(6));
            Assert.That(PlayerStats.TotalGamePlayTime, Is.EqualTo(60));
            Assert.That(PlayerStats.TotalGamesPlayed, Is.EqualTo(3));
            Assert.That(PlayerStats.CanadianScoring, Is.EqualTo(12));
            Assert.That(PlayerStats.TotalOnGoalShots, Is.EqualTo(600));
            Assert.That(PlayerStats.TotalGoalsPassed, Is.EqualTo(6));
            Assert.That(Math.Round(PlayerStats.Efficiency, 2), Is.EqualTo(4.00));
            Assert.That(Math.Round(PlayerStats.BlockingEfficiency, 2), Is.EqualTo(0.99));
        }

        [Test]
        public void WhenGoalieGetScoresAsStringShouldReturnCorrectStatistics()
        {
            //arrange
            var TestGoalie = new Goalie("Andrzej", "Wiśniewski", "C1234");
            string fileName = ($"{TestGoalie.Licence}" + ".TXT").ToUpper();
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            TestGoalie.AddGamePoints("1, 1, 10, 100, 1");
            TestGoalie.AddGamePoints("2, 2, 20, 200, 2");
            TestGoalie.AddGamePoints("3, 3, 30, 300, 3");

            //act

            ScoringStatistics PlayerStats = TestGoalie.GetScoring();

            //assert
            Assert.That(PlayerStats.TotalGoals, Is.EqualTo(6));
            Assert.That(PlayerStats.TotalAssists, Is.EqualTo(6));
            Assert.That(PlayerStats.TotalGamePlayTime, Is.EqualTo(60));
            Assert.That(PlayerStats.TotalGamesPlayed, Is.EqualTo(3));
            Assert.That(PlayerStats.CanadianScoring, Is.EqualTo(12));
            Assert.That(PlayerStats.TotalOnGoalShots, Is.EqualTo(600));
            Assert.That(PlayerStats.TotalGoalsPassed, Is.EqualTo(6));
            Assert.That(Math.Round(PlayerStats.Efficiency, 2), Is.EqualTo(4.00));
            Assert.That(Math.Round(PlayerStats.BlockingEfficiency, 2), Is.EqualTo(0.99));
        }
    }
}
﻿namespace HockeyScores
{
    public class ScoringStatistics
    {
        public int TotalGoals { get; private set; }
        public int TotalAssists { get; private set; }
        public long TotalGamePlayTime { get; private set; }
        public int TotalOnGoalShots { get; private set; }
        public int TotalGoalsPassed { get; private set; }
        public int TotalGamesPlayed { get; private set; }

        public float Efficiency
        {
            get
            {
                if (this.TotalGamesPlayed != 0)
                {
                    return (float)this.CanadianScoring / this.TotalGamesPlayed;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int CanadianScoring
        {
            get
            {
                return this.TotalGoals + this.TotalAssists;
            }
        }
        public float BlockingEfficiency                                     //only for Goalies
        {
            get
            {
                if (this.TotalOnGoalShots > 0)
                {
                    return (float)(this.TotalOnGoalShots - this.TotalGoalsPassed) / this.TotalOnGoalShots;
                }
                else
                {
                    return 0;
                }
            }
        }
        public ScoringStatistics()
        {
            this.TotalGoals = 0;
            this.TotalAssists = 0;
            this.TotalGamePlayTime = 0;
            this.TotalOnGoalShots = 0;
            this.TotalGoalsPassed = 0;
            this.TotalGamesPlayed = 0;
        }
        public void AddGamePoints(int[] gamePoints)
        {
            switch (gamePoints.Length)
            {
                case 3:
                    this.TotalGoals += gamePoints[0];
                    this.TotalAssists += gamePoints[1];
                    this.TotalGamePlayTime += gamePoints[2];
                    this.TotalGamesPlayed++;
                    break;
                case 5:
                    this.TotalGoals += gamePoints[0];
                    this.TotalAssists += gamePoints[1];
                    this.TotalGamePlayTime += gamePoints[2];
                    this.TotalOnGoalShots += gamePoints[3];
                    this.TotalGoalsPassed += gamePoints[4];
                    this.TotalGamesPlayed++;
                    break;
                default:
                    throw new Exception("Unsupported data set. Should be 3 or 5 INT values.");
            }
        }
    }
}
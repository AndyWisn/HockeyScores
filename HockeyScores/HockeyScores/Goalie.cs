namespace HockeyScores
{
    public class Goalie : HockeyPlayerBase
    {
        public override event HattrickDelegate HattrickScored;
        public override event DataSavedDelegate DataSaved;
        public delegate void GoalieClearAccountDelegate(object sender, EventArgs args);
        public event GoalieClearAccountDelegate ClearAccount;

        public Goalie(string name, string surname, string licence)
            : base(name, surname, licence)
        {
            this.Position = "Goalie";
            this.ClearAccount += this.GoalieClearAccount;
        }
        public void GoalieClearAccount(object sender, EventArgs args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Event: Goalie clear account! Congrats!");
            Console.ResetColor();
        }
        public override void DisplayDataInputMessage()
        {
            Console.WriteLine("Enter new scores: Goals Scored, Assists Gained, Time Played in Seconds, Shots On Goal, Shoots Passed");
            Console.WriteLine("Example: 2,3,1200,151,1   and confirm with <Enter>");
        }
        public override void AddGamePoints(int[] gamePoints)            //Object's specific data validation included
        {
            if (gamePoints.Length == 5)
            {
                if (gamePoints[0] < 0)                                      //Goals
                {
                    throw new Exception("Goals Gained can't be negative!");
                }
                else if (gamePoints[1] < 0)                                 //Assists
                {
                    throw new Exception("Assists Gained can't be negative!");
                }
                else if ((gamePoints[2] < 0) ^ (gamePoints[3] > 4800))      //GamePlay[s]
                {
                    throw new Exception("Time played can't be negative or larger than 4800[s]!");
                }
                else if (gamePoints[3] < 0)                                 //OnGoalShots
                {
                    throw new Exception("On goal shots can't be negative!");
                }
                else if (gamePoints[4] < 0)                                 //Goals against
                {
                    throw new Exception("Goals against can't be negative!");
                }
                else if (gamePoints[3] < gamePoints[4])
                {
                    throw new Exception("Goals against larger then on goal shots! Check the data!");
                }
                else
                {
                    var FileName = this.Licence + ".TXT";
                    using (var writer = File.AppendText(FileName))
                    {
                        writer.WriteLine($"{gamePoints[0]};{gamePoints[1]};{gamePoints[2]};{gamePoints[3]};{gamePoints[4]}");
                        if (DataSaved != null)
                        {
                            DataSaved(this, new EventArgs());
                        }
                    }
                    if (gamePoints[0] > 2)
                    {
                        if (HattrickScored != null)
                        {
                            HattrickScored(this, new EventArgs());
                        }
                    }
                    if (gamePoints[4] == 0)
                    {
                        if (ClearAccount != null)
                        {
                            ClearAccount(this, new EventArgs());
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Data error. There should be 5xInt value entered.");
            }
        }
        public override ScoringStatistics GetScoring()
        {
            var Scoring = new ScoringStatistics();
            var FileName = this.Licence + ".TXT";

            if (File.Exists(FileName))
            {
                using (var reader = File.OpenText(FileName))
                {
                    string? line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.EndsWith(';'))
                        {
                            line = line.Remove(line.Length - 1);
                        }
                        int[] gamePoints = line.Split(';').Select(int.Parse).ToArray();
                        if (gamePoints.Length == 5)
                        {
                            Scoring.AddGamePoints(gamePoints);
                        }
                        else
                        {
                            throw new Exception("Corrupted FieldPlayer data line. Should be three 5xINT value like: 12;23;34;45;56");
                        }
                    }
                }
            }
            return Scoring;
        }
        public override void ShowScoring(ScoringStatistics playerScoringStatistics)
        {
            var gamePlayTime = TimeSpan.FromSeconds(playerScoringStatistics.TotalGamePlayTime).ToString("hh':'mm':'ss");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{this.Name} {this.Surname} -> {this.Position} Player in total {playerScoringStatistics.TotalGamesPlayed} Games with actual statistics:");
            Console.WriteLine($"|Goals:{playerScoringStatistics.TotalGoals}| |Assists:{playerScoringStatistics.TotalAssists}| |GamePlay:{gamePlayTime}| |Score:{playerScoringStatistics.CanadianScoring}| |Save eff:{playerScoringStatistics.BlockingEfficiency:N2}%| |Goals Against:{playerScoringStatistics.TotalGoalsPassed}|");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
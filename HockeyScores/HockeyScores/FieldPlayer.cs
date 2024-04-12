namespace HockeyScores
{
    public class FieldPlayer : HockeyPlayerBase
    {
        public override event HattrickDelegate HattrickScored;
        public override event DataSavedDelegate DataSaved;

        public FieldPlayer(string name, string surname, string licence)
            : base(name, surname, licence)
        {
            this.Position = "Field";
        }
        public override void DisplayDataInputMessage()
        {
            Console.WriteLine("Enter new scores: Goals Scored, Assists Gained, Time Played");
            Console.WriteLine("Example: 2,4,1200   and confirm with <Enter>");
        }
        public override void AddGamePoints(int[] gamePoints)            //Object's specific data validation included
        {
            if (gamePoints.Length == 3)
            {
                if (gamePoints[0] < 0)                                      //Goals
                {
                    throw new Exception("Goals Gained can't be negative!");
                }
                else if (gamePoints[1] < 0)                                 //Assists
                {
                    throw new Exception("Assists Gained can't be negative!");
                }
                else if ((gamePoints[2] < 0) ^ (gamePoints[2] > 4800))      //GamePlay[s]
                {
                    throw new Exception("Time played can't be negative or larger than 4800[s]!");
                }
                else
                {
                    var FileName = this.Licence + ".TXT";
                    using (var writer = File.AppendText(FileName))
                    {
                        writer.WriteLine($"{gamePoints[0]};{gamePoints[1]};{gamePoints[2]}");
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
                }
            }
            else
            {
                throw new Exception("Data error. There should be 3xInt value entered.");
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
                        int[] GamePoints = line.Split(';').Select(int.Parse).ToArray();
                        if (GamePoints.Length == 3)
                        {
                            Scoring.AddGamePoints(GamePoints);
                        }
                        else
                        {
                            throw new Exception("Corrupted FieldPlayer data line. Should be 3xINT value like: 12;23;34");
                        }
                    }
                }
            }
            return Scoring;
        }
        public override void ShowScoring(ScoringStatistics PlayerScoringStatistics)
        {
            var GamePlayTime = TimeSpan.FromSeconds(PlayerScoringStatistics.TotalGamePlayTime).ToString("hh':'mm':'ss");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{this.Name} {this.Surname} -> {this.Position} Player in total {PlayerScoringStatistics.TotalGamesPlayed} Games with actual statistics:");
            Console.WriteLine($"|Goals:{PlayerScoringStatistics.TotalGoals}| |Assists:{PlayerScoringStatistics.TotalAssists}| |GamePlay:{GamePlayTime}| |Eff:{PlayerScoringStatistics.Efficiency:N2}%| |Score:{PlayerScoringStatistics.CanadianScoring}|");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}

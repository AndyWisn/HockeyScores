using System.Drawing;

namespace HockeyScores
{
    public class FieldPlayer : HockeyPlayerBase
    {
        public override event HattrickDelegate HattrickScored;
        public FieldPlayer(string name, string surname, string licence)
            : base(name, surname, licence)
        {
            this.Position = "Field";
        }
        public override void DisplayDataInputMessage()
        {
            Console.WriteLine("Enter new scores: Goals Scored; Assists Gained; Time Played");
        }
        public override void AddGamePoints(int[] GamePoints)            //Object's specific data validation included
        {
            if (GamePoints.Length == 3)
            {
                if (GamePoints[0] < 0)                                      //Goals
                {
                    throw new Exception("Goals Gained can't be negative!");
                }
                else if (GamePoints[1] < 0)                                 //Assists
                {
                    throw new Exception("Assists Gained can't be negative!");
                }
                else if ((GamePoints[2] < 0) ^ (GamePoints[2] > 4800))      //GamePlay[s]
                {
                    throw new Exception("Time played can't be negative or larger than 3600[s]!");
                }
                else
                {
                    var FileName = this.Licence + ".TXT";
                    using (var writer = File.AppendText(FileName))
                    {
                        writer.WriteLine($"{GamePoints[0]};{GamePoints[1]};{GamePoints[2]}");
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
            Console.ResetColor();
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{this.Name} {this.Surname} -> {this.Position} in total {PlayerScoringStatistics.TotalGamesPlayed} Games with actual statistics:");
            Console.WriteLine($"Goals:{PlayerScoringStatistics.TotalGoals}| Assists:{PlayerScoringStatistics.TotalAssists}| GamePlay:{PlayerScoringStatistics.TotalGamePlayTime}s| Efficiency:{PlayerScoringStatistics.Efficiency:N2}| Canadian scoring:{PlayerScoringStatistics.CanadianScoring}|");
            Console.ResetColor();

        }
    }
}

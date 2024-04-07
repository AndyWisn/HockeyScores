namespace HockeyScores
{
    public abstract class HockeyPlayerBase : IHockeyPlayer
    {
        public delegate void HattrickDelegate(object sender, EventArgs args);
        public abstract event HattrickDelegate HattrickScored;       
        public HockeyPlayerBase(string name, string surname, string licence)
        {
            this.Name = name;
            this.Surname = surname;
            this.Licence = licence;
            this.HattrickScored += this.PlayerHattrickScored;
        }
        public void PlayerHattrickScored(object sender, EventArgs args)
        {
            Console.WriteLine("Hattrick scored!");
        }
        public virtual string? Name { get; set; }
        public virtual string? Surname { get; set; }
        public virtual string? Licence { get; set; }
        public virtual string? Position { get; set; }
       
        public void AddGamePoints(int Goals, int Assists, int TimeInSeconds, int OnGoalShots, int ShotsPassed)  
        {
            var FileName = this.Licence + ".TXT";
            using (var writer = File.AppendText(FileName))
            {
                writer.WriteLine($"{Goals};{Assists};{TimeInSeconds};{OnGoalShots};{ShotsPassed}");
            }
        }

        public abstract void AddGamePoints(int[] GamePoints);

        public void AddGamePoints(string GamePointsInString)
        {
            if ((GamePointsInString.EndsWith(';'))^ (GamePointsInString.EndsWith('.')))
            {
                GamePointsInString = GamePointsInString.Remove(GamePointsInString.Length - 1);
            }
            int[] GamePoints = GamePointsInString.Split(';').Select(int.Parse).ToArray();
            this.AddGamePoints(GamePoints);
        }

        public abstract ScoringStatistics GetScoring();

        public abstract void ShowScoring(ScoringStatistics PlayerScoringStatistics);
      
        public abstract void DisplayDataInputMessage();

    }
}

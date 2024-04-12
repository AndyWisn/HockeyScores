using static HockeyScores.HockeyPlayerBase;
namespace HockeyScores
{
    public interface IHockeyPlayer
    {
        string Name { get; }
        string Surname { get; }
        string Licence { get; }
        string Position { get; }
        public void AddGamePoints(string gamePointString);
        public void ShowScoring(ScoringStatistics playerScoringStatistic);
        public ScoringStatistics GetScoring();
        public event HattrickDelegate HattrickScored;
    }
}
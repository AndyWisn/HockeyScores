using static HockeyScores.HockeyPlayerBase;

namespace HockeyScores
{
    public interface IHockeyPlayer
    {
        string Name { get; }
        string Surname { get; }
        string Licence { get; }
        string Position { get; }
        public void AddGamePoints(string GamePointString);
        public void ShowScoring(ScoringStatistics PlayerScoringStatistic);
        public ScoringStatistics GetScoring();

        event HattrickDelegate HattrickScored;
    }
}

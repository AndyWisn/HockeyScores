using HockeyScores;

Console.WriteLine("The HockeyScores v1.0");
Console.WriteLine("=====================");
Console.WriteLine();
List<HockeyPlayerBase> players = new List<HockeyPlayerBase>();
players.Add(new Goalie("Leon", "Wiśniewski", "A3456"));
players.Add(new FieldPlayer("Roch", "Wiśniewski", "B3434"));
players.Add(new FieldPlayer("Andrzej", "Wiśniewski", "C1234"));

while (true)
{
    Console.WriteLine("List of registered players:\n");
    foreach (var player in players)
    {
        Console.WriteLine($"Licence |{player.Licence}| {player.Name} {player.Surname} ({player.Position})");
    }
    Console.WriteLine();
    Console.WriteLine("Please choose the Player licence # to view/edit scores or Q to quit:");
    string input = Console.ReadLine().ToUpper();
    if (input == "Q")
    {
        break;
    }
    var playerFound = false;
    foreach (var player in players)
    {
        if (input == player.Licence)
        {
            playerFound = true;
            player.ShowScoring(player.GetScoring());
            player.DisplayDataInputMessage();
            try
            {
                player.AddGamePoints(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}\n");
            }
            player.ShowScoring(player.GetScoring());
        }
    }
    if (!playerFound)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"No Player with licence #{input} found!");
        Console.ResetColor();
        Console.WriteLine();
    }
}
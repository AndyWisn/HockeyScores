using HockeyScores;
Console.WriteLine("The HockeyScores v1.0");
Console.WriteLine("=====================");
Console.WriteLine();
List<HockeyPlayerBase> Players = new List<HockeyPlayerBase>();
Players.Add(new Goalie("Leon", "Wiśniewski", "A3456"));
Players.Add(new FieldPlayer("Roch", "Wiśniewski", "B3434"));
while (true)
{
    Console.WriteLine("List of registered players:\n");
    foreach (var Player in Players)
    {
        Console.WriteLine($"Licence |{Player.Licence}| {Player.Name} {Player.Surname} ({Player.Position})");
    }
    Console.WriteLine();
    Console.WriteLine("Please choose the Player licence # to view/edit scores or Q for quit:");
    string? input = Console.ReadLine().ToUpper();
    if (input == "Q")
    {
        break;
    }  
    var PlayerFound = false;
    foreach (var Player in Players)
    {
        if (input == Player.Licence)
        {
            PlayerFound = true;
            
            Player.ShowScoring(Player.GetScoring());
            Player.DisplayDataInputMessage();
                try
                {
                    Player.AddGamePoints(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception: {e.Message}\n");
                }
            Player.ShowScoring(Player.GetScoring());
        }
    }
    if (!PlayerFound)
    {
        Console.WriteLine($"No player licence #{input} found!");
        Console.WriteLine();
    }
}
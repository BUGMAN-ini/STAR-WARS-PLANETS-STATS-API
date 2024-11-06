using STAR_WARS_PLANETS_STATS.ApiDataAccess;

try
{
    await new StarWarsPlanetStatsApp(
        new ApiDataReader(),
        new MockStarWarsApiDataReader()).Run();
}
catch (Exception ex)
{

    Console.WriteLine(ex.Message);
}

Console.ReadKey();









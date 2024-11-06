using STAR_WARS_PLANETS_STATS.ApiDataAccess;
using System.Text.Json;

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

public class StarWarsPlanetStatsApp
{
    private readonly IApiDataReader _apiDataReader;
    private readonly IApiDataReader _secondapidata;
    public StarWarsPlanetStatsApp(IApiDataReader apiDataReader, IApiDataReader secondapidata)
    {
        _apiDataReader = apiDataReader;
        _secondapidata = secondapidata;
    }




    public async Task Run()
    {
        string? json = null;
        try
        {
            json = await _apiDataReader.Read("https://swapi.dev/", "api/planets");

        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex.Message);
        }
        if (json is null)
        {
            json = await _secondapidata.Read("https://swapi.dev/", "api/planets");
        }

        var root = JsonSerializer.Deserialize<Root>(json);

        var planets = ToPlanets(root);
    }

    private IEnumerable<Planet> ToPlanets(Root? root)
    {
        if (root is null)
        {
            throw new ArgumentNullException(nameof(root));
        }
        
        var planets = new List<Planet>();

        foreach (var planetDto in root.results)
        {
            Planet planet = (Planet)planetDto;
            planets.Add(planet);
        }

        return planets;
    }

    public readonly record struct Planet
    {
        public string Name { get; }
        public int Diameter { get; }
        public int? SurfaceWater { get; }
        public int? Population { get; }

        public Planet(
            string name,
            int diameter,
            int surface,
            int population)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
            Diameter = diameter;
            SurfaceWater = surface;
            Population = population;
        }

        public static explicit operator Planet(Result planetDto)
        {
            var name = planetDto.name;
            var diameter = int.Parse(planetDto.diameter);
            int? population = null;
            if (int.TryParse(
                planetDto.population,out int populationParsed))
            {
                population = populationParsed;
            }
            int? SurfaceTemp = null;
            if (int.TryParse(
                planetDto.population, out int surfacetemperature))
            {
                SurfaceTemp = surfacetemperature;
            }
        }
    }
}







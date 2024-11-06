using STAR_WARS_PLANETS_STATS.ApiDataAccess;
using System.Text.Json;

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

        foreach (var planet in planets)
        {
            Console.WriteLine(planet);
        }
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


    public static explicit operator Planet(Result? planetDto)
    {
        var name = planetDto.name;
        var diameter = int.Parse(planetDto.diameter);
        int? population = planetDto?.population.IsIntOrNull();
        int? Surface = planetDto?.surface_water.IsIntOrNull();

        return new Planet(name, diameter, population, Surface);
    }
}









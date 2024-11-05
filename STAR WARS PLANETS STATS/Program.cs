using STAR_WARS_PLANETS_STATS.ApiDataAccess;
using System.Text.Json;
using System.Text.Json.Serialization;

const string ExpectedBaseAddress = "https://swapi.dev/";
const string ExpectedRequestUri = "api/planets";
var fullUrl = "https://swapi.dev/api/planets";


IApiDataReader apiDataReader = new ApiDataReader();
var json = await apiDataReader.Read(fullUrl);



Console.ReadKey();




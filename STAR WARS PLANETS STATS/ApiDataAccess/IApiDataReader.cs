namespace STAR_WARS_PLANETS_STATS.ApiDataAccess;

public interface IApiDataReader
{
    Task<string> Read(string baseAddress, string requestUri);
    Task<string> Read(string url);
}

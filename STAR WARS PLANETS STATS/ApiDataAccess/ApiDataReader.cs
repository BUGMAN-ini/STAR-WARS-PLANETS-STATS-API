using STAR_WARS_PLANETS_STATS.ApiDataAccess;

public class ApiDataReader : IApiDataReader
{
    public async Task<string> Read(string baseaddress, string requesturi)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(baseaddress);
        HttpResponseMessage response = await client.GetAsync(requesturi);

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> Read(string url)
    {
        using var client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
    }
}




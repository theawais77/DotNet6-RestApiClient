using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
public class RestApiClient
{
    private readonly HttpClient client;

    // Base Address (to get or set baseaddress of uri)
    public RestApiClient(string baseAddress)
    {
        client = new HttpClient
        {
            BaseAddress = new Uri(baseAddress)
        };

        client.DefaultRequestHeaders.Accept.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    // GET method
    public async Task<T> GetAsync<T>(string path)
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            return default;
        }
    }

    // DELETE method
    public async Task<HttpStatusCode> DeleteAsync(string path)
    {
        try
        {
            HttpResponseMessage response = await client.DeleteAsync(path);
            return response.StatusCode;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            return HttpStatusCode.InternalServerError;
        }
    }


    // POST method
    public async Task<Uri> PostAsync<T>(string path, T item)
    {
        try
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(path, item);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            return null;
        }
    }

    // PUT method
    public async Task<T> PutAsync<T>(string path, T item)
    {
        try
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(path, item);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            return default;
        }
    }


}

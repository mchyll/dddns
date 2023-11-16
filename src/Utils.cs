using System.Net;
using System.Text.Json;

class Utils : IDisposable
{
    private readonly HttpClient client = new();
    private readonly string hostname;
    private readonly string token;
    private readonly string secret;

    public Utils()
    {
        var json = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "config.json")));
        hostname = json.GetValueOrDefault("hostname", "");
        token = json.GetValueOrDefault("token", "");
        secret = json.GetValueOrDefault("secret", "");
    }

    public async Task<string> GetCurrentIp()
    {
        return await client.GetStringAsync("https://api.ipify.org/");
    }

    public async Task<string> GetHostIp()
    {
        var hostEntry = await Dns.GetHostEntryAsync(hostname);
        return hostEntry.AddressList.First().ToString();
    }

    public async Task UpdateHostIp(string ip)
    {
        var message = new HttpRequestMessage(HttpMethod.Get, $"https://api.domeneshop.no/v0/dyndns/update?hostname={hostname}&myip={ip}");
        message.Headers.Authorization = new("Basic", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{token}:{secret}")));
        await client.SendAsync(message);
    }

    public void Dispose()
    {
        client.Dispose();
    }
}
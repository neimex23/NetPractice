using System.Net.Http.Json;
using System.Text.Json;
using NetMobile.Models;

namespace NetMobile.Services;

public class ApiService
{
    // En Android emulador, localhost del host = 10.0.2.2
    // En iOS simulador, podés usar localhost directo.
    // Ajustá si corre en dispositivo real.
#if ANDROID
    private const string BASE = "https://10.0.2.2:7250/api";
#else
    private const string BASE = "https://localhost:7250/api";
#endif

    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions _json = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ApiService()
    {
#if DEBUG
        // Permitir certs self-signed en dev (HTTPS de Kestrel)
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
        };
        _http = new HttpClient(handler);
#else
        _http = new HttpClient();
#endif
        _http.Timeout = TimeSpan.FromSeconds(15);
    }

    // ================  PAISES  ================
    public Task<List<Pais>> GetPaises()          => GetList<Pais>("/pais");
    public Task<Pais?>      GetPais(string id)   => GetOne<Pais>($"/pais/{id}");
    public Task<Pais?>      CreatePais(Pais p)   => Post<Pais>("/pais", p);
    public Task<bool>       UpdatePais(Pais p)   => Put($"/pais/{p.Id}", WithId(p, p.Id!));
    public Task<bool>       DeletePais(string id) => Delete($"/pais/{id}");

    // ================  CONFEDERACIONES  ================
    public Task<List<Confederacion>> GetConfederaciones()        => GetList<Confederacion>("/confederacion");
    public Task<Confederacion?>      CreateConfederacion(Confederacion c) => Post<Confederacion>("/confederacion", c);
    public Task<bool>                UpdateConfederacion(Confederacion c) => Put($"/confederacion/{c.Id}", WithId(c, c.Id!));
    public Task<bool>                DeleteConfederacion(string id)       => Delete($"/confederacion/{id}");

    // ================  DEPORTES  ================
    public Task<List<Deporte>> GetDeportes()              => GetList<Deporte>("/deporte");
    public Task<Deporte?>      CreateDeporte(Deporte d)   => Post<Deporte>("/deporte", d);
    public Task<bool>          UpdateDeporte(Deporte d)   => Put($"/deporte/{d.Id}", WithId(d, d.Id!));
    public Task<bool>          DeleteDeporte(string id)   => Delete($"/deporte/{id}");

    // ================  HELPERS  ================
    private async Task<List<T>> GetList<T>(string path)
    {
        try
        {
            var resp = await _http.GetAsync(BASE + path);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<List<T>>(_json) ?? new();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[API] GET {path} -> {ex.Message}");
            return new();
        }
    }

    private async Task<T?> GetOne<T>(string path)
    {
        var resp = await _http.GetAsync(BASE + path);
        if (!resp.IsSuccessStatusCode) return default;
        return await resp.Content.ReadFromJsonAsync<T>(_json);
    }

    private async Task<T?> Post<T>(string path, object body)
    {
        var resp = await _http.PostAsJsonAsync(BASE + path, body, _json);
        if (!resp.IsSuccessStatusCode)
        {
            var err = await resp.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"[API] POST {path} {resp.StatusCode} {err}");
            return default;
        }
        try { return await resp.Content.ReadFromJsonAsync<T>(_json); }
        catch { return default; }
    }

    private async Task<bool> Put(string path, object body)
    {
        var resp = await _http.PutAsJsonAsync(BASE + path, body, _json);
        if (!resp.IsSuccessStatusCode)
        {
            var err = await resp.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"[API] PUT {path} {resp.StatusCode} {err}");
        }
        return resp.IsSuccessStatusCode;
    }

    private async Task<bool> Delete(string path)
    {
        var resp = await _http.DeleteAsync(BASE + path);
        return resp.IsSuccessStatusCode;
    }

    // El back espera { id, Id, ... } en el PUT (igual que el front)
    private static Dictionary<string, object?> WithId(object source, string id)
    {
        var json = JsonSerializer.Serialize(source, _json);
        var dict = JsonSerializer.Deserialize<Dictionary<string, object?>>(json, _json)
                   ?? new Dictionary<string, object?>();
        dict["id"] = id;
        dict["Id"] = id;
        return dict;
    }
}

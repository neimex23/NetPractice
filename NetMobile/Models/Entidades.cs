using System.Text.Json.Serialization;

namespace NetMobile.Models;

public class Pais
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("fechaFundacion")]
    public DateTime? FechaFundacion { get; set; }

    [JsonPropertyName("confederacionId")]
    public string? ConfederacionId { get; set; }

    [JsonPropertyName("deporteId")]
    public string? DeporteId { get; set; }

    // ---------- Props solo para binding en la UI (no se serializan) ----------
    [JsonIgnore]
    public string ConfederacionNombre { get; set; } = "—";

    [JsonIgnore]
    public string DeporteNombre { get; set; } = "—";

    [JsonIgnore]
    public string FechaFundacionDisplay =>
        FechaFundacion.HasValue
            ? $"Fundación: {FechaFundacion.Value:dd MMM yyyy}"
            : "Fundación: —";
}

public class Confederacion
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = string.Empty;
}

public class Deporte
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = string.Empty;
}

using NetMobile.Models;
using NetMobile.Services;

namespace NetMobile.Pages;

[QueryProperty(nameof(DeporteParam), "Deporte")]
public partial class DeporteFormPage : ContentPage
{
    private readonly ApiService _api;
    private Deporte? _editing;

    public Deporte? DeporteParam
    {
        set
        {
            _editing = value;
            if (value is not null)
            {
                TitleLabel.Text  = "Editar deporte";
                NombreEntry.Text = value.Nombre;
            }
        }
    }

    public DeporteFormPage(ApiService api)
    {
        InitializeComponent();
        _api = api;
    }

    private async void OnSave(object? sender, EventArgs e)
    {
        var nombre = NombreEntry.Text?.Trim();
        if (string.IsNullOrWhiteSpace(nombre))
        {
            await DisplayAlertAsync("", "El nombre es requerido", "OK");
            return;
        }

        SaveButton.IsEnabled = false;

        var dep = new Deporte { Id = _editing?.Id, Nombre = nombre };
        bool ok = _editing is null
            ? await _api.CreateDeporte(dep) is not null
            : await _api.UpdateDeporte(dep);

        SaveButton.IsEnabled = true;

        if (!ok)
        {
            await DisplayAlertAsync("Error", "No se pudo guardar", "OK");
            return;
        }

        await Shell.Current.GoToAsync("..");
    }

    private async void OnCancel(object? sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");
}

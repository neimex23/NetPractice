using NetMobile.Models;
using NetMobile.Services;

namespace NetMobile.Pages;

[QueryProperty(nameof(ConfederacionParam), "Confederacion")]
public partial class ConfederacionFormPage : ContentPage
{
    private readonly ApiService _api;
    private Confederacion? _editing;

    public Confederacion? ConfederacionParam
    {
        set
        {
            _editing = value;
            if (value is not null)
            {
                TitleLabel.Text  = "Editar confederación";
                NombreEntry.Text = value.Nombre;
            }
        }
    }

    public ConfederacionFormPage(ApiService api)
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

        var conf = new Confederacion { Id = _editing?.Id, Nombre = nombre };
        bool ok = _editing is null
            ? await _api.CreateConfederacion(conf) is not null
            : await _api.UpdateConfederacion(conf);

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

using NetMobile.Models;
using NetMobile.Services;

namespace NetMobile.Pages;

public partial class PaisesPage : ContentPage
{
    private readonly ApiService _api;
    private List<Pais>          _paises = new();
    private List<Confederacion> _confs  = new();
    private List<Deporte>       _deps   = new();

    public PaisesPage(ApiService api)
    {
        InitializeComponent();
        _api = api;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        Loader.IsVisible = true;
        Loader.IsRunning = true;

        var pTask = _api.GetPaises();
        var cTask = _api.GetConfederaciones();
        var dTask = _api.GetDeportes();
        await Task.WhenAll(pTask, cTask, dTask);

        _paises = pTask.Result;
        _confs  = cTask.Result;
        _deps   = dTask.Result;

        var confMap = _confs.Where(c => c.Id is not null)
                            .ToDictionary(c => c.Id!, c => c.Nombre);
        var depMap  = _deps.Where(d => d.Id is not null)
                           .ToDictionary(d => d.Id!, d => d.Nombre);

        foreach (var p in _paises)
        {
            p.ConfederacionNombre = p.ConfederacionId is not null
                                    && confMap.TryGetValue(p.ConfederacionId, out var cn)
                                        ? cn : "—";
            p.DeporteNombre       = p.DeporteId is not null
                                    && depMap.TryGetValue(p.DeporteId, out var dn)
                                        ? dn : "—";
        }

        StatPaises.Text    = _paises.Count.ToString();
        StatConfs.Text     = _confs.Count.ToString();
        StatDeportes.Text  = _deps.Count.ToString();
        SubtitleLabel.Text = $"{_paises.Count} registros";

        // Forzamos re-bind de la colección (no es ObservableCollection)
        PaisesList.ItemsSource = null;
        PaisesList.ItemsSource = _paises;

        Loader.IsRunning = false;
        Loader.IsVisible = false;
        Refresher.IsRefreshing = false;
    }

    private async void OnRefresh(object? sender, EventArgs e) => await LoadAsync();

    private async void OnNewClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("paisForm");
    }

    private async void OnEditClicked(object? sender, EventArgs e)
    {
        if (sender is Button b && b.CommandParameter is Pais p)
        {
            await Shell.Current.GoToAsync("paisForm",
                new Dictionary<string, object> { ["Pais"] = p });
        }
    }

    private async void OnDeleteClicked(object? sender, EventArgs e)
    {
        if (sender is not Button b || b.CommandParameter is not Pais p || p.Id is null) return;

        var ok = await DisplayAlertAsync("Eliminar país",
                                    $"¿Eliminar “{p.Nombre}”?",
                                    "Eliminar", "Cancelar");
        if (!ok) return;

        var result = await _api.DeletePais(p.Id);
        if (result)
        {
            await DisplayAlertAsync("", "País eliminado", "OK");
            await LoadAsync();
        }
        else
        {
            await DisplayAlertAsync("Error", "No se pudo eliminar", "OK");
        }
    }
}

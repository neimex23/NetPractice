using NetMobile.Models;
using NetMobile.Services;

namespace NetMobile.Pages;

public partial class ConfederacionesPage : ContentPage
{
    private readonly ApiService _api;

    public ConfederacionesPage(ApiService api)
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

        var data = await _api.GetConfederaciones();
        List.ItemsSource   = data;
        SubtitleLabel.Text = $"{data.Count} registros";

        Loader.IsRunning = false;
        Loader.IsVisible = false;
        Refresher.IsRefreshing = false;
    }

    private async void OnRefresh(object? sender, EventArgs e) => await LoadAsync();

    private async void OnNewClicked(object? sender, EventArgs e)
        => await Shell.Current.GoToAsync("confederacionForm");

    private async void OnEditClicked(object? sender, EventArgs e)
    {
        if (sender is Button b && b.CommandParameter is Confederacion c)
        {
            await Shell.Current.GoToAsync("confederacionForm",
                new Dictionary<string, object> { ["Confederacion"] = c });
        }
    }

    private async void OnDeleteClicked(object? sender, EventArgs e)
    {
        if (sender is not Button b || b.CommandParameter is not Confederacion c || c.Id is null) return;

        var ok = await DisplayAlertAsync("Eliminar confederación", $"¿Eliminar “{c.Nombre}”?", "Eliminar", "Cancelar");
        if (!ok) return;

        if (await _api.DeleteConfederacion(c.Id))
        {
            await DisplayAlertAsync("", "Confederación eliminada", "OK");
            await LoadAsync();
        }
        else
        {
            await DisplayAlertAsync("Error", "No se pudo eliminar", "OK");
        }
    }
}

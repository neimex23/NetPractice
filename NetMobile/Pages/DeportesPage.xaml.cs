using NetMobile.Models;
using NetMobile.Services;

namespace NetMobile.Pages;

public partial class DeportesPage : ContentPage
{
    private readonly ApiService _api;

    public DeportesPage(ApiService api)
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

        var data = await _api.GetDeportes();
        List.ItemsSource   = data;
        SubtitleLabel.Text = $"{data.Count} registros";

        Loader.IsRunning = false;
        Loader.IsVisible = false;
        Refresher.IsRefreshing = false;
    }

    private async void OnRefresh(object? sender, EventArgs e) => await LoadAsync();

    private async void OnNewClicked(object? sender, EventArgs e)
        => await Shell.Current.GoToAsync("deporteForm");

    private async void OnEditClicked(object? sender, EventArgs e)
    {
        if (sender is Button b && b.CommandParameter is Deporte d)
        {
            await Shell.Current.GoToAsync("deporteForm",
                new Dictionary<string, object> { ["Deporte"] = d });
        }
    }

    private async void OnDeleteClicked(object? sender, EventArgs e)
    {
        if (sender is not Button b || b.CommandParameter is not Deporte d || d.Id is null) return;

        var ok = await DisplayAlertAsync("Eliminar deporte", $"¿Eliminar “{d.Nombre}”?", "Eliminar", "Cancelar");
        if (!ok) return;

        if (await _api.DeleteDeporte(d.Id))
        {
            await DisplayAlertAsync("", "Deporte eliminado", "OK");
            await LoadAsync();
        }
        else
        {
            await DisplayAlertAsync("Error", "No se pudo eliminar", "OK");
        }
    }
}

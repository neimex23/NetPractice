using NetMobile.Models;
using NetMobile.Services;

namespace NetMobile.Pages;

[QueryProperty(nameof(PaisParam), "Pais")]
public partial class PaisFormPage : ContentPage
{
    private readonly ApiService _api;
    private Pais? _editing;

    private List<Confederacion> _confs = new();
    private List<Deporte>       _deps  = new();

    private Confederacion? _selectedConf;
    private Deporte?       _selectedDep;

    public Pais? PaisParam
    {
        set
        {
            _editing = value;
            if (value is not null) BindForm(value);
        }
    }

    public PaisFormPage(ApiService api)
    {
        InitializeComponent();
        _api = api;
        FechaPicker.Date = DateTime.Today;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _confs = await _api.GetConfederaciones();
        _deps  = await _api.GetDeportes();

        // Preselección si estoy editando
        if (_editing is not null)
        {
            if (!string.IsNullOrEmpty(_editing.ConfederacionId))
            {
                _selectedConf = _confs.FirstOrDefault(c => c.Id == _editing.ConfederacionId);
                if (_selectedConf is not null) SetConfLabel(_selectedConf.Nombre);
            }
            if (!string.IsNullOrEmpty(_editing.DeporteId))
            {
                _selectedDep = _deps.FirstOrDefault(d => d.Id == _editing.DeporteId);
                if (_selectedDep is not null) SetDepLabel(_selectedDep.Nombre);
            }
        }
    }

    private void BindForm(Pais p)
    {
        TitleLabel.Text  = "Editar país";
        NombreEntry.Text = p.Nombre;
        if (p.FechaFundacion.HasValue)
            FechaPicker.Date = p.FechaFundacion.Value;
    }

    private void SetConfLabel(string text)
    {
        ConfLabel.Text      = text;
        ConfLabel.TextColor = Color.FromArgb("#171717");
    }

    private void SetDepLabel(string text)
    {
        DepLabel.Text      = text;
        DepLabel.TextColor = Color.FromArgb("#171717");
    }

    private async void OnConfTapped(object? sender, TappedEventArgs e)
    {
        if (_confs.Count == 0)
        {
            await DisplayAlert("", "No hay confederaciones cargadas", "OK");
            return;
        }

        var opts   = _confs.Select(c => c.Nombre).ToArray();
        var chosen = await DisplayActionSheet("Seleccionar confederación", "Cancelar", null, opts);
        if (string.IsNullOrEmpty(chosen) || chosen == "Cancelar") return;

        var sel = _confs.FirstOrDefault(c => c.Nombre == chosen);
        if (sel is not null)
        {
            _selectedConf = sel;
            SetConfLabel(sel.Nombre);
        }
    }

    private async void OnDepTapped(object? sender, TappedEventArgs e)
    {
        if (_deps.Count == 0)
        {
            await DisplayAlert("", "No hay deportes cargados", "OK");
            return;
        }

        var opts   = _deps.Select(d => d.Nombre).ToArray();
        var chosen = await DisplayActionSheet("Seleccionar deporte", "Cancelar", null, opts);
        if (string.IsNullOrEmpty(chosen) || chosen == "Cancelar") return;

        var sel = _deps.FirstOrDefault(d => d.Nombre == chosen);
        if (sel is not null)
        {
            _selectedDep = sel;
            SetDepLabel(sel.Nombre);
        }
    }

    private async void OnSave(object? sender, EventArgs e)
    {
        var nombre = NombreEntry.Text?.Trim();
        if (string.IsNullOrWhiteSpace(nombre))
        {
            await DisplayAlert("", "El nombre es requerido", "OK");
            return;
        }
        if (_selectedConf is null)
        {
            await DisplayAlert("", "Seleccioná una confederación", "OK");
            return;
        }
        if (_selectedDep is null)
        {
            await DisplayAlert("", "Seleccioná un deporte", "OK");
            return;
        }

        SaveButton.IsEnabled = false;

        var pais = new Pais
        {
            Id              = _editing?.Id,
            Nombre          = nombre,
            FechaFundacion  = FechaPicker.Date,
            ConfederacionId = _selectedConf.Id,
            DeporteId       = _selectedDep.Id
        };

        bool ok;
        if (_editing is null)
        {
            var created = await _api.CreatePais(pais);
            ok = created is not null;
        }
        else
        {
            ok = await _api.UpdatePais(pais);
        }

        SaveButton.IsEnabled = true;

        if (!ok)
        {
            await DisplayAlert("Error", "No se pudo guardar", "OK");
            return;
        }

        await Shell.Current.GoToAsync("..");
    }

    private async void OnCancel(object? sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");
}

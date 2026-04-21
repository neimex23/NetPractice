using NetMobile.Pages;

namespace NetMobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Rutas para navegación modal (formularios)
        Routing.RegisterRoute("paisForm",          typeof(PaisFormPage));
        Routing.RegisterRoute("confederacionForm", typeof(ConfederacionFormPage));
        Routing.RegisterRoute("deporteForm",       typeof(DeporteFormPage));
    }
}

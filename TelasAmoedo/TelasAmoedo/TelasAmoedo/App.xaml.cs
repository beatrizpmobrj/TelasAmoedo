using System;
using TelasAmoedo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("BeauRivage-Regular.ttf", Alias = "Amoedo")]
[assembly: ExportFont("JosefinSans-Bold.ttf", Alias = "Pontos")]
[assembly: ExportFont("JosefinSans-Light.ttf", Alias = "Others")]

namespace TelasAmoedo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            //MainPage = new MenuPrincipal();
            //MainPage = new Login();
            //MainPage = new Campanha03();
            //MainPage = new Cadastro();
            //MainPage = new MenuCampanhas();
            //MainPage = new ConfirmacaoCodigo();
            //MainPage = new ConfirmacaoTelefone();
            //MainPage = new AppShell();

            Routing.RegisterRoute("cadastropage", typeof(Cadastro));
            //Routing.RegisterRoute("menuprincipalpage", typeof(MenuPrincipal));
            Routing.RegisterRoute("confirmacaotelefonepage", typeof(ConfirmacaoTelefone));
            Routing.RegisterRoute("confirmacaocodigopage", typeof(ConfirmacaoCodigo));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

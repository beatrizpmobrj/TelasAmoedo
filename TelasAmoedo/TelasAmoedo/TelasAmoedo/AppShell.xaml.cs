using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelasAmoedo.Views;
using Xamarin.Forms;

namespace TelasAmoedo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("cadastropage", typeof(Cadastro));
            Routing.RegisterRoute("menuprincipalpage", typeof(MenuPrincipal));
            Routing.RegisterRoute("confirmacaotelefonepage", typeof(ConfirmacaoTelefone));
            Routing.RegisterRoute("confirmacaocodigopage", typeof(ConfirmacaoCodigo));
            Routing.RegisterRoute("menucampanhas", typeof(MenuCampanhas));
            Routing.RegisterRoute("menuvoucher", typeof(MenuVoucher));
            Routing.RegisterRoute("resgate", typeof(Resgate));
            Routing.RegisterRoute("extrato", typeof(Extrato));

        }
    }
}

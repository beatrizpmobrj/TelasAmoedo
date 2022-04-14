using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TelasAmoedo.Views;
using Xamarin.Forms;

namespace TelasAmoedo.ViewModels
{
    public class MenuPrincipalViewModel
    {
        public ICommand AvancarCampanhas { get; set; }

        public MenuPrincipalViewModel()
        {
            AvancarCampanhas = new Command(async () => await Campanhas());

        }

        private async Task Campanhas()
        {
            await Shell.Current.GoToAsync("menucampanhas");
        }
    }
}

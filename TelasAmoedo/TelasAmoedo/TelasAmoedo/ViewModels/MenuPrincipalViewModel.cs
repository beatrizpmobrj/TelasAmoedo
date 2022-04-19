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
        public ICommand AvancarVoucher { get; set; }
        public ICommand AvancarResgate { get; set; } 
        public ICommand AvancarExtrato { get; set; }

        public MenuPrincipalViewModel()
        {
            AvancarCampanhas = new Command(async () => await RedirectToMenu("menucampanhas"));
            AvancarVoucher = new Command(async () => await RedirectToMenu("menuvoucher"));
            AvancarResgate = new Command(async () => await RedirectToMenu("resgate"));
            AvancarExtrato = new Command(async () => await RedirectToMenu("extrato"));
        }

   
        private async Task RedirectToMenu(string menu)
        {
            await Shell.Current.GoToAsync(menu);
        }

    }
}

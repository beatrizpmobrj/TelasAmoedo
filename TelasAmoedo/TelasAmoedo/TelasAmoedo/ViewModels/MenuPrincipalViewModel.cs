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
            AvancarCampanhas = new Command(async () => await Campanhas());
            AvancarVoucher = new Command(async () => await Voucher());
            AvancarResgate = new Command(async () => await Resgate());
            AvancarExtrato = new Command(async () => await Extrato());
        }

        private async Task Campanhas()
        {
            await Shell.Current.GoToAsync("menucampanhas");
        }

        private async Task Voucher()
        {
            await Shell.Current.GoToAsync("menuvoucher");
        }

        private async Task Resgate()
        {
            await Shell.Current.GoToAsync("resgate");
        }

        private async Task Extrato()
        {
            await Shell.Current.GoToAsync("extrato");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TelasAmoedo.Views;
using Xamarin.Forms;

namespace TelasAmoedo.ViewModels
{
    public class CadastroViewModel
    {
        public ICommand AvancarCommand { get; set; }
        public CadastroViewModel()
        {
            AvancarCommand = new Command(async () => await AvancarAsync());
        }

        private async Task AvancarAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}

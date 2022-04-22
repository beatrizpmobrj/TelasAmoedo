using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TelasAmoedo.Views;
using Xamarin.Forms;

namespace TelasAmoedo.ViewModels
{
    public class OpcoesViewModel
    {
        public ICommand LogoutCommand { get; set; }

        public OpcoesViewModel()
        {
            LogoutCommand = new Command(async () => await LogoutAsync());
        }

        public async Task LogoutAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}

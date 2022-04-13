using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TelasAmoedo.ViewModels
{
    public class ConfirmacaoTelefoneViewModel
    {
        public ICommand VoltarCommand { get; set; }
        public ConfirmacaoTelefoneViewModel()
        {
            VoltarCommand = new Command(async () => await BotaoVoltar());
        }

        private async Task BotaoVoltar()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

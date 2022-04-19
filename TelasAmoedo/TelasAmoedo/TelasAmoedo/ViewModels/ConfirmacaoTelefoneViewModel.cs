using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TelasAmoedo.ViewModels
{
    public class ConfirmacaoTelefoneViewModel
    {
        public ICommand VoltarCommand { get; set; }
        public ICommand AvancarCommand { get; set; }
        public ConfirmacaoTelefoneViewModel()
        {
            VoltarCommand = new Command(async () => await BotaoVoltar());
            AvancarCommand = new Command(async () => await Avancar());
        }
        private async Task Avancar()
        {
            await Shell.Current.GoToAsync("confirmacaocodigopage");
        }
        private async Task BotaoVoltar()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

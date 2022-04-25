using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TelasAmoedo.Models;
using Xamarin.Forms;

namespace TelasAmoedo.ViewModels
{
    public class MenuVoucherViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ItemsMenuVoucher> Itens { get; set; }
        public ICommand AvancarResgate { get; set; }

        public MenuVoucherViewModel()
        {
            Itens = new ObservableCollection<ItemsMenuVoucher>();
            Itens.Add(new ItemsMenuVoucher { Codigo=123456, Valor=250});
            Itens.Add(new ItemsMenuVoucher { Codigo = 234567, Valor = 150 });
            Itens.Add(new ItemsMenuVoucher { Codigo = 345678, Valor = 150 });
            Itens.Add(new ItemsMenuVoucher { Codigo = 456789, Valor = 50 });
            Itens.Add(new ItemsMenuVoucher { Codigo = 567890, Valor = 250 });
            Itens.Add(new ItemsMenuVoucher { Codigo = 678901, Valor = 150 });

            AvancarResgate = new Command(async () => await RedirectToResgate());
        }

        private async Task RedirectToResgate()
        {
            await Shell.Current.GoToAsync("resgate");
        }
         
    }
}

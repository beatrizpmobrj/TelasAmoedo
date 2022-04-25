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
    public class MenuCampanhasViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ItemsMenuCampanhas> Itens { get; set; }
        public ICommand AvancarDetalhesCampanha { get; set; }

        public MenuCampanhasViewModel()
        {
            Itens = new ObservableCollection<ItemsMenuCampanhas>();
            Itens.Add(new ItemsMenuCampanhas { Nome = "Campanha 06"});
            Itens.Add(new ItemsMenuCampanhas { Nome = "Campanha 05"});
            Itens.Add(new ItemsMenuCampanhas { Nome = "Campanha 04"});
            Itens.Add(new ItemsMenuCampanhas { Nome = "Campanha 03"});
            Itens.Add(new ItemsMenuCampanhas { Nome = "Campanha 02"});
            Itens.Add(new ItemsMenuCampanhas { Nome = "Campanha 01"});

            AvancarDetalhesCampanha = new Command(async () => await RedirectToDetailsCampanha());

        }

        private async Task RedirectToDetailsCampanha()
        {
            await Shell.Current.GoToAsync("detalhescampanha");
        }
    }
}


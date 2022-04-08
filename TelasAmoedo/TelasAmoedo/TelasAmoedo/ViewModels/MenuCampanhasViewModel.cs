using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using TelasAmoedo.Models;

namespace TelasAmoedo.ViewModels
{
    public class MenuCampanhasViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ItemsMenuCampanhas> Itens { get; set; }

        public MenuCampanhasViewModel()
        {
            Itens = new ObservableCollection<ItemsMenuCampanhas>();
            Itens.Add(new ItemsMenuCampanhas { Nome = "Campanha 06"});
            Itens.Add(new ItemsMenuCampanhas { Nome = "Campanha 05"});
            Itens.Add(new ItemsMenuCampanhas { Nome = "Campanha 04"});
            Itens.Add(new ItemsMenuCampanhas { Nome = "Campanha 03"});
            Itens.Add(new ItemsMenuCampanhas { Nome = "Campanha 02"});
            Itens.Add(new ItemsMenuCampanhas { Nome = "Campanha 01"});

        }
    }
}


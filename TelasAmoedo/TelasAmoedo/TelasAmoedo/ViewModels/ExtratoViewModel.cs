using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TelasAmoedo.Models;
using Xamarin.Forms;

namespace TelasAmoedo.ViewModels
{
    public class ExtratoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ItemsExtrato> ExtratoHeader { get => GetExtrato(); }
        public ObservableCollection<ExtratoContent> ExtratoContent { get; set; }

        private ObservableCollection<ItemsExtrato> GetExtrato()
        {
            return new ObservableCollection<ItemsExtrato>
            {
                new ItemsExtrato { Tipo = "Lançamento", Valor = "R$ 250,00",
                    ExtratoContent = new ObservableCollection<ExtratoContent> { new ExtratoContent { NumOp=123, DataOp="12/08/2021 13:45:23", DataEx=DateTime.Today, Pontos=123, Produtos="Poltrona Flex"} }},

                  new ItemsExtrato { Tipo = "Resgate", Valor = "R$ 150,00",
                    ExtratoContent = new ObservableCollection<ExtratoContent> { new ExtratoContent { NumOp=123, DataOp="12/08/2021 13:45:23", DataEx=DateTime.Today, Pontos=123, Produtos="Poltrona Flex"} }},

                  new ItemsExtrato { Tipo = "Lançamento", Valor = "R$ 150,00",
                    ExtratoContent = new ObservableCollection<ExtratoContent> { new ExtratoContent { NumOp=123, DataOp="12/08/2021 13:45:23", DataEx=DateTime.Today, Pontos=123, Produtos="Poltrona Flex"} }},

                  new ItemsExtrato { Tipo = "Resgate", Valor = "R$ 50,00",
                    ExtratoContent = new ObservableCollection<ExtratoContent> { new ExtratoContent { NumOp=123, DataOp="12/08/2021 13:45:23", DataEx=DateTime.Today, Pontos=123, Produtos="Poltrona Flex"} }},

                  new ItemsExtrato { Tipo = "Lançamento", Valor = "R$ 250,00",
                    ExtratoContent = new ObservableCollection<ExtratoContent> { new ExtratoContent { NumOp=123, DataOp="12/08/2021 13:45:23", DataEx=DateTime.Today, Pontos=123, Produtos="Poltrona Flex"} }},

                  new ItemsExtrato { Tipo = "Resgate", Valor = "R$ 150,00",
                    ExtratoContent = new ObservableCollection<ExtratoContent> { new ExtratoContent { NumOp=123, DataOp="12/08/2021 13:45:23", DataEx=DateTime.Today, Pontos=123, Produtos="Poltrona Flex"} }},

            };
        }
    }
}

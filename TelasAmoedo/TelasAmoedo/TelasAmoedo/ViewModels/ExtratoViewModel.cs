using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TelasAmoedo.Models;
using Xamarin.Forms;

namespace TelasAmoedo.ViewModels
{
    public class ExtratoViewModel: INotifyPropertyChanged
    {
        public List<ItemsExtrato> Extrato { get; private set; }
        public ICommand ExpandCommand { get; private set; }

        public bool IsExpanded { get; set; }
        public string Message { get; private set; }

        public ExtratoViewModel()
        {
            CriarColecaoExtrato();
            ExpandCommand = new Command<ItemsExtrato>(Expand);
            IsExpanded = true;
        }

        void Expand(ItemsExtrato itens)
        {
            Message = $"{itens.Tipo} tocado.";
            OnPropertyChanged("Message");
        }

        void CriarColecaoExtrato()
        {
            Extrato = new List<ItemsExtrato>();
            Extrato.Add(new ItemsExtrato
            {
                Tipo = "Lançamento",
                Valor = "R$ 250,00",
                NumOp=123,
                DataOp="12/08/2021 13:45:23",
                DataEx=DateTime.Today,
                Pontos=123,
                Produtos="Poltrona Flex"

            });

            Extrato.Add(new ItemsExtrato
            {
                Tipo = "Resgate",
                Valor = "R$ 150,00",
                 NumOp = 123,
                DataOp = "12/08/2021 13:45:23",
                DataEx = DateTime.Today,
                Pontos = 123,
                Produtos = "Poltrona Flex"

            });

            Extrato.Add(new ItemsExtrato
            {
                Tipo = "Lançamento",
                Valor = "R$ 150,00",
                NumOp = 123,
                DataOp = "12/08/2021 13:45:23",
                DataEx = DateTime.Today,
                Pontos = 123,
                Produtos = "Poltrona Flex"
            });

            Extrato.Add(new ItemsExtrato
            {
                Tipo = "Resgate",
                Valor = "R$ 50,00",
                NumOp = 123,
                DataOp = "12/08/2021 13:45:23",
                DataEx = DateTime.Today,
                Pontos = 123,
                Produtos = "Poltrona Flex"
            });

            Extrato.Add(new ItemsExtrato
            {
                Tipo = "Lançamento",
                Valor = "R$ 250,00",
                NumOp = 123,
                DataOp = "12/08/2021 13:45:23",
                DataEx = DateTime.Today,
                Pontos = 123,
                Produtos = "Poltrona Flex"
            });

            Extrato.Add(new ItemsExtrato
            {
                Tipo = "Resgate",
                Valor = "R$ 150,00",
                NumOp = 123,
                DataOp = "12/08/2021 13:45:23",
                DataEx = DateTime.Today,
                Pontos = 123,
                Produtos = "Poltrona Flex"
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

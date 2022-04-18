using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelasAmoedo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TelasAmoedo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Extrato : ContentPage
    {
        public Extrato()
        {
            BindingContext = new ExtratoViewModel();
            InitializeComponent();
        }
    }
}
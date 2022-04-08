using System;
using System.Collections.Generic;
using System.Text;

namespace TelasAmoedo.Models
{
    public class ItemsMenuCampanhas
    {
       public string Nome { get; set; }
        public DateTime PeriodoI { get; set; } = DateTime.Now;
        public DateTime PeriodoF { get; set; } = DateTime.Now;

        private string _dataFormatada;
        public string DataFormatada 
        { 
            get { return ObterDataFormatada(PeriodoI,PeriodoF); }  
            set { _dataFormatada = value; } 
        }   
        public string Icone { get; set; } = "flechamcamp.png";


        public string ObterDataFormatada(DateTime periodoInicial, DateTime periodoFinal)
        {
            string diaI = periodoInicial.Day.ToString();
            string mesI = periodoInicial.Month.ToString();

            string diaF = periodoFinal.Day.ToString();
            string mesF = periodoFinal.Month.ToString();


            return $"{diaI}/{mesI} - {diaF}/{mesF}";
        }
    }
}

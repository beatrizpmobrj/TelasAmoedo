using System;
using System.Collections.Generic;
using System.Text;

namespace TelasAmoedo.Models
{
    public class ItemsExtrato
    {
        public string Tipo { get; set; }
        public string Valor { get; set; }
        public int NumOp { get; set; }
        public string DataOp { get; set; }
        public DateTime DataEx { get; set; }
        public int Pontos { get; set; }
        public string Produtos { get; set; }
    }
}

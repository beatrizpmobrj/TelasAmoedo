using System;
using System.Collections.Generic;

namespace TelasAmoedo.Models
{
        public class Login
        {
            public string Email { get; set; }
            public string Senha { get; set; }
        }

        public class Record
        {
            public string Id { get; set; }
            public DateTime CreatedTime { get; set; }
            public Login Fields { get; set; }
        }

        public class AirtableResponse
        {
            public List<Record> Records { get; set; }
        }
    
}

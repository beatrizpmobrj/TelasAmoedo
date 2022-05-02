using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TelasAmoedo.Interfaces
{
    public interface ILogin
    {
        Task ValidarLogin(string email, string senha);
        Task LoginAsync();
        Task SalvarLogin();
    }
}

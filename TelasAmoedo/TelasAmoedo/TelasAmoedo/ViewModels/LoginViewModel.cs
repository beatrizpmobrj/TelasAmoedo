using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Fingerprint.Abstractions;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using TelasAmoedo.Views;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.Extensions;

namespace TelasAmoedo.ViewModels
{
    public class LoginViewModel : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string IsPrimeiroAcesso { get; set; }
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string _senha;
        public string Senha
        {
            get
            {
                return _senha;
            }
            set
            {
                _senha = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Senha"));
            }
        }
        public ICommand MostrarLoginCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CadastroCommand { get; set; }
        public ICommand SalvarLoginCommand { get; set; }

        public LoginViewModel()
        {
            MostrarLoginCommand = new Command(async () => await MostrarLoginAsync());
            LoginCommand = new Command(async () => await LoginAsync());
            CadastroCommand = new Command(async () => await CadastroAsync());
            //SalvarLoginCommand = new Command(async () => await SalvarLogin()); // Icone do Face Id, apenas para teste
        }

        private async Task CadastroAsync()
        {
            //await Shell.Current.GoToAsync("cadastropage");
            await Shell.Current.GoToAsync("confirmacaotelefonepage");
        }

        private async Task LoginAsync()
        {
            string emailValido = "email@mobrj.br";

            string _emailSecureStorage = await Xamarin.Essentials.SecureStorage.GetAsync("email");
            string _senhaSecureStorage = await Xamarin.Essentials.SecureStorage.GetAsync("senha");
            string _isPrimeiroAcesso = await Xamarin.Essentials.SecureStorage.GetAsync("isPrimeiroAcesso");

            // Se for primeira vez, usuário opta ou não por logar com biometria
            if (!String.IsNullOrWhiteSpace(Email) && !String.IsNullOrWhiteSpace(Senha)) // Usuário passa email e senha
            { 
                if(Email != _emailSecureStorage) // Se email não for o do secure storage, perguntar se quer salvar
                {
                    var availability = await CrossFingerprint.Current.IsAvailableAsync();

                    if (availability)
                    {
                        var reposta = await App.Current.MainPage.DisplayAlert("Alerta!", "Deseja usar biometria para entrar na próxima vez?", "Sim", "Não");

                        if (reposta) // Se o usuário quiser salvar o login
                        {
                            IsPrimeiroAcesso = "true";
                            await SalvarLogin();
                            await ValidarLogin(Email, Senha);
                        }
                        else // Se o usuário não quiser salvar o login
                        {
                            await ValidarLogin(Email, Senha);
                        }
                    }
                }
                else // Se o email for igual ao secure storage, verificar se possui biometria
                {
                    var availability = await CrossFingerprint.Current.IsAvailableAsync();

                    if (availability) // Se possuir biometria, logar com ela
                    {
                        var authResult = await CrossFingerprint.Current.AuthenticateAsync(
                        new AuthenticationRequestConfiguration("Acesso Biométrico", "Confirme sua impressão digital para acessar sua conta."));

                        if (authResult.Authenticated) // Caso não seja autenticado o próprio pacote possui uma mensagem
                        {
                            await Shell.Current.GoToAsync($"//{nameof(MenuPrincipal)}");
                        }
                    }
                    else // Se não possuir biometria, entrar com login e senha
                    {
                        await ValidarLogin(Email, Senha);
                    }
                }
                
            }

            // Se não for a primeira vez
            //// Se usuário optou por logar com biometria
            //// Se usuário optou por não logar com biometria

            //if (!String.IsNullOrWhiteSpace(Email) && !String.IsNullOrWhiteSpace(Senha)) // Usuário passa email e senha - sem digital
            //{
            //    await ValidarLogin(Email, Senha);
            //}
            //else if (!String.IsNullOrWhiteSpace(Email) && String.IsNullOrWhiteSpace(Senha)) // Se passar somente email - com digital
            //{
            //    if (Email == emailValido)
            //    {
            //        // Pode virar um método
            //        var availability = await CrossFingerprint.Current.IsAvailableAsync();

            //        if (!availability)
            //        {
            //            await App.Current.MainPage.DisplayToastAsync("Leitor biométrico não disponível.", 3000);
            //            return; // Sai do método
            //        }

            //        var authResult = await CrossFingerprint.Current.AuthenticateAsync(
            //            new AuthenticationRequestConfiguration("Acesso Biométrico", "Confirme sua impressão digital para acessar sua conta."));

            //        if (authResult.Authenticated) // Caso não seja autenticado o próprio pacote possui uma mensagem
            //        {
            //            await Shell.Current.GoToAsync($"//{nameof(MenuPrincipal)}");
            //        }
            //    }
            //    else
            //    {
            //        await App.Current.MainPage.DisplayToastAsync("Insira seu login corretamente.", 3000);
            //        return;
            //    }
            //}
            //else if(String.IsNullOrWhiteSpace(Email) && String.IsNullOrWhiteSpace(Senha)) // Usuário não passa nada, verificar secure storage
            //{
            //    if (_emailSecureStorage == emailValido)
            //    {
            //        Email = _emailSecureStorage; // Mostra o email na tela

            //        // Pode virar um método
            //        var availability = await CrossFingerprint.Current.IsAvailableAsync();

            //        if (!availability)
            //        {
            //            await App.Current.MainPage.DisplayToastAsync("Leitor biométrico não disponível.", 3000);
            //            return; // Sai do método
            //        }

            //        var reposta = await App.Current.MainPage.DisplayAlert("Alerta!", "Deseja usar biometria?", "Sim", "Não");

            //        if (reposta)
            //        {
            //            var authResult = await CrossFingerprint.Current.AuthenticateAsync(
            //            new AuthenticationRequestConfiguration("Acesso Biométrico", "Confirme sua impressão digital para acessar sua conta."));

            //            if (authResult.Authenticated)
            //            {
            //                await Shell.Current.GoToAsync($"//{nameof(MenuPrincipal)}");
            //            }
            //        }
            //        else
            //        {
            //            Email = null; // Retira o email da tela
            //            await App.Current.MainPage.DisplayToastAsync("Insira email e senha.", 2000);
            //            await ValidarLogin(Email, Senha);
            //        }
            //    }
            //    else
            //    {
            //        await App.Current.MainPage.DisplayToastAsync("Usuário não encontrado.", 3000);
            //        return;
            //    }
            //}
            //else
            //{
            //    await App.Current.MainPage.DisplayToastAsync("Insira seu login.", 2000);
            //}
            
        }

        //private async Task IsDigitalValidadaAsync()
        //{
        //    var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

        //    if (status != PermissionStatus.Granted)
        //        status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

        //    if (status != PermissionStatus.Granted) // Se por qualquer motivo o status for diferente de Granted; caso seja negado o uso
        //        return;

        //    var availability = await CrossFingerprint.Current.IsAvailableAsync();

        //    var authenticationType = await CrossFingerprint.Current.GetAuthenticationTypeAsync(); // Pegando o tipo para posterior mensagem personalizada

        //    if (!availability)
        //    {
        //        await App.Current.MainPage.DisplayToastAsync("Leitor biométrico não disponível.", 5000);
        //        return;
        //    }

        //    var authResult = await CrossFingerprint.Current.AuthenticateAsync(
        //        new AuthenticationRequestConfiguration("Acesso Biométrico", "Confirme sua impressão digital para acessar sua conta."));

        //    if (authResult.Authenticated)
        //    {
        //        await App.Current.MainPage.DisplayToastAsync("Acesso liberado!", 5000);
        //        await Shell.Current.GoToAsync("confirmacaotelefonepage");
        //    }
        //}
        private async Task<string> MostrarLoginAsync()
        {
            try
            {
                _email = await Xamarin.Essentials.SecureStorage.GetAsync("email");
            }
            catch (Exception)
            {
                _email = "";
            }
            return _email;
        }
        private async Task SalvarLogin()
        {
            await Xamarin.Essentials.SecureStorage.SetAsync("email", Email);
            await Xamarin.Essentials.SecureStorage.SetAsync("senha", Senha);
            await Xamarin.Essentials.SecureStorage.SetAsync("isPrimeiroAcesso", IsPrimeiroAcesso);
            //await App.Current.MainPage.DisplayToastAsync("Login salvo com sucesso!", 3000);
        }
        public async Task ValidarLogin(string email, string senha)
        {
            string emailValido = "email@mobrj.br";
            string senhaValida = "123456";

            if (!String.IsNullOrWhiteSpace(email) && !String.IsNullOrWhiteSpace(senha))
            {
                if (emailValido == email && senhaValida == senha)
                {
                    await App.Current.MainPage.DisplayToastAsync("Acesso liberado!", 2000);
                    await Shell.Current.GoToAsync($"//{nameof(MenuPrincipal)}");
                }
                else
                {
                    await App.Current.MainPage.DisplayToastAsync("Email e/ou senha inválidos", 3000);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayToastAsync("Digite seu email e senha.", 3000);
            }
        }
    }
}

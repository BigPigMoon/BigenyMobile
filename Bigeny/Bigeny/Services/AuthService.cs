using Bigeny.Http;
using Bigeny.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Bigeny.Services
{
    internal class AuthService
    {
        public static async Task<bool> Login(string email, string password)
        {
            Tokens tokens = await API.Login(email, password);
            if (tokens == null)
                return false;
            try
            {
                await SecureStorage.SetAsync(StorageKey.AccessToken, tokens.AccessToken);
                await SecureStorage.SetAsync(StorageKey.RefreshToken, tokens.RefreshToken);
            }
            catch (Exception ex)
            {
                // may be need to degub this, mb :/
            }

            return true;
        }

        public static async Task<bool> Register(string email, string password, string nickname)
        {
            Tokens tokens = await API.Registration(email, password, nickname);
            if (tokens == null)
                return false;
            try
            {
                await SecureStorage.SetAsync(StorageKey.AccessToken, tokens.AccessToken);
                await SecureStorage.SetAsync(StorageKey.RefreshToken, tokens.RefreshToken);
            }
            catch (Exception ex)
            {
                // may be need to degub this, mb :/
            }

            return true;
        }

        public static async void Logout()
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                await API.Logout(at);
                SecureStorage.Remove(StorageKey.AccessToken);
                SecureStorage.Remove(StorageKey.RefreshToken);
            }
            catch(Exception ex)
            {

            }
        }
    }
}

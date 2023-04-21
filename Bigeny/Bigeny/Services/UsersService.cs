using Bigeny.Http;
using Bigeny.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarians.CropImage;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bigeny.Services
{
    internal class UsersService
    {
        public static async Task<User> GetMe()
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();
                return await UserApi.GetMe(tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task UpdateDeviceToken(string deviceToken)
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();
                await UserApi.UpdateDeviceToken(tok, deviceToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static async Task<List<User>> GetUsers()
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();
                return await UserApi.GetUsers(tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<User> GetUser(int id)
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();
                return await UserApi.GetUser(tok, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<User> UploadAvatar()
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();

                string filename = await StorageService.Upload();
                return await UserApi.UpdateAvatar(tok, filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<User> ChangeNickname(string newName)
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();

                return await UserApi.ChangeName(tok, newName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}

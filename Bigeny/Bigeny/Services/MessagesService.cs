using Bigeny.Http;
using Bigeny.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Bigeny.Services
{
    internal class MessagesService
    {
        public static async Task<List<Dialog>> GetDialogs()
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();
                return await MessageApi.GetDialogs(tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<Dialog> GetDialog(int id)
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();
                return await MessageApi.GetDialog(tok, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<List<Message>> GetMessages(int id)
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();
                return await MessageApi.GetMessages(tok, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<Dialog> CreateDialog(List<int> users, string name, string avatar)
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();
                return await MessageApi.CreateDialog(tok, users, name, avatar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task Send(int dialogId, string text)
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();
                await MessageApi.Send(tok, dialogId, text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

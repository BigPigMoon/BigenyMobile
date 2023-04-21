using Bigeny.Http;
using Bigeny.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Bigeny.Services
{
    public class ChannelService
    {
        public static async Task<Channel> CreateChannel(string name, string description, string avatar)
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();
                return await ChannelApi.CreateChannel(tok, name, description, avatar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<List<Channel>> GetChannels()
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();
                return await ChannelApi.GetChannels(tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<Channel> GetChannel(int channelId)
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();
                return await ChannelApi.GetChannel(tok, channelId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<List<Channel>> GetSubsChannels()
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();
                return await ChannelApi.GetSubsChannels(tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<Channel> Subscribe(int channelId)
        {
            try
            {
                Tokens tok = await AuthService.GetTokens();
                return await ChannelApi.Subscribe(tok, channelId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}

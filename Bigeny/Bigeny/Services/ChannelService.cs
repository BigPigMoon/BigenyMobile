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
        public static async Task<bool> CreateChannel(string name, string description, string avatar)
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await ChannelApi.CreateChannel(tok, name, description, avatar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public static async Task<List<Channel>> GetChannels()
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
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
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
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
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await ChannelApi.GetSubsChannels(tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<bool> Subscribe(int channelId)
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await ChannelApi.Subscribe(tok, channelId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}

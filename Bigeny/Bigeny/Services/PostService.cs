using Bigeny.Http;
using Bigeny.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Bigeny.Services
{
    public class PostService
    {
        public static async Task<Post> CreatePost(string content, string image, int channelId)
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await PostApi.CreatePost(tok, content, image, channelId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<List<Post>> GetPosts()
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await PostApi.GetPosts(tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<Post> GetPost(int id)
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await PostApi.GetPost(tok, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<List<Post>> GetPostsFromChannel(int channelId)
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await PostApi.GetPostsFromChannel(tok, channelId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<List<Post>> GetPostFromSubscribesChannel()
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await PostApi.GetPostFromSubscribesChannel(tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<bool> SetPostRate(int postId, bool positive)
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await PostApi.SetPostRate(tok, postId, positive);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public static async Task<PostRate> GetPostRate(int postId)
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await PostApi.GetPostRate(tok, postId) ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}

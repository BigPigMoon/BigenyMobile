using Bigeny.Models;
using Bigeny.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Bigeny.Http
{
    internal class AuthApi
    {
        public static async Task<Tokens> Login(string email, string password)
        {
            StringContent data = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    email,
                    password,
                }),
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage res;
            try
            {
                res = await Api.api.PostAsync("auth/local/singin", data);
            }
            catch
            {
                return null;
            }

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonRes = await res.Content.ReadAsStringAsync();
                Tokens tokens = JsonConvert.DeserializeObject<Tokens>(jsonRes);
                return tokens;
            }
            return null;
        }

        public static async Task<Tokens> Registration(string email, string password, string nickname)
        {
            StringContent data = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    email,
                    password,
                    nickname,
                }),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage res; 
            try
            {
            res = await Api.api.PostAsync("auth/local/singup", data);

            }
            catch
            {
                return null;
            }

            if (res.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var jsonRes = await res.Content.ReadAsStringAsync();
                Tokens tokens = JsonConvert.DeserializeObject<Tokens>(jsonRes);
                return tokens;
            }

            return null;
        }

        public static async Task Logout(string token)
        {
            HttpClient headerApi = new HttpClient()
            {
                BaseAddress = Api.api.BaseAddress
            };

            headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
            await headerApi.PostAsync("auth/logout", null);

            }
            catch
            {
                return;
            }
        }

        public static async Task<Tokens> Refresh(string refreshToken)
        {
            HttpClient headerApi = new HttpClient()
            {
                BaseAddress = Api.api.BaseAddress
            };

            headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", refreshToken);
            HttpResponseMessage res;
            try
            {
                res = await headerApi.PostAsync("auth/refresh", null);
            }
            catch
            {
                return null;
            }

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    var jsonRes = await res.Content.ReadAsStringAsync();
                    Tokens tokens = JsonConvert.DeserializeObject<Tokens>(jsonRes);
                    await SecureStorage.SetAsync(StorageKey.AccessToken, tokens.AccessToken);
                    await SecureStorage.SetAsync(StorageKey.RefreshToken, tokens.RefreshToken);
                    return tokens;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }
            return null;
        }
    }
}

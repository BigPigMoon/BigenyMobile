using Bigeny.Models;
using Bigeny.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Bigeny.Http
{
    internal class API
    {
        private static HttpClient api = new HttpClient()
        {
            BaseAddress = new Uri("http://45.132.1.89:3000/"),
        };

        public static async Task<Tokens> Login(string email, string password)
        {
            StringContent data = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    email,
                    password,
                }),
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage res = await api.PostAsync("auth/local/singin", data);

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

            HttpResponseMessage res = await api.PostAsync("auth/local/singup", data);

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
                BaseAddress = api.BaseAddress
            };

            headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await headerApi.PostAsync("auth/logout", null);
        }

        public static async Task<Tokens> Refresh(string refreshToken)
        {
            HttpClient headerApi = new HttpClient()
            {
                BaseAddress = api.BaseAddress
            };

            headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", refreshToken);
            HttpResponseMessage res = await headerApi.PostAsync("auth/refresh", null);

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

        public static async Task<List<Users>> GetUsers(Tokens tokens)
        {
            HttpClient headerApi = new HttpClient()
            {
                BaseAddress = api.BaseAddress
            };

            headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            HttpResponseMessage res = await headerApi.GetAsync("users");

            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Tokens newTokens = await Refresh(tokens.RefreshToken);
                if (newTokens != null)
                {
                    headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newTokens.AccessToken);
                    res = await headerApi.GetAsync("users");
                }
                else
                    return null;
            }
            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            string usersJson = await res.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Users>>(usersJson);
        }

        public static async Task<Users> GetMe(Tokens tokens)
        {
            HttpClient headerApi = new HttpClient()
            {
                BaseAddress = api.BaseAddress
            };

            headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            HttpResponseMessage res = await headerApi.GetAsync("users/me");

            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Tokens newTokens = await Refresh(tokens.RefreshToken);
                if (newTokens != null)
                {
                    headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newTokens.AccessToken);
                    res = await headerApi.GetAsync("users/me");
                }
                else
                    return null;
            }
            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            string usersJson = await res.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Users>(usersJson);
        }

        public static async Task<List<Users>> GetUsersNotme(Tokens tokens)
        {
            HttpClient headerApi = new HttpClient()
            {
                BaseAddress = api.BaseAddress
            };

            headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            HttpResponseMessage res = await headerApi.GetAsync("users/notme");

            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Tokens newTokens = await Refresh(tokens.RefreshToken);
                if (newTokens != null)
                {
                    headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newTokens.AccessToken);
                    res = await headerApi.GetAsync("users/notme");
                }
                else
                    return null;
            }
            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            string usersJson = await res.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Users>>(usersJson);
        }

        public static async Task<Users> UploadAvatar(Tokens tokens, Stream stream, string filename)
        {
            HttpClient headerApi = new HttpClient()
            {
                BaseAddress = api.BaseAddress
            };

            var formData = new MultipartFormDataContent
            {
                { new StreamContent(stream), "image", filename }
            };

            headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            HttpResponseMessage res = await headerApi.PostAsync("users/uploadAvatar", formData);

            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Tokens newTokens = await Refresh(tokens.RefreshToken);
                if (newTokens != null)
                {
                    headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newTokens.AccessToken);
                    res = await headerApi.PostAsync("users/uploadAvatar", formData);
                }
                else
                    return null;
            }
            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            string usersJson = await res.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Users>(usersJson);
        }

        public static async Task<List<Dialog>> GetDialogs(Tokens tokens)
        {
            HttpClient headerApi = new HttpClient()
            {
                BaseAddress = api.BaseAddress
            };

            headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            HttpResponseMessage res = await headerApi.GetAsync("messages/dialogs");

            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Tokens newTokens = await Refresh(tokens.RefreshToken);
                if (newTokens != null)
                {
                    headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newTokens.AccessToken);
                    res = await headerApi.GetAsync("messages/dialogs");
                }
                else
                    return null;
            }
            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            string usersJson = await res.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Dialog>>(usersJson);
        }

        public static async Task<Dialog> GetDialog(Tokens tokens, int id)
        {
            HttpClient headerApi = new HttpClient()
            {
                BaseAddress = api.BaseAddress
            };

            headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            HttpResponseMessage res = await headerApi.GetAsync($"messages/dialogs/{id}");

            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Tokens newTokens = await Refresh(tokens.RefreshToken);
                if (newTokens != null)
                {
                    headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newTokens.AccessToken);
                    res = await headerApi.GetAsync($"messages/dialogs/{id}");
                }
                else
                    return null;
            }
            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            string usersJson = await res.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Dialog>(usersJson);
        }

        public static async Task<bool> Send(Tokens tokens, int dialogId, string text)
        {
            StringContent data = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    dialogId,
                    text,
                }),
                Encoding.UTF8,
                "application/json"
            );

            HttpClient headerApi = new HttpClient()
            {
                BaseAddress = api.BaseAddress
            };

            headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            HttpResponseMessage res = await headerApi.PostAsync("messages/send", data);

            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Tokens newTokens = await Refresh(tokens.RefreshToken);
                if (newTokens != null)
                {
                    headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newTokens.AccessToken);
                    res = await headerApi.PostAsync("messages/send", data);
                }
                else
                    return false;
            }
            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return false;

            return true;
        }

        public static async Task<bool> CreateDialog(Tokens tokens, List<int> usersIds)
        {
            StringContent data = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    usersIds
                }),
                Encoding.UTF8,
                "application/json"
            );

            HttpClient headerApi = new HttpClient()
            {
                BaseAddress = api.BaseAddress
            };

            headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            HttpResponseMessage res = await headerApi.PostAsync("messages/createDialog", data);

            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Tokens newTokens = await Refresh(tokens.RefreshToken);
                if (newTokens != null)
                {
                    headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newTokens.AccessToken);
                    res = await headerApi.PostAsync("messages/createDialog", data);
                }
                else
                    return false;
            }
            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return false;

            return true;
        }
    }
}

using Bigeny.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Bigeny.Http
{
    internal class Api
    {
        public static readonly string BaseIpAddress = "https://www.bigeny.ru";

        public static HttpClient api = new HttpClient()
        {
            BaseAddress = new Uri($"{BaseIpAddress}/"),
        };

        public static async Task<string> TokenyzeGet(string url, Tokens tokens)
        {
            try
            {
                HttpClient headerApi = new HttpClient()
                {
                    BaseAddress = api.BaseAddress
                };
                headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
                HttpResponseMessage res = await headerApi.GetAsync(url);
                if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Tokens newTokens = await AuthApi.Refresh(tokens.RefreshToken);
                    if (newTokens != null)
                    {
                        headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newTokens.AccessToken);
                        res = await headerApi.GetAsync(url);
                    }
                    else
                        return null;
                }
                if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return null;
                return await res.Content.ReadAsStringAsync();
            } catch
            {
                return null;
            }            
        }

        public static async Task<string> TokenyzePost(string url, HttpContent body, Tokens tokens)
        {
            try
            {

                HttpClient headerApi = new HttpClient()
                {
                    BaseAddress = api.BaseAddress
                };

                headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
                HttpResponseMessage res = await headerApi.PostAsync(url, body);

                if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Tokens newTokens = await AuthApi.Refresh(tokens.RefreshToken);
                    if (newTokens != null)
                    {
                        headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newTokens.AccessToken);
                        res = await headerApi.PostAsync(url, body);
                    }
                    else
                        return null;
                }
                if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return null;
                return await res.Content.ReadAsStringAsync();
            } catch
            {
                return null;
            }
        }

        public static async Task<string> TokenyzePut(string url, HttpContent body, Tokens tokens)
        {
            try
            {

                HttpClient headerApi = new HttpClient()
                {
                    BaseAddress = api.BaseAddress
                };

                headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
                HttpResponseMessage res = await headerApi.PutAsync(url, body);

                if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Tokens newTokens = await AuthApi.Refresh(tokens.RefreshToken);
                    if (newTokens != null)
                    {
                        headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newTokens.AccessToken);
                        res = await headerApi.PutAsync(url, body);
                    }
                    else
                        return null;
                }
                if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return null;
                return await res.Content.ReadAsStringAsync();
            } catch
            {
                return null;
            }
        }
    }
}

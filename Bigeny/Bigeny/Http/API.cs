using Bigeny.Models;
using Bigeny.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Bigeny.Http
{
    internal class Api
    {
        //public static readonly string BaseIpAddress = "45.132.1.89";
        public static readonly string BaseIpAddress = "192.168.0.101";

        public static HttpClient api = new HttpClient()
        {
            BaseAddress = new Uri($"http://{BaseIpAddress}:3000/"),
        };

        public static async Task<string> TokenyzeGet(string url, Tokens tokens)
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
        }

        public static async Task<string> TokenyzePost(string url, HttpContent body, Tokens tokens)
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
        }

        public static async Task<string> TokenyzePut(string url, HttpContent body, Tokens tokens)
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
        }
    }
}

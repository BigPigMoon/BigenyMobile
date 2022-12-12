using Bigeny.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bigeny.Http
{
    internal class UserApi
    {
        public static async Task<List<User>> GetUsers(Tokens tokens)
        {
            return JsonConvert.DeserializeObject<List<User>>(await Api.TokenyzeGet("users", tokens));
        }

        public static async Task<User> GetMe(Tokens tokens)
        {
            return JsonConvert.DeserializeObject<User>(await Api.TokenyzeGet("users/me", tokens));
        }

        public static async Task<User> UpdateAvatar(Tokens tokens, string filename)
        {
            var data = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    filename
                }),
                Encoding.UTF8,
                "application/json"
            );
            return JsonConvert.DeserializeObject<User>(await Api.TokenyzePut("users/updateAvatar", data, tokens));
        }

        public static async Task<User> ChangeName(Tokens toknes, string nickname)
        {
            StringContent data = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    nickname,
                }),
                Encoding.UTF8,
                "application/json"
            );

            return JsonConvert.DeserializeObject<User>(await Api.TokenyzePut("users/changeNickname", data, toknes));
        }
    }
}

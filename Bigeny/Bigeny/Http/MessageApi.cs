using Bigeny.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bigeny.Views;

namespace Bigeny.Http
{
    internal class MessageApi
    {
        public static async Task<List<Dialog>> GetDialogs(Tokens tokens)
        {
            return JsonConvert.DeserializeObject<List<Dialog>>(await Api.TokenyzeGet("messages/dialogs", tokens));
        }

        public static async Task<bool> CreateDialog(Tokens tokens, List<int> users, string name, string avatar)
        {
            StringContent data = new StringContent(
                JsonConvert.SerializeObject(new {
                    users,
                    name,
                    avatar
                }),
                Encoding.UTF8,
                "application/json"
            );
            return JsonConvert.DeserializeObject<bool>(await Api.TokenyzePost("messages/createDialog", data, tokens));
        }

        public static async Task<Dialog> GetDialog(Tokens tokens, int id)
        {
            return JsonConvert.DeserializeObject<Dialog>(await Api.TokenyzeGet($"messages/dialogs/{id}", tokens));
        }

        public static async Task<List<Message>> GetMessages(Tokens tokens, int id)
        {
            return JsonConvert.DeserializeObject<List<Message>>(await Api.TokenyzeGet($"messages/{id}", tokens));
        }

        public static async Task<string> Send(Tokens tokens, int dialogId, string content)
        {
            StringContent data = new StringContent(
                JsonConvert.SerializeObject(new {
                    dialogId,
                    content
                }
            ), Encoding.UTF8, "application/json");
            string res = await Api.TokenyzePost("messages/send", data, tokens);
            return res;
        }
    }
}

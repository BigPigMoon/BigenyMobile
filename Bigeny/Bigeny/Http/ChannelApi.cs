using Bigeny.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bigeny.Http
{
    public class ChannelApi
    {
        public static async Task<bool> CreateChannel(Tokens tokens, string name, string description, string avatar)
        {
            StringContent data = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    description,
                    avatar,
                    name,
                }),
                Encoding.UTF8,
                "application/json"
            );
            return JsonConvert.DeserializeObject<bool>(await Api.TokenyzePost("channels/create", data, tokens));
        }

        public static async Task<List<Channel>> GetChannels(Tokens tokens)
        {
            return JsonConvert.DeserializeObject<List<Channel>>(await Api.TokenyzeGet("channels/", tokens));
        }

        public static async Task<Channel> GetChannel(Tokens tokens, int channelId)
        {
            return JsonConvert.DeserializeObject<Channel>(await Api.TokenyzeGet($"channels/{channelId}", tokens));
        }

        public static async Task<List<Channel>> GetSubsChannels(Tokens tokens)
        {
            return JsonConvert.DeserializeObject<List<Channel>>(await Api.TokenyzeGet("channels/subs", tokens));
        }

        public static async Task<bool> Subscribe(Tokens tokens, int channelId)
        {
            StringContent data = new StringContent(
                JsonConvert.SerializeObject(new {}),
                Encoding.UTF8,
                "application/json"
            );

            return JsonConvert.DeserializeObject<bool>(await Api.TokenyzePost($"channels/sub/{channelId}", data, tokens));
        }
    }
}

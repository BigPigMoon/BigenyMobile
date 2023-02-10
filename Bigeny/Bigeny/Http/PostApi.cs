using Bigeny.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bigeny.Http
{
    public class PostApi
    {
        public static async Task<Post> CreatePost(Tokens tokens, string content, string image, int channelId)
        {
            StringContent data = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    content,
                    image,
                    channelId,
                }),
                Encoding.UTF8,
                "application/json"
            );
            return JsonConvert.DeserializeObject<Post>(await Api.TokenyzePost("posts/create", data, tokens));
        }

        public static async Task<List<Post>> GetPosts(Tokens tokens)
        {
            return JsonConvert.DeserializeObject<List<Post>>(await Api.TokenyzeGet("posts/", tokens));
        }

        public static async Task<Post> GetPost(Tokens tokens, int id)
        {
            return JsonConvert.DeserializeObject<Post>(await Api.TokenyzeGet($"posts/{id}", tokens));
        }

        public static async Task<List<Post>> GetPostsFromChannel(Tokens tokens, int channelId)
        {
            return JsonConvert.DeserializeObject<List<Post>>(await Api.TokenyzeGet($"posts/channel/{channelId}", tokens));
        }

        public static async Task<List<Post>> GetPostFromSubscribesChannel(Tokens tokens)
        {
            return JsonConvert.DeserializeObject<List<Post>>(await Api.TokenyzeGet("posts/subs", tokens));
        }

        public static async Task<bool> SetPostRate(Tokens tokens, int postId, bool positive)
        {
            StringContent data = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    positive
                }),
                Encoding.UTF8,
                "application/json"
            );

            return JsonConvert.DeserializeObject<bool>(await Api.TokenyzePost($"posts/rate/{postId}", data, tokens));
        }

        public static async Task<PostRate> GetPostRate(Tokens tokens, int postId)
        {
            return JsonConvert.DeserializeObject<PostRate>(await Api.TokenyzeGet($"posts/rate/{postId}", tokens));
        }
    }
}

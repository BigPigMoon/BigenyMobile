using Bigeny.Http;
using Bigeny.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarians.CropImage;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bigeny.Services
{
    internal class StorageService
    {
        public static async Task<string> Upload()
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };

                var pickedImg = await FilePicker.PickAsync(new PickOptions { FileTypes = FilePickerFileType.Images, PickerTitle = "Pick the image" });
                if (pickedImg == null) return null;
                var cropResult = await CropImageService.Instance.CropImage(pickedImg.FullPath, CropRatioType.Square);
                var stream = await pickedImg.OpenReadAsync();
                var filename = pickedImg.FileName;

                var formData = new MultipartFormDataContent
                {
                    { new StreamContent(stream), "image", filename }
                };
                return await Api.TokenyzePost("store/upload", formData, tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static object Download(string avatar)
        {
            if (avatar == null || avatar.Length == 0) return "notfound.png";
            return new UriImageSource
            {
                Uri = new Uri($"http://{Api.BaseIpAddress}:3000/store/download/" + avatar),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(10, 0, 0, 0)
            };
        }
    }
}

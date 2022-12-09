using Bigeny.Http;
using Bigeny.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarians.CropImage;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bigeny.Services
{
    internal class UsersService
    {
        public static async Task<Users> GetMe()
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await API.GetMe(tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<List<Users>> GetUsers()
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await API.GetUsers(tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<List<Users>> GetUsersNotme()
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await API.GetUsersNotme(tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<Users> UploadAvatar()
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

                return await API.UploadAvatar(tok, stream, filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static object GetAvatar(Users user)
        {
            if (user.AvatarImg == null || user.AvatarImg.Length == 0) return "notfound.png";
            return new UriImageSource { Uri = new Uri("http://45.132.1.89:3000/users/downloadAvatar/" + user.AvatarImg), CachingEnabled=true, CacheValidity=new TimeSpan(10, 0,0,0)};
        }
    }
}

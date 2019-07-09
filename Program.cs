using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace InstaApi
{
    class Program
    {
        #region Hide
        static string name = "myName";
        static string pass = "myPass123";
        #endregion

        private static UserSessionData user;
        private static IInstaApi api;
        static void Main(string[] args)
        {
            user = new UserSessionData();
            user.UserName = name;
            user.Password = pass;

            Login();

            Console.ReadKey();
        }
        
        public static async void Login()
        {
            api = InstaApiBuilder.CreateBuilder()
                .SetUser(user)
                .UseLogger(new DebugLogger(LogLevel.Exceptions))
                .SetRequestDelay(RequestDelay.FromSeconds(2, 2))
                .Build();

            var loginRequest = await api.LoginAsync();
            if (loginRequest.Succeeded)
            {
                Console.WriteLine("Logged in");
                //Methods
                Search("userName");
                GetHashtagInfo("");
                //GetLocationFeed();
               //GetUserAsync();
            }
            else Console.WriteLine($"Logon error: {loginRequest.Info.Message}");

        }
        public static async void Search(string word) 
        {
            var search = await api.SearchUsersAsync(word);

            foreach (var item in search.Value)
            {
                Console.WriteLine(item.UserName);
            }
        }

        public static void GetHashtagInfo(string tag)
        {
            var info = api.GetHashtagInfo(tag);
        }
    }
}

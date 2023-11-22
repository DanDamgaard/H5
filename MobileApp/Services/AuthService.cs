using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public class AuthService
    {
        public async Task<bool> isAuthenticatedAsync()
        {
            bool loginState = Preferences.Default.Get("loginState", false);
            await Task.Delay(2000);
            return loginState;
        }

        public void login()
        {
            Preferences.Default.Set("loginState", true);
        }

        public void logout()
        {
            Preferences.Default.Remove("loginState");
            Preferences.Default.Remove("EmailKey");
            Preferences.Default.Remove("PassKey");

        }
    }
}

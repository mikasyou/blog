using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Shared.Utils {
    public class DataTools {
        const string GravatarUrl = "https://www.gravatar.com/avatar/";


        public static string MakeGravatarImage(string email) {
            var md5 = MD5.Create().ComputeHash(Encoding.Default.GetBytes(email.ToLower()));
            var hash = BitConverter.ToString(md5).Replace("-", "").ToLower();
            return DataTools.GravatarUrl + hash;
        }


        public static string MakeWebTitle(string title, bool urlEncoded = false) {
            if (urlEncoded)
                return WebUtility.UrlEncode(title + " | Kaakira");

            return title + " | Kaakira";
        }
    }
}
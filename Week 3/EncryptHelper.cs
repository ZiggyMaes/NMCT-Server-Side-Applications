using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Security;

namespace Week2Demo3Cookies {
    public class EncryptHelper {

        const string Anonymous = "<anonymous>";

        public static string GetMachineKeyPurpose(IPrincipal user) {
            return String.Format("CookieDemo:Username:{0}",
                user.Identity.IsAuthenticated ? user.Identity.Name : Anonymous);
        }

        public static string Encrypt(byte[] data) {
            if (data == null || data.Length == 0) return null;


            var purpose = GetMachineKeyPurpose(Thread.CurrentPrincipal);
            var value = MachineKey.Protect(data, purpose);
            return Convert.ToBase64String(value);
        }

        public static byte[] Decrypt(string value) {
            if (String.IsNullOrWhiteSpace(value)) return null;


            var purpose = GetMachineKeyPurpose(Thread.CurrentPrincipal);
            var bytes = Convert.FromBase64String(value);
            return MachineKey.Unprotect(bytes, purpose);
        }
    }
}

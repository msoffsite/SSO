using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSOService.Helpers
{
    public class Constants
    {

        internal static string Invalid { get; } = "Invalid:";

        internal static string ConnectionName { get; } = "defaultConnectionString";

        internal static string ServiceEventLog { get; } = "PTSServiceEventLog";

        public const string HttpBindingGet = "GET";
        public const string HttpBindingPost = "POST";

        public const string HttpRolesSsoAdmin = "SSO Admin";

    }
}

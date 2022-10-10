using NUnit.Framework;
using TechCon.Tests.Utils.Extensions;

namespace TechCon.Tests.Data
{
    public static class Params
    {
        public static string PortalUrl => GetParameter(nameof(PortalUrl));
        public static string BrowserName => GetParameter(nameof(BrowserName));
        public static string Username => GetParameter(nameof(Username));
        public static string Password => GetParameter(nameof(Password));
        public static int ActionTimeout => GetParameter(nameof(ActionTimeout)).ToInt();
        public static bool TraceView => GetParameter(nameof(TraceView)).ToBool();

        private static string GetParameter(string parameterName)
        {
            var ErrorMsg = $"No value found for parameter '{parameterName}'. " +
                            "Make sure your selected runsettings file contains an entry " +
                            "for this parameter, and that the spelling/casing is correct.";

            return TestContext.Parameters[parameterName]?? throw new ArgumentException(ErrorMsg);
        }
    }
}

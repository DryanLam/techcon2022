using System.Text;

namespace TechCon.Tests.Utils.Helpers
{
    public static class IOHelpers
    {
        public static string ProjectPath => GetProjectPath();
        public static string DownloadPath => GetDownLoadPath();
        private static string GetProjectPath()
        {
            string prjPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            return prjPath;
        }

        private static string GetDownLoadPath()
        {
            string prjPath = ProjectPath;
            var downloadPath = Path.Combine(prjPath, "Downloads");
            bool folderExists = Directory.Exists(downloadPath);
            if (!folderExists)
            {
                Directory.CreateDirectory(downloadPath);
            }
            return downloadPath;
        }

        public static string EncodeBase64String(string text)
        {
            return Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(text));
        }
    }
}

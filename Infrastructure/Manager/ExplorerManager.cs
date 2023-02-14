using System.IO;

namespace ChatbotConstructorTelegram.Infrastructure.Manager
{
    internal class ExplorerManager
    {
        #region Debug

        public static readonly string? LocationProject;
        public static readonly string? LocationExe;
        public static readonly string LocationListProjects;
        public static readonly string LocationEnv;
        public static readonly string LocationWorkEnvironment;
        public static readonly string LocationArchiveAiogram;
        public static readonly string NameArchiveAiogram;
        private const int LEVELUPPROJECT = 3;

        #endregion

        static ExplorerManager()
        {
            LocationProject = GetLocation(LEVELUPPROJECT);
            LocationExe = GetLocation(0);
            LocationArchiveAiogram = LocationProject + "\\LibraryPython";
            NameArchiveAiogram = "aiogram-2.23.tar.gz";
            LocationEnv = LocationExe + "\\Environment";
            LocationListProjects = LocationExe+"\\Projects\\list_projects.config";
            LocationWorkEnvironment = LocationExe + "\\Projects\\statusWorkEnvironment.config";
        }

        private static string? GetLocation(int levelupproject)
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(location);

            if (levelupproject != 0)
            {
                for (int i = 0; i < levelupproject; i++)
                    path = Path.GetDirectoryName(path);
            }

            return path;
        }
    }
}

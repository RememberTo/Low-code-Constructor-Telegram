using System.IO;

namespace ChatbotConstructorTelegram.Infrastructure.Python;

internal class PythonHelper
{

    public static async void WriteCodeFileAsync(string path, string code, FileMode fileMode)
    {
       
        await using var fileStream = new FileStream(path, fileMode);
        await using var output = new StreamWriter(fileStream);
        await output.WriteAsync(code);
    }

    private string ReadCodeFile(string pathToFile)
    {
        var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
        var path = Path.GetDirectoryName(location);

        for (int i = 0; i < 3; i++)
        {
            path = Path.GetDirectoryName(path);
        }

        path += pathToFile;
        var code = string.Empty;
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        using StreamReader reader = new StreamReader(fs);
        code = reader.ReadToEnd();

        return code;
    }

}
namespace ChatbotConstructorTelegram.Infrastructure.Python;

internal class Decorator
{
    public string NameFunc { get; set; }
    public string TypeParameters { get; set; }
    public string? Parameter { get; set; }

    public Decorator(string name, string typeparam, string? param)
    {
        NameFunc = name;
        TypeParameters = typeparam;
        Parameter = param;
    }

}
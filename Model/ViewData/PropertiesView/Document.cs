using ChatbotConstructorTelegram.ViewModels.Base;
using System.Text;

namespace ChatbotConstructorTelegram.Model.ViewData.PropertiesView
{
    public class Document : ViewModel
    {
        private string? _path;
        public string? Path
        {
            get
            {
                if (string.IsNullOrEmpty(_path) == false)
                {
                    var sb = new StringBuilder();
                    foreach (var t in _path)
                    {
                        if (t == '\\')
                            sb.Append(@"\\");
                        else
                            sb.Append(t);
                    }

                    return sb.ToString();
                }

                return _path;
            }
            set => Set(ref _path, value);
        }
        private string? _caption;

        public string? Caption
        {
            get => _caption;
            set => Set(ref _caption, value);
        }
    }
}

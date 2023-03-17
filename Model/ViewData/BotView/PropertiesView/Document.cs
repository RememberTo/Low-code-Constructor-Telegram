using System.Text;
using ChatbotConstructorTelegram.ViewModels.Base;

namespace ChatbotConstructorTelegram.Model.ViewData.BotView.PropertiesView
{
    public class Document : ViewModel, IPropertyFile
    {
        private string? _path;
        public string? Path
        {
            get
            {
                //if (string.IsNullOrEmpty(_path) == false)
                //{
                //    var sb = new StringBuilder();
                //    foreach (var t in _path)
                //    {
                //        if (t == '\\')
                //            sb.Append(@"\\");
                //        else
                //            sb.Append(t);
                //    }

                //    return sb.ToString();
                //}

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

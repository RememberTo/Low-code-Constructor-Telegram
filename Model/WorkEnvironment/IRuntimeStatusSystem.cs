using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotConstructorTelegram.Model.WorkEnvironment
{
    internal interface IRuntimeStatusSystem
    {
        public bool IsExistPython { get; set; }
        public bool IsExistEnvironment { get; set; }
        public bool IsExistLibrary { get; set; }
    }
}

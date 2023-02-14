using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotConstructorTelegram.Model.WorkEnvironment
{
    internal class WorkEnvironmentException : Exception
    {
        public string SystemState { get; set; }

        public WorkEnvironmentException(string state)
        {
            SystemState = state;
        }
    }
}

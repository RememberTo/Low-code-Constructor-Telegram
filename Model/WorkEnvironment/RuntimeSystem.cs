using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotConstructorTelegram.Model.WorkEnvironment
{
    [DataContract]
    public class RuntimeSystem : IRuntimeStatusSystem
    {
        [DataMember]
        public Environment? Environment { get; set; }

        [DataMember]
        public PythonInformation? PythonInfo { get; set; }

        [DataMember]
        public PythonLibraryInformation? PythonLibraryInfo { get; set; }

        [DataMember]
        public bool IsExistPython { get; set; }

        [DataMember]
        public bool IsExistEnvironment { get; set; }

        [DataMember]
        public bool IsExistLibrary { get; set; }

        public RuntimeSystem()
        {

            try
            {
                Environment = new Environment();
                PythonInfo = new PythonInformation();
                PythonLibraryInfo = new PythonLibraryInformation();

                //PythonInfo.IsInstalled = true;
                //PythonLibraryInfo.IsInstalled = true;
                //PythonInfo.PipVersion = "Pip None";
                //PythonInfo.Version = "Python None";
                //Environment.Name = ".bot";
                //Environment.Path = "C:\\user";
                //PythonLibraryInfo.NameLibrary = "aiogramm";
                //PythonLibraryInfo.Version = "None";

                // check python 
                // create or check env
                // add or check librarry aiogramm
            }
            catch (WorkEnvironmentException e)
            {

            }
            catch (Exception)
            {

            }
        }
    }
}

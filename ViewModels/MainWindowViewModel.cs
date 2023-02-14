using ChatbotConstructorTelegram.Infrastructure.Commands;
using ChatbotConstructorTelegram.Infrastructure.Manager;
using ChatbotConstructorTelegram.Model.ViewData;
using ChatbotConstructorTelegram.ViewModels.Base;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using ChatbotConstructorTelegram.Model.WorkEnvironment;


namespace ChatbotConstructorTelegram.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private string _title = "Конструктор бота";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private string _status = "Готово";

        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        private List<NoteProject> _recordsProject = null!;

        public List<NoteProject> RecordsProject
        {
            get => _recordsProject;
            set => Set(ref _recordsProject, value);
        }
        #region Command

        public ICommand CloseApplicationCommand { get; }

        private bool OnCloseApplicationCommandExecute(object p)
        {
            return true;
        }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion


        public MainWindowViewModel()
        {
            #region Command

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, OnCloseApplicationCommandExecute);

            #endregion

            try
            {
                RuntimeSystemManager.CheckSystem();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            

            RecordsProject = GetParseRecordsProjects();


            Log.Info("Главное окно успешно запущено");
        }

        public MainWindowViewModel(string filePath)
        {
            #region Command

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, OnCloseApplicationCommandExecute);

            #endregion



            RecordsProject = GetParseRecordsProjects();

            Log.Info("Главное окно успешно запущено");
        }

        private List<NoteProject> GetParseRecordsProjects()
        {
            var note = new List<NoteProject>();
            const string patternNameFile = @".*?\.";
            const string patternPathFile = @".*?%";
            const string patternDate = @"%.*?%";

            using var streamReader = new StreamReader(new FileStream(ExplorerManager.LocationListProjects, FileMode.Open, FileAccess.Read));
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();

                if (line != null)
                    note.Add(new NoteProject()
                    {
                        ProjectName = GetSearchStringPattern(patternNameFile, line.Substring(line.LastIndexOf(@"\", StringComparison.Ordinal))),
                        Path = GetSearchStringPattern(patternPathFile, line),
                        Date = Convert.ToDateTime(GetSearchStringPattern(patternDate, line))
                    });
            }

            return note;
        }

        private string? GetSearchStringPattern(string pattern, string s)
        {
            var regex = new Regex(pattern);
            var matches = regex.Matches(s);
            if (matches.Count > 0)
            {
                return matches[0].Value.Substring(1, matches[0].Value.Length - 2);
            }
            return null;
        }

    }
}

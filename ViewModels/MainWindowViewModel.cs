using ChatbotConstructorTelegram.Infrastructure.Commands;
using ChatbotConstructorTelegram.Infrastructure.Manager;
using ChatbotConstructorTelegram.Model.ViewData;
using ChatbotConstructorTelegram.ViewModels.Base;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ChatbotConstructorTelegram.Model.WorkEnvironment;


namespace ChatbotConstructorTelegram.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly DispatcherTimer _timer;

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

        private ObservableCollection<NoteProject> _recordsProject = null!;

        public ObservableCollection<NoteProject> RecordsProject
        {
            get => _recordsProject;
            set => Set(ref _recordsProject, value);
        }

        private NoteProject _selectedNoteProject;

        public NoteProject SelectedNoteProject
        {
            get => _selectedNoteProject;
            set => Set(ref _selectedNoteProject, value);
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

        public ICommand DeleteCommand { get; }

        private bool OnDeleteCommandExecute(object p)
        {
            return p is NoteProject;
        }

        private void OnDeleteCommandExecuted(object p)
        {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить проект?", "Заголовок", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var noteProj = (NoteProject)p;
                RecordsProject.Remove(noteProj);
                //Set(ref _recordsProject, RecordsProject);

                if (!File.Exists(noteProj.Path)) return;

                File.Delete(noteProj.Path);
                if (File.Exists(noteProj.Path.Replace(".xml", ".py")))
                    File.Delete(noteProj.Path.Replace(".xml", ".py"));

                var sb = new StringBuilder();

                using (var streamReader = new StreamReader(new FileStream(ExplorerManager.LocationListProjects, FileMode.Open, FileAccess.Read)))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var line = streamReader.ReadLine();
                        if (!line.Contains(noteProj.Path))
                            sb.AppendLine(line);
                    }
                }

                using (var streamWriter = new StreamWriter(ExplorerManager.LocationListProjects, false))
                {
                    streamWriter.WriteLineAsync(sb.ToString());
                }

                SetStatusStartTimer($"Проект {noteProj.ProjectName} удален");
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            #region Command

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, OnCloseApplicationCommandExecute);
            DeleteCommand = new LambdaCommand(OnDeleteCommandExecuted, OnDeleteCommandExecute);
            #endregion

            try
            {
                RuntimeSystemManager.CheckSystem();

            }
            catch (WorkEnvironmentException e)
            {
                MessageBox.Show(e.SystemState);
            }
            finally
            {
                RecordsProject = GetParseRecordsProjects();

                Log.Info("Главное окно успешно запущено");
                _timer = new DispatcherTimer();
                _timer.Tick += Timer_Tick;
                _timer.Interval = new TimeSpan(0, 0, 5);
            }
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            Status = "Готово";
        }

        private void SetStatusStartTimer(string s)
        {
            Status = s;
            _timer.Start();
        }
        public MainWindowViewModel(string filePath)
        {
            #region Command

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, OnCloseApplicationCommandExecute);

            #endregion

            RecordsProject = GetParseRecordsProjects();

            Log.Info("Главное окно успешно запущено");
        }

        private ObservableCollection<NoteProject> GetParseRecordsProjects()
        {
            var note = new ObservableCollection<NoteProject>();
            const string patternNameFile = @".*?\.";
            const string patternPathFile = @".*?%";
            const string patternDate = @"%.*?%";

            using var streamReader = new StreamReader(new FileStream(ExplorerManager.LocationListProjects, FileMode.Open, FileAccess.Read));
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();

                if (!string.IsNullOrEmpty(line))
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

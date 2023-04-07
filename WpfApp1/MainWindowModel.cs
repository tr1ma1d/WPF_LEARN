using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows.App;
using System.Windows;

namespace WpfApp1
{
    class MWP : ViewModel
    {
        private string f_Text;

        public string Text
        {
            get => f_Text;
            set => Set(ref f_Text, value);
        }
        private string f_FileName;
        private static int p;

        public string FileName
        {
            get => f_FileName;
            set
            {
                if(Set(ref f_FileName, value))
                {
                    ReadFileAsync(value);
                }
            }
        }

        public ICommand CreateCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ExitCommnad { get; } = new LamdaCommand(p => Application.Current.Shutdown());
        public MWP()
        {
            CreateCommand = new LamdaCommand(OnCreateCommandExecuted);
            SaveCommand = new LamdaCommand(OnSaveCommandExecutedAcync, OnSaveCommandCanExecuted);
        }
       

        private async void OnSaveCommandExecutedAcync(object FilePath)
        {
            var file_name = FilePath as string;
            if(file_name == null)
            {
                var dialog = new SaveFileDialog()
                {
                    Title = "SaveFile",
                    Filter = "Text file (*.txt)|*.txt| AllFiles (*.*)|(*.*)",
                    InitialDirectory = Environment.CurrentDirectory,
                    RestoreDirectory = true,
                };
                if (dialog.ShowDialog() != true) return;
                file_name = dialog.FileName;
            }

            using (var writer = new StreamWriter(new FileStream(file_name, FileMode.Create, FileAccess.Write))) 
                await writer.WriteAsync(f_Text).ConfigureAwait(true);
            FileName = file_name;
        }
        private bool OnSaveCommandCanExecuted(object FilePath)
        {
            return !string.IsNullOrEmpty(f_Text);
        }


        private void OnCreateCommandExecuted(object p)
        {
            Text = "";
            FileName = null;
        }


        private async void ReadFileAsync(string FilePath)
        {
            if(!File.Exists(FilePath)) {
                return;       
            }
            using(var reader = File.OpenText(FilePath))
            {
                Text = await reader.ReadToEndAsync().ConfigureAwait(true);
            }
        }
    }
}
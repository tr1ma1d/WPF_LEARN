using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows;

namespace WpfApp1
{
    class OpenD : Freezable
    {

      

        public string Title { 
            get => (string)GetValue(TitleProperty); 
            set => SetValue(TitleProperty, value);
        }
       
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
          nameof(Title),
          typeof(string),
          typeof(OpenD),
          new PropertyMetadata(default(string)));
        
        
        public string Filter
        {
            get => (string)GetValue(FilterProperty);
            set => SetValue(FilterProperty, value);
        }

        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register(
          nameof(Filter),
          typeof(string),
          typeof(OpenD),
          new PropertyMetadata("Text file (*.txt)|*.txt| AllFiles (*.*)|(*.*)"));
        

        public string SelectedFile
        {
            get => (string)GetValue(SelectedFileProperty);
            set => SetValue(SelectedFileProperty, value);
        }

        public static readonly DependencyProperty SelectedFileProperty = DependencyProperty.Register(
          nameof(SelectedFile),
          typeof(string),
          typeof(OpenD),
          new PropertyMetadata(default(string)));


        public ICommand OpenCommand
        {
            get;
        }

        public OpenD()
        {
            OpenCommand = new LamdaCommand(OnOpenCommandExecuted);
        }
        void OnOpenCommandExecuted(object p)
        {
            var dlg = new OpenFileDialog
            {
                Title = Title,
                Filter = Filter,
                RestoreDirectory = true,
                InitialDirectory = Environment.CurrentDirectory
            
            };
            if(dlg.ShowDialog() != true) return;

            SelectedFile = dlg.FileName;
        }

        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }
    }
}

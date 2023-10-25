using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wordle_BL;

namespace Worlde_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public List<string> words = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Progress { get; set; }
        private int _progress 
        {
            get { return _progress; }
            set { _progress = value; NotifyPropertyChange("Progress"); }
        }
        private BackgroundWorker _worker;


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _worker = new()
            {
                WorkerReportsProgress = true,
            };
        }

        private void Vælg_fil_knap_Click(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                if (filePath != string.Empty)
                {
                    Path_label.Text = System.IO.Path.GetFileName(filePath);
                }

                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    while (reader.Peek() >= 0)
                    {
                        words.Add(reader.ReadLine());
                    }
                }
            }
        }

        public event EventHandler<int> update;

        private void Start_knap_Click(object sender, RoutedEventArgs e)
        {
            Binary B = new(words);
            Algorithm A = new();
            List<List<int>> binaryWords = A.Run(B.bitsWords);
            Path_label_2.Text = binaryWords.Count.ToString();
            _worker.DoWork += UpdateProgressBar;
        }

        private void UpdateProgressBar(object sender, int procent)
        {
            Progress = procent;
        }
    }
}

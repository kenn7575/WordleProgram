using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
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
        public List<List<string>> combinations = new();
        private string _filePath = string.Empty;
        private OpenFileDialog _openFileDialog = new();
        private Stream _fileStream;


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Progress
        {
            get { return _progress; }
            set { _progress = value; NotifyPropertyChange("Progress"); }
        }
        private int _progress { get; set; }
        private BackgroundWorker _worker;


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _worker = new();
            _worker.DoWork += new DoWorkEventHandler(WorkerDowork);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerCompleteWork);
            _worker.WorkerReportsProgress = true;
            _worker.ProgressChanged += new ProgressChangedEventHandler(UpdateProgressBar);
        }

        private void Vælg_fil_knap_Click(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;

            if (_openFileDialog.ShowDialog() == true)
            {
                _filePath = _openFileDialog.FileName;
                if (_filePath != string.Empty)
                {
                    Path_label.Text = System.IO.Path.GetFileName(_filePath);
                }

                _fileStream = _openFileDialog.OpenFile();
            }
        }

        public List<List<int>> BinaryWords { get; set; } = new();
        private void Start_knap_Click(object sender, RoutedEventArgs e)
        {
            _worker.RunWorkerAsync();
        }

        public void WorkerDowork(object? sender, DoWorkEventArgs e)
        {
            using (StreamReader reader = new StreamReader(_fileStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.ToLower();
                    if (line.Length != 5) continue;
                    if (line.Distinct().Count() != 5) continue;
                    if (words.Where(x => string.Concat(x, line).Distinct().Count() == 5).Count() > 0) continue;
                    words.Add(line);
                }
            }
            DataProcessing dp = new(words);
            Binary B = new(dp.words);
            Algorithm A = new();
            //A.handlePlz += UpdateProgressBar;
            BinaryWords = A.Run(B.bitsWords, _worker);
            combinations = B.ConvertBitWord(BinaryWords);
        }

        public void workerCompleteWork(object? sender, RunWorkerCompletedEventArgs e)
        {
            Path_label_2.Text = BinaryWords.Count.ToString();
        }

        private void UpdateProgressBar(object? sender, ProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
        }

        private async void saveFile_Click(object sender, RoutedEventArgs e)
        {
            if (combinations.Count() == 0)
            {
                return;
            }
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, "WriteTextAsync.txt")))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

                string combinationString = "";
                foreach(List<string> combination in combinations)
                {
                    foreach (string word in combination)
                    {
                        combinationString += word + " ";
                    }
                    combinationString += "\n";
                }
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        File.WriteAllText(filePath, combinationString);
                        // Handle success
                    }
                    catch (Exception ex)
                    {
                        // Handle error
                    }
                }
            }
        }
    }

}

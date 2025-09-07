using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfApp.Commands;

namespace WpfApp.ViewModels
{
    internal class TitleViewModel : INotifyPropertyChanged
    {
        private TitleModel _model;
        public string TitleName => _model.TitleName;
        public ICommand ShowMessageCommand { get; }

        public TitleViewModel()
        {
            _model = new TitleModel { TitleName = "C#とSQLite(ついでにWPF)習熟プロジェクト" };
            ShowMessageCommand = new RelayCommand(_ => MessageBox.Show("test"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

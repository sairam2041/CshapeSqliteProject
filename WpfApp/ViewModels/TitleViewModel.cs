using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfApp.Commands;

namespace WpfApp.ViewModels
{
    internal class TitleViewModel : INotifyPropertyChanged
    {
        private TitleModel _model;
        private string _titleName;
        public string TitleName
        {
            get => _titleName;
            set
            {
                if (_titleName != value)
                {
                    _titleName = value;
                    OnPropertyChanged(nameof(TitleName)); // UIが更新される
                }
            }
        }

        public ICommand ShowMessageCommand { get; }

        public TitleViewModel()
        {
            _model = new TitleModel { TitleName = "C#とSQLite(ついでにWPF)習熟プロジェクト" };
            _titleName = _model.TitleName;
            //ShowMessageCommand = new RelayCommand(_ => MessageBox.Show("test"));
            ShowMessageCommand = new RelayCommand(_ =>
            {
                _model.TitleName = "ちゃんと変わった？";
                TitleName = _model.TitleName;
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

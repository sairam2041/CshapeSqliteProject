using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.ViewModels.Base;

namespace WpfApp.ViewModels.Implementations
{
    internal class TitleViewModel : BaseViewModel
    {
        private TitleModel _model;
        private string _titleName;
        public string TitleName
        {
            get => _titleName;
            set => SetProperty(ref _titleName, value);
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
    }
}

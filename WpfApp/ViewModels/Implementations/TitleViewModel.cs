using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.Models;
using WpfApp.Repositories.Implementations;
using WpfApp.ViewModels.Base;
using WpfApp.Repositories.Interfaces;

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
        public ICommand ReplaceTable { get; }

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

            ReplaceTable = new RelayCommand(_ =>
            {
                var dao = new SampleDao("sample.db","sample","sample.csv");
                var dao2 = new SampleDao("sample2.db", "sample", "sample.csv");

                var test = new TableReplaceModel(new List<ITableDataReplaceDao>() { dao, dao2 });

                test.ExecuteRelpace();
            });
        }
    }
}

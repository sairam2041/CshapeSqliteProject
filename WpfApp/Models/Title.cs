using System.ComponentModel;

/// <summary>
/// 確認用のモデルクラス
/// </summary>
public class Title : INotifyPropertyChanged
{
    private string _titleName;
    public string TitleName
    {
        get => _titleName;
        set
        {
            _titleName = value;
            OnPropertyChanged(nameof(TitleName));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

namespace compiler_theory.app.view_model;

public class TabViewModel : ViewModelBase
{
    private string _tabPath;
    public string TabPath
    {
        get { return _tabPath; }
        set
        {
            if (_tabPath != value)
            {
                _tabPath = value;
                OnPropertyChanged(nameof(TabPath));
            }
        }
    }

    private string _tabCode;
    public string TabCode
    {
        get { return _tabCode; }
        set
        {
            if (_tabCode != value)
            {
                _tabCode = value;
                OnPropertyChanged(nameof(TabCode));
            }
        }
    }

    private bool _fileOpenOrCreate;

    public bool FileOpenOrCreate
    {
        get { return _fileOpenOrCreate; }

        set
        {
            _fileOpenOrCreate = value;
            OnPropertyChanged(nameof(FileOpenOrCreate));
        }
    }
}
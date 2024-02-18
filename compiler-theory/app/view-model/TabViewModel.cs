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
    
    private string _tabTitle;
    public string TabTitle
    {
        get { return _tabTitle; }
        set
        {
            if (_tabTitle != value)
            {
                _tabTitle = value;
                OnPropertyChanged(nameof(TabTitle));
            }
        }
    }
}
namespace compiler_theory.app.view_model;

/// <summary>
/// Represents a tab in the application.
/// Inherits from the ViewModelBase class.
/// </summary>
public class TabViewModel : ViewModelBase
{
    private string _tabPath;

    /// <summary>
    /// Gets or sets the path of the tab.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the code of the tab.
    /// </summary>
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

    /// <summary>
    /// Gets or sets a value indicating whether the file is open or created.
    /// </summary>
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
using System.Globalization;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Prism.Commands;

namespace compiler_theory.app.view_model;

public class MainWindowViewModel : ViewModelBase
{
    private RelayCommand _openFileCommand;
    private string _code = "";
    private bool _fileOpenOrCreate = false;
    private string _fileOpenOrCreatePath;
    private string culture = "Russian";
    private double _fontSize = 16;

    public double FontSize
    {
        get { return _fontSize; }

        set
        {
            if (_fontSize != value)
            {
                _fontSize = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }
    }
    
    private DelegateCommand<string[]> _dropCommand;
    public DelegateCommand<string[]> DropCommand
    {
        get
        {
            return _dropCommand ??= new DelegateCommand<string[]>(Drop);
        }
    }

    private void Drop(string[] files)
    {
        if (files != null && files.Length > 0)
        {
            string filePath = files[0];
            LoadFile(filePath);
        }
    }
    
    public void LoadFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            Code = File.ReadAllText(filePath);
            _fileOpenOrCreate = true;
            _fileOpenOrCreatePath = filePath;
        }
    }

    private ICommand _incFont;

    public ICommand IncFontCommand
    {
        get
        {
            return _incFont ?? (_incFont = new RelayCommand(IncFont));
        }
    }
    
    private ICommand _decFont;
    
    public ICommand DecFontCommand
    {
        get
        {
            return _decFont ?? (_decFont = new RelayCommand(DecrFont));
        }
    }
    
    private ICommand _exitCommand;
    public ICommand ExitCommand
    {
        get
        {
            return _exitCommand ?? (_exitCommand = new RelayCommand(Exit));
        }
    }
    
    private ICommand _saveAsCommand;
    public ICommand SaveAsCommand
    {
        get
        {
            return _saveAsCommand ??= new RelayCommand(SaveAs);
        }
    }
    
    private ICommand _saveCommand;
    public ICommand SaveCommand
    {
        get
        {
            return _saveCommand ??= new RelayCommand(Save);
        }
    }
    
    private ICommand _createFileCommand;
    public ICommand CreateFileCommand
    {
        get
        {
            return _createFileCommand ??= new RelayCommand(CreateFile);
        }
    }

    public string Code
    {
        get => _code;

        set
        {
            _code = value;
            OnPropertyChanged(nameof(Code));
        }
    }
    
    public ICommand OpenFileCommand
    {
        get
        {
            _openFileCommand = new RelayCommand(OpenFile);
            return _openFileCommand;
        }
    }
    
    public MainWindowViewModel()
    {
        
    }
    
    private void CreateFile(object parameter)
    {
        if (Code.Length != 0)
        {
            MessageBoxResult result = MessageBox.Show("Хотите сохранить изменения перед созданием нового файла?", 
                "Внимание", 
                MessageBoxButton.YesNoCancel, 
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Save(null);
            }
        }

        var fileService = new FileService();
        var resultCreate = fileService.CreateFile();
        
        if (resultCreate == "Создание файла не получилось") return;
        
        _fileOpenOrCreate = true;
        _fileOpenOrCreatePath = resultCreate;
    }
    
    private void OpenFile(object parameter)
    {
        var openFileDialog = new OpenFileDialog();

        if (Code.Length != 0)
        {
            MessageBoxResult result = MessageBox.Show("Хотите сохранить изменения перед открытием нового файла?", 
                "Внимание", 
                MessageBoxButton.YesNoCancel, 
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Save(null);
            }
        }
        
        if (openFileDialog.ShowDialog() == true)
        {
            string filePath = openFileDialog.FileName;
            FileService fileService = new FileService();
            var text = fileService.ReadTextFromFile(filePath);
            Code = text;
            _fileOpenOrCreate = true;
            _fileOpenOrCreatePath = filePath;
        }
    }

    private void Save(object parameter)
    {
        FileService fileService = new FileService();
        var a = Code;
        if (_fileOpenOrCreate == false)
        {
            var returnValue = fileService.Save(a);
        }
        else
        {
            var returnValue = fileService.Save(a, _fileOpenOrCreatePath);
        }
    }

    private void SaveAs(object parameter)
    {
        FileService fileService = new FileService();
        var a = Code;

        var returnValue = fileService.SaveAs(a);

        if (returnValue == "Сохранение файла не получилось") return;

        _fileOpenOrCreate = true;
        _fileOpenOrCreatePath = returnValue;
    }

    private void DecrFont(object parameter)
    {
        FontSize -= 1.0;
    }

    private void IncFont(object parameter)
    {
        FontSize += 1.0;
    }
    
    private void Exit(object parameter)
    {
        if (Code.Length != 0)
        {
            MessageBoxResult result = MessageBox.Show("Хотите сохранить изменения перед открытием нового файла?",
                "Внимание",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Save(null);
            }
        }

        Application.Current.Shutdown();
    }
    
    // private ICommand _changeLanguageCommand;
    // public ICommand ChangeLanguageCommand
    // {
    //     get
    //     {
    //         return _changeLanguageCommand ??= new RelayCommand(ChangeLanguage);
    //     }
    // }
    
    private ICommand _changeLanguageCommand;
    public ICommand ChangeLanguageCommand
    {
        get
        {
            return _changeLanguageCommand ??= new RelayCommand(ChangeLanguage);
        }
    }
    
    private void ChangeLanguage(object p)
    {
        CultureInfo newCulture = new CultureInfo("en-US");
        CultureInfo.CurrentUICulture = newCulture;
    }
}
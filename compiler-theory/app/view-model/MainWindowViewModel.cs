using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace compiler_theory.app.view_model;

public class MainWindowViewModel : ViewModelBase
{
    private RelayCommand _openFileCommand;
    private string _code = "";
    private bool _fileOpenOrCreate = false;
    private string _fileOpenOrCreatePath;
    
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
}
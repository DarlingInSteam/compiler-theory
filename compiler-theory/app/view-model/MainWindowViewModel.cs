using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using compiler_theory.app.model;
using compiler_theory.app.model.parser;
using Microsoft.Win32;
using Prism.Commands;

namespace compiler_theory.app.view_model;

public class MainWindowViewModel : ViewModelBase
{
    private const string _aboutPath = "About.html";
    private const string _helpPath = "Help.html";
    private const string _postanovPath = "Postanovka.html";
    private const string _grammaPath = "Gramma.html";
    private const string _analyzePath = "Analyze.html";
    private const string _testCode = "Map<Char,Char> _asd = new HashMap<Char,Char>();";
    private RelayCommand _openFileCommand;
    private bool _exitFlag = false;
    private string _code = "";
    private bool _fileOpenOrCreate = false;
    private string _fileOpenOrCreatePath;
    private string culture = "Russian";
    private double _fontSize = 16;

    private ObservableCollection<Token> _tokens;

    public ObservableCollection<Token> Tokens
    {
        get { return _tokens; }
        set
        {
            _tokens = value;
            OnPropertyChanged(nameof(Tokens));
        }
    }

    private ObservableCollection<ParsingError> _parsingErrors;

    public ObservableCollection<ParsingError> ParsingErrors
    {
        get { return _parsingErrors; }

        set
        {
            _parsingErrors = value;
            OnPropertyChanged(nameof(ParsingError));
        }
    }
    
    private RelayCommand _aboutCommand;
    private RelayCommand _helpCommand;
    private RelayCommand _postanovCommand;
    private RelayCommand _grammaCommand;
    private RelayCommand _analyzeCommand;
    public RelayCommand AnalyzeCommandPath
    {
        get => _analyzeCommand ??= new RelayCommand(_ => HtmlHelper.OpenInBrowser(_analyzePath));
    }
    
    public RelayCommand GrammaCommand
    {
        get => _grammaCommand ??= new RelayCommand(_ => HtmlHelper.OpenInBrowser(_grammaPath));
    }
    public RelayCommand PostanovCommand
    {
        get => _postanovCommand ??= new RelayCommand(_ => HtmlHelper.OpenInBrowser(_postanovPath));
    }
    
    public RelayCommand AboutCommand
    {
        get => _aboutCommand ??= new RelayCommand(_ => HtmlHelper.OpenInBrowser(_aboutPath));
    }
    
    public RelayCommand HelpCommand
    {
        get => _helpCommand ??= new RelayCommand(_ => HtmlHelper.OpenInBrowser(_helpPath));
    }
    private TabViewModel _selectedTab;

    public TabViewModel SelectedTab
    {
        get { return _selectedTab; }

        set
        {
            if (SelectedTab != null)
            {
                for (int i = 0; i < Tabs.Count; i++)
                {
                    if (Tabs[i].TabPath == SelectedTab.TabPath)
                    {
                        Tabs[i].TabCode = Code;
                    }

                    if (Tabs[i].TabPath == "Новый файл")
                    {
                        Tabs[i].TabCode = Code;
                        Tabs[i].TabPath = SelectedTab.TabPath;
                        Tabs[i].FileOpenOrCreate = SelectedTab.FileOpenOrCreate;
                    }
                }
            }

            _selectedTab = value;
            Code = value.TabCode;
            _fileOpenOrCreatePath = value.TabPath;
            FileService.GetFileEncoding(value.TabPath);
            FileService.GetFileLineSeparator(value.TabPath);
            FilePath = value.TabPath;
            _fileOpenOrCreate = value.FileOpenOrCreate;
            
            OnPropertyChanged(nameof(SelectedTab));
        }
    }
    
    private ObservableCollection<TabViewModel> _tabs;
    public ObservableCollection<TabViewModel> Tabs
    {
        get { return _tabs; }
        set
        {
            if (_tabs != value)
            {
                _tabs = value;
                OnPropertyChanged(nameof(Tabs));
            }
        }
    }
    
    private string _filePath;
    public string FilePath
    {
        get { return _filePath; }
        set
        {
            if (_filePath != value)
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }
    }
    
    private string _lineSeparator = "LF";
    public string LineSeparator
    {
        get { return _lineSeparator; }
        set
        {
            if (_lineSeparator != value)
            {
                _lineSeparator = value;
                OnPropertyChanged(nameof(LineSeparator));
            }
        }
    }

    private string _fileEncoding = "UTF8";
    public string FileEncoding
    {
        get { return _fileEncoding; }
        set
        {
            if (_fileEncoding != value)
            {
                _fileEncoding = value;
                OnPropertyChanged(nameof(FileEncoding));
            }
        }
    }
    
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
            FilePath = filePath;
            LoadFile(filePath);
        }
    }
    
    public void LoadFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            _fileOpenOrCreate = true;
            _fileOpenOrCreatePath = filePath;
            FilePath = filePath;
            LineSeparator = FileService.GetFileLineSeparator(filePath);
            FileEncoding = FileService.GetFileEncoding(filePath);

            var newTab = new TabViewModel();
            
            newTab.TabPath = filePath;
            newTab.TabCode = File.ReadAllText(filePath);
            newTab.FileOpenOrCreate = true;
        
            Tabs.Add(newTab);
            SelectedTab = newTab;
        }
    }

    private ICommand _testCodeInput;

    public ICommand TestCodeInput
    {
        get
        {
            return _testCodeInput ?? (_testCodeInput = new RelayCommand(InputTestCode));
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

    private ICommand _fixCode;

    public ICommand FixCodeCommand
    {
        get
        {
            return _fixCode ?? (_fixCode = new RelayCommand(FixCode));
        }
    }
    
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
            AnalyzeCodeCommandAntlr(null);
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
        Tabs = new ObservableCollection<TabViewModel>();
        var newTab = new TabViewModel();
        newTab.TabCode = Code;
        newTab.TabPath = "Новый файл";
        Tabs.Add(newTab);
        Tokens = new ObservableCollection<Token>();
        ParsingErrors = new ObservableCollection<ParsingError>();
    }
    
    private void CreateFile(object parameter)
    {
        var resultCreate = FileService.CreateFile();
        
        if (resultCreate == "Создание файла не получилось") return;
        
        _fileOpenOrCreate = true;
        _fileOpenOrCreatePath = resultCreate;
        FilePath = resultCreate;
        LineSeparator = FileService.GetFileLineSeparator(FilePath);
        FileEncoding = FileService.GetFileEncoding(FilePath);
        var newTab = new TabViewModel();
        newTab.TabPath = resultCreate;
        newTab.TabCode = "";
        newTab.FileOpenOrCreate = true;
        
        Tabs.Add(newTab);
        SelectedTab = newTab;
    }
    
    private void OpenFile(object parameter)
    {
        var openFileDialog = new OpenFileDialog();
        
        if (openFileDialog.ShowDialog() == true)
        {
            string filePath = openFileDialog.FileName;
            var text = FileService.ReadTextFromFile(filePath);
            _fileOpenOrCreate = true;
            _fileOpenOrCreatePath = filePath;
            FilePath = filePath;
            LineSeparator = FileService.GetFileLineSeparator(filePath);
            FileEncoding = FileService.GetFileEncoding(filePath);
            var newTab = new TabViewModel();
            newTab.TabPath = filePath;
            newTab.TabCode = text;
            newTab.FileOpenOrCreate = true;
            
            Tabs.Add(newTab);
            SelectedTab = newTab;
        }
    }

    private void Save(object parameter)
    {
        var a = Code;
        if (_exitFlag == true)
        {
            foreach (var tab in Tabs)
            {
                if (tab.FileOpenOrCreate == false)
                {
                    var returnValue = FileService.Save(tab.TabCode);

                    SelectedTab.TabPath = returnValue;
                    SelectedTab.FileOpenOrCreate = true;
                    _fileOpenOrCreate = true;
                    FilePath = returnValue;
                    _fileOpenOrCreatePath = returnValue;
                }
                else
                {
                    var returnValue = FileService.Save(tab.TabCode, tab.TabPath);
                }
            }
        }
        else
        {
            if (_fileOpenOrCreate == false)
            {
                var returnValue = FileService.Save(a);

                SelectedTab.TabPath = returnValue;
                SelectedTab.FileOpenOrCreate = true;
                _fileOpenOrCreate = true;
                FilePath = returnValue;
                _fileOpenOrCreatePath = returnValue;
            }
            else
            {
                var returnValue = FileService.Save(a, _fileOpenOrCreatePath);
            }
        }
    }

    private void SaveAs(object parameter)
    {
        var a = Code;

        var returnValue = FileService.SaveAs(a);

        if (returnValue == "Сохранение файла не получилось") return;

        _fileOpenOrCreate = true;
        _fileOpenOrCreatePath = returnValue;
    }

    private void DecrFont(object parameter)
    {
        FontSize -= 1.0;
    }

    private void InputTestCode(object parameter)
    {
        Code = _testCode;
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
                _exitFlag = true;
                Save(null);
            }
        }

        Application.Current.Shutdown();
    }
    
    private ICommand _changeLanguageCommand;
    public ICommand ChangeLanguageCommand
    {
        get
        {
            return _changeLanguageCommand ??= new RelayCommand(ChangeLanguage);
        }
    }

    private ICommand _analyzeCode;

    public ICommand AnalyzeCode
    {
        get
        {
            return _analyzeCode ??= new RelayCommand(AnalyzeCodeCommand);
        }
    }
    
    private ICommand _analyzeCodeAntlr;
    
    public ICommand AnalyzeCodeAntlr
    {
        get
        {
            return _analyzeCodeAntlr ??= new RelayCommand(AnalyzeCodeCommandAntlr);
        }
    }

    private void FixCode(object p)
    {
        var a = new AntlrParser();
        var buff = a.Parse(Code);
        Code = CodeFix.Fix(Code, buff.GetParsingErrors());
    }

    private void AnalyzeCodeCommand(object p)
    {
        Scanner scanner = new Scanner(Code);
        scanner.Analyze();
        var buff = scanner.Tokens;
        
        Tokens.Clear();
        
        foreach (var token in buff)
        {
            Tokens.Add(token);
        }

        List<Token> tokensBuff = new List<Token>();

        foreach (var token in buff)
        {
            if (token.Code != "13")
            {
                tokensBuff.Add(token);
            }
        }
        
        Parser parser = new Parser(tokensBuff);

        var errors = parser.Parse();
        AntlrParser antlrParser = new AntlrParser();
        var asd = antlrParser.Parse(Code);
        
        ParsingErrors.Clear();
        
        foreach (var error in errors)
        {
            ParsingErrors.Add(error);
        }
    } 
    
    private void AnalyzeCodeCommandAntlr(object p)
    {
        AntlrParser antlrParser = new AntlrParser();
        var errors = antlrParser.Parse(Code);
        
        ParsingErrors.Clear();
        Tokens.Clear();
        
        foreach (var error in errors.GetLexerErrors())
        {
            Tokens.Add(error);
        }

        foreach (var error in errors.GetParsingErrors())
        {
            ParsingErrors.Add(error);
        }
    } 
    
    private void ChangeLanguage(object p)
    {
        CultureInfo newCulture = new CultureInfo("en-US");
        CultureInfo.CurrentUICulture = newCulture;
    }
}
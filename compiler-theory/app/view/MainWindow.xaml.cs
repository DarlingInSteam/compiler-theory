using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using compiler_theory.app.model;
using compiler_theory.app.view_model;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace compiler_theory.app.view;

public partial class MainWindow : Window
{
    private readonly Dictionary<string, string> AutoCompleteSuggestions = new Dictionary<string, string>
    {
        {"int", "1"},
        {"bool", "2"},
        {"string", "3"},
        {"String", "3"},
        {"Map", "4"},
        {"new", "5"},
        {"float", "6"},
        {"byte", "7"},
        {"short", "8"},
        {"long", "9"},
        {"char", "10"},
        {"HashMap", "11"},
    };
    
    private CompletionWindow _completionWindow;
    private readonly MainWindowViewModel _mainWindowViewModel = new MainWindowViewModel();
    public MainWindow()
    {
        InitializeComponent();
        textEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
        textEditor.TextArea.TextEntered += textEditor_TextArea_TextEntered;
        textEditor.TextChanged += textEditor_TextChanged;
        DataContext = _mainWindowViewModel;
        textEditor.LineNumbersForeground = Brushes.White;
    }
    
    private void Window_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                ((MainWindowViewModel)DataContext).DropCommand.Execute(files);
            }
        }
        e.Handled = true;
    }
    
    private void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
    {
        string currentWord = GetCurrentWord();

        if (currentWord.Length == 0)
        {
            var suggestions = AutoCompleteSuggestions
                .Where(entry => entry.Key.StartsWith(e.Text))
                .Select(entry => new MyCompletionData(entry.Key, entry.Value, "Description goes here"))
                .ToList();

            if (suggestions.Any())
            {
                _completionWindow = new CompletionWindow(textEditor.TextArea);
                var data = _completionWindow.CompletionList.CompletionData;

                foreach (var suggestion in suggestions)
                {
                    data.Add(suggestion);
                }

                _completionWindow.Show();
            }
        }
    }
    
    private string GetCurrentWord()
    {
        var offset = textEditor.CaretOffset;
        var document = textEditor.Document;
        var line = document.GetLineByOffset(offset);
        var lineText = document.GetText(line);

        // Find the start of the current word
        var start = offset - 1;

        // Check if the start is within the bounds of the line
        if (start < line.Offset)
        {
            start = line.Offset;
        }

        while (start >= line.Offset && start < lineText.Length && char.IsLetterOrDigit(lineText[start]))
        {
            start--;
        }

        // Extract the current word
        var currentWord = start < offset - 1
            ? lineText.Substring(start + 1, offset - start - 1)
            : string.Empty;

        return currentWord;
    }
    
    private void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
    {
        switch (e.Text)
        {
            case ".":
                ShowCompletion();
                break;
            case "{":
                textEditor.Document.Insert(textEditor.CaretOffset, "}");
                textEditor.CaretOffset--;
                break;
            case "(":
                textEditor.Document.Insert(textEditor.CaretOffset, ")");
                textEditor.CaretOffset--;
                break;
            case "\"":
                textEditor.Document.Insert(textEditor.CaretOffset, "\"");
                textEditor.CaretOffset--;
                break;
            case "'":
                textEditor.Document.Insert(textEditor.CaretOffset, "'");
                textEditor.CaretOffset--;
                break;
            case "<":
                textEditor.Document.Insert(textEditor.CaretOffset, ">");
                textEditor.CaretOffset--;
                break;
        }
    }
    
    private void ShowCompletion()
    {
        var currentWord = GetCurrentWord();

        if (currentWord.Length != 0) return;
        var suggestions = AutoCompleteSuggestions
            .Where(entry => entry.Key.StartsWith("."))
            .Select(entry => new MyCompletionData(entry.Key, entry.Value, "Description goes here"))
            .ToList();

        if (!suggestions.Any()) return;
        _completionWindow = new CompletionWindow(textEditor.TextArea);
        var data = _completionWindow.CompletionList.CompletionData;

        foreach (var suggestion in suggestions)
        {
            data.Add(suggestion);
        }

        _completionWindow.Show();
        _completionWindow.Closed += (o, args) =>
        {
            if (_completionWindow.CompletionList.SelectedItem == null) return;
            var selectedText = ((ICompletionData) _completionWindow.CompletionList.SelectedItem).Text;
            textEditor.Document.Replace(textEditor.CaretOffset - currentWord.Length, currentWord.Length, selectedText);
        };
    }

    
    private void AnalyzeCode()
    {
        
    }

    private void textEditor_TextChanged(object sender, EventArgs e)
    {
        AnalyzeCode();
    }
    
    
    
    private DelegateCommand _undoCommand;
    public DelegateCommand UndoCommand
    {
        get
        {
            return _undoCommand ??= new DelegateCommand(Undo);
        }
    }

    private DelegateCommand _redoCommand;
    public DelegateCommand RedoCommand
    {
        get
        {
            return _redoCommand ??= new DelegateCommand(Redo);
        }
    }

    private DelegateCommand _cutCommand;
    public DelegateCommand CutCommand
    {
        get
        {
            return _cutCommand ??= new DelegateCommand(Cut);
        }
    }

    private DelegateCommand _copyCommand;
    public DelegateCommand CopyCommand
    {
        get
        {
            return _copyCommand ??= new DelegateCommand(Copy);
        }
    }

    private DelegateCommand _pasteCommand;
    public DelegateCommand PasteCommand
    {
        get
        {
            return _pasteCommand ??= new DelegateCommand(Paste);
        }
    }

    private DelegateCommand _deleteCommand;
    public DelegateCommand DeleteCommand
    {
        get
        {
            return _deleteCommand ??= new DelegateCommand(Delete);
        }
    }

    private DelegateCommand _selectAllCommand;
    public DelegateCommand SelectAllCommand
    {
        get
        {
            return _selectAllCommand ??= new DelegateCommand(SelectAll);
        }
    }
    
    
    private void Undo()
    {
        textEditor.Undo();
    }

    private void Redo()
    {
        textEditor.Redo();
    }

    private void textEditor_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
    {
        GetCaretPosition();
    }
    
    private void GetCaretPosition()
    {
        int offset = textEditor.CaretOffset;
        var location = textEditor.Document.GetLocation(offset);
        CursorPositionTextBlock.Text = $"Строка: {location.Line}, Столбец: {location.Column}";
    }
    
    private void Cut()
    {
        textEditor.Cut();
    }

    private void Copy()
    {
        textEditor.Copy();
    }

    private void Paste()
    {
        textEditor.Paste();
    }

    private void Delete()
    {
        textEditor.SelectedText = string.Empty;
    }

    private void SelectAll()
    {
        textEditor.SelectAll();
    }

    private void MainWindow_OnClosed(object? sender, EventArgs e)
    {
        MainWindowViewModel asd = (MainWindowViewModel)DataContext;
        
        asd.ExitCommand.Execute(null);
    }

    private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is ListView listView && listView.SelectedItem is Token selectedToken)
        {
            // Найдите соответствующий текст в AvalonEdit
            string tokenText = selectedToken.Value;
            int startIndex = selectedToken.StartIndex - 1;
            int endIndex = selectedToken.EndIndex - 1;

            if (startIndex >= 0 && endIndex < textEditor.Document.TextLength)
            {
                textEditor.Select(startIndex, endIndex - startIndex + 1);
                textEditor.ScrollToLine(textEditor.TextArea.Document.GetLocation(startIndex).Line);
            }
        }
    }
}

public class MyCompletionData : ICompletionData
{
    public MyCompletionData(string text, string returnType, string discription)
    {
        Text = text;
        ReturnType = returnType;
        _description = discription;
    }

    private string _description = "";
    public object Content => Text;

    public object Description => $"Returns: {ReturnType}\nDescription for {_description}";

    public ImageSource Image { get; }

    public double Priority => 0;

    public string Text { get; }

    public string ReturnType { get; }

    public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
    {
        textArea.Document.Replace(completionSegment, Text);
    }
}


public class ErrorItem
{
    public string Error { get; set; }
    public string Description { get; set; }
}

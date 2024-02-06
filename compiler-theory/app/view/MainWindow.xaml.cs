using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using compiler_theory.app.view_model;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace compiler_theory.app.view;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private CompletionWindow completionWindow;

    public MainWindow()
    {
        InitializeComponent();
        textEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
        textEditor.TextArea.TextEntered += textEditor_TextArea_TextEntered;
        textEditor.TextChanged += textEditor_TextChanged;
    }
    
    private void Window_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                // Вызываем команду Drop для обработки перетаскивания файла
                ((MainWindowViewModel)DataContext).DropCommand.Execute(files);
            }
        }
        e.Handled = true;
    }
    
    private void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
    {
        

        if (e.Text.Length > 0 && completionWindow != null)
        {
            if (!char.IsLetterOrDigit(e.Text[0]))
            {
                // Завершение слова
                completionWindow.CompletionList.RequestInsertion(e);
            }
        }
    }
    
    private void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
    {
        if (e.Text == ".")
        {
            // Запуск автодополнения при вводе точки
            ShowCompletion();
        }
        
        if (e.Text == "{")
        {
            textEditor.Document.Insert(textEditor.CaretOffset, "}");
            textEditor.CaretOffset--; // Переместить курсор между { и }
        }
    
        // Аналогично для кавычек "
        if (e.Text == "\"")
        {
            textEditor.Document.Insert(textEditor.CaretOffset, "\"");
            textEditor.CaretOffset--; // Переместить курсор между кавычками ""
        }
    
        // Аналогично для одинарных кавычек '
        if (e.Text == "'")
        {
            textEditor.Document.Insert(textEditor.CaretOffset, "'");
            textEditor.CaretOffset--; // Переместить курсор между одинарными кавычками ''
        }
    }
    
    private void ShowCompletion()
    {
        var text = textEditor.Text;
        var syntaxTree = CSharpSyntaxTree.ParseText(text);
        var root = syntaxTree.GetRoot();
        var compilation = CSharpCompilation.Create("MyCompilation")
            .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
            .AddSyntaxTrees(syntaxTree);
        var semanticModel = compilation.GetSemanticModel(syntaxTree);
    
        var caretPosition = textEditor.CaretOffset;
        var token = root.FindToken(caretPosition);
    
        var memberAccess = token.Parent as MemberAccessExpressionSyntax ?? token.Parent.Parent as MemberAccessExpressionSyntax;
    
        if (memberAccess != null)
        {
            // Получение типа выражения перед точкой
            var typeInfo = semanticModel.GetTypeInfo(memberAccess.Expression);
    
            // Получение доступных членов типа
            var members = typeInfo.Type?.GetMembers()
                .Where(m => m.Kind == SymbolKind.Method || m.Kind == SymbolKind.Property)
                .Select(m => new MyCompletionData(m.Name))
                .ToList();
    
            if (members != null && members.Any())
            {
                // Отображение окна автодополнения
                completionWindow = new CompletionWindow(textEditor.TextArea);
                var data = completionWindow.CompletionList.CompletionData;
    
                foreach (var member in members)
                {
                    data.Add(member);
                }
                completionWindow.Show();
            }
        }
    }
    
    private void AnalyzeCode()
    {
        string code = textEditor.Text;
        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var compilation = CSharpCompilation.Create("MyCompilation")
            .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
            .AddSyntaxTrees(syntaxTree);
        var diagnostics = compilation.GetDiagnostics();

        var errorList = new List<ErrorItem>();

        foreach (var diagnostic in diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error))
        {
            var span = diagnostic.Location.GetLineSpan();
            var errorItem = new ErrorItem
            {
                Error = $"Error at Line {span.StartLinePosition.Line + 1}, Column {span.StartLinePosition.Character + 1}",
                Description = diagnostic.GetMessage()
            };
            errorList.Add(errorItem);
        }

        // Очищаем старые ошибки и добавляем новые в ListView
        errorListViewXX.Items.Clear();
        foreach (var errorItem in errorList)
        {
            errorListViewXX.Items.Add(errorItem);
        }
    }

    private void textEditor_TextChanged(object sender, EventArgs e)
    {
        // При каждом изменении текста в редакторе перезапускаем анализ кода и обновляем ошибки
        AnalyzeCode();
    }
}

public class MyCompletionData : ICompletionData
{
    public MyCompletionData(string text)
    {
        Text = text;
    }

    public object Content => Text;

    public object Description => "Description for " + Text;

    public ImageSource Image => null;

    public double Priority => 0;

    public string Text { get; }

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

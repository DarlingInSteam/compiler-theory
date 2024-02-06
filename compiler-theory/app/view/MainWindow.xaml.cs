using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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
        // Получение кода C# из редактора
        var text = textEditor.Text;

        // Анализ кода с использованием Roslyn
        var syntaxTree = CSharpSyntaxTree.ParseText(text);
        var root = syntaxTree.GetRoot();
        var compilation = CSharpCompilation.Create("MyCompilation")
            .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
            .AddSyntaxTrees(syntaxTree);
        var semanticModel = compilation.GetSemanticModel(syntaxTree);

        var caretPosition = textEditor.CaretOffset;
        var token = root.FindToken(caretPosition);

        // Получение типа выражения перед точкой
        var typeInfo = semanticModel.GetTypeInfo(((MemberAccessExpressionSyntax)token.Parent).Expression);

        // Получение доступных членов типа
        var members = typeInfo.Type.GetMembers()
            .Where(m => m.Kind == SymbolKind.Method || m.Kind == SymbolKind.Property)
            .Select(m => new MyCompletionData(m.Name))
            .ToList();

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
using System.Windows;
using ICSharpCode.AvalonEdit;
using Microsoft.Xaml.Behaviors;

namespace compiler_theory.app.view_model;

public class AvalonEditBehaviour : Behavior<TextEditor>
{
    public static readonly DependencyProperty InputTextProperty =
        DependencyProperty.Register("InputText", typeof(string), typeof(AvalonEditBehaviour),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, InputTextChanged));

    public string InputText
    {
        get { return (string)GetValue(InputTextProperty); }
        set { SetValue(InputTextProperty, value); }
    }

    private static void InputTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var behavior = (AvalonEditBehaviour)d;
        var textEditor = behavior.AssociatedObject;
        
        if (textEditor != null && textEditor.Document != null)
        {
            if (textEditor.Document.Text != behavior.InputText)
            {
                textEditor.Document.Text = behavior.InputText;
            }
        }
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        if (AssociatedObject != null)
        {
            AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
        }
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        if (AssociatedObject != null)
        {
            AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
        }
    }

    private void AssociatedObjectOnTextChanged(object sender, EventArgs eventArgs)
    {
        var textEditor = sender as TextEditor;
        if (textEditor != null)
        {
            if (textEditor.Document != null)
            {
                InputText = textEditor.Document.Text;
            }
        }
    }
}

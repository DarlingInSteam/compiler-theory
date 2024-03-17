namespace compiler_theory.app.view_model;

using System.ComponentModel;

/// <summary>
/// Base class for all ViewModel classes in the application.
/// Implements the INotifyPropertyChanged interface to support 
/// binding in the MVVM pattern.
/// </summary>
public class ViewModelBase : INotifyPropertyChanged
{
    /// <summary>
    /// Event that is invoked when a property is changed.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Method to raise the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
        // Invoke the PropertyChanged event; the event receiver
        // will handle the event if it isn't null.
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
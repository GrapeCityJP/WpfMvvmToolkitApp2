using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace WpfMvvmToolkitApp2;

public partial class NewMainViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string firstName = "太郎";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string lastName = "葡萄";

    public string FullName => $"{LastName} {FirstName}";

    [ObservableProperty]
    public string greetingMessage = "";

    [RelayCommand]
    private void Greet(string? user)
    {
        GreetingMessage = $"こんにちは、{user}！";
    }
}

public partial class OldMainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string firstName = "太郎";
    public string FirstName
    {
        get => firstName;
        set
        {
            firstName = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FullName)));
        }
    }

    private string lastName = "葡萄";
    public string LastName
    {
        get => lastName;
        set
        {
            lastName = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FullName)));
        }
    }

    public string FullName => $"{LastName} {FirstName}";

    private string greetingMessage = "";
    public string GreetingMessage
    {
        get => greetingMessage;
        set
        {
            greetingMessage = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GreetingMessage)));
        }
    }

    private DelegateCommand<string>? greetCommand;
    public DelegateCommand<string>? GreetCommand
    {
        get
        {
            return greetCommand ??= new DelegateCommand<string>(Greet);
        }
    }

    private void Greet(string? user)
    {
        GreetingMessage = $"こんにちは、{user}！";
    }
}

public class DelegateCommand<T> : ICommand
{
    private readonly Action<T> _execute;
    private readonly Func<bool> _canExecute;

    public event EventHandler? CanExecuteChanged;

    public DelegateCommand(Action<T> execute) : this(execute, () => true)
    {
    }

    public DelegateCommand(Action<T> execute, Func<bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public void Execute(object? parameter)
    {
        _execute((T)parameter);
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute();
    }
}
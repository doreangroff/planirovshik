using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace planirovshik.Windows;

public partial class DelTaskWin : Window
{
    private readonly int _selectedIndex;
    public ObservableCollection<Task> MainTasksList { get; set; }
    
    public DelTaskWin(int selectedIndex)
    {
        InitializeComponent();
        _selectedIndex = selectedIndex;
    }

    private void Yes(object? sender, RoutedEventArgs e)
    {
        MainTasksList.RemoveAt(_selectedIndex);
        this.Close();
    }

    private void No(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
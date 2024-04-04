using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace planirovshik.Windows;

public partial class EditTaskWin : Window
{
    private readonly Task _selectedTask;
    public EditTaskWin(Task selectedTask)
    {
        InitializeComponent();
        _selectedTask = selectedTask;
        NameTb.Text = selectedTask.TaskName;
    }

    private void Save(object? sender, RoutedEventArgs e)
    {
        string name = NameTb.Text;
        _selectedTask.TaskName = name;
        this.Close();
    }

    private void Cancel(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
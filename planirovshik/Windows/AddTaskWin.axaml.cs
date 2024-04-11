using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace planirovshik.Windows;

public partial class AddTaskWin : Window
{
    public ObservableCollection<Task> MainTasksList { get; set; }
    public AddTaskWin()
    {
        InitializeComponent();
        MainTasksList = new ObservableCollection<Task>();
    }

    private void Save(object? sender, RoutedEventArgs e)
    {
        string name = NameTb.Text;
        bool isCompleted = false;
        Task task = new Task { TaskName = name, IsCompleted = isCompleted};
        MainTasksList.Add(task);
        this.Close();
    }

    private void Cancel(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
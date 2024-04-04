using System;
using System.ComponentModel;

namespace planirovshik;

public class Task : INotifyPropertyChanged
{
    private bool _isCompleted;

    public bool IsCompleted
    {
        get { return _isCompleted; }
        set
        {
            if (_isCompleted != value)
            {
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }
    }
        
    private string _taskName;
    public string TaskName
    {
        get { return _taskName; }
        set
        {
            if (_taskName != value)
            {
                _taskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
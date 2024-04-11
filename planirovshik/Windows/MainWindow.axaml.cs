using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System.Media;
using Avalonia.Threading;
using NAudio.Wave;
using planirovshik.Windows;


namespace planirovshik
{
    public partial class MainWindow : Window
    {
        private WaveOutEvent waveOut;
        private AudioFileReader audioFile;
        private ObservableCollection<Task> _tasks;
        private TaskStorage _taskStorage;
        private ObservableCollection<Event> _events;
        private EventStotage _eventStotage;
        private bool isEventWinOpen = false;
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            _taskStorage = new TaskStorage("tasks.json");
            _tasks = new ObservableCollection<Task>(_taskStorage.LoadTasks(out var lastUpdate));
            _eventStotage = new EventStotage("events.json");
            _events = new ObservableCollection<Event>(_eventStotage.LoadEvents());
            TaskLB.ItemsSource = _tasks;
            waveOut = new WaveOutEvent();
            audioFile = new AudioFileReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bomb.wav"));
            if (lastUpdate.Date != DateTime.Today)
            {
                _tasks.Clear();
            }
            Closed += delegate { SaveTasks(); SaveEvent();};
        }
        
        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += Timer_Tick;
        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            ProcessSelectedDate();
        }
        
        private void ProcessSelectedDate()
        {
            if (Calendar.SelectedDate.HasValue)
            {
                DateTime selectedDate = Calendar.SelectedDate.Value;
                OpenEventWin(selectedDate);
            }
        }

        private void SaveTasks()
        {
            _taskStorage.SaveTasks(_tasks.ToList());
        }

        private void SaveEvent()
        {
            _eventStotage.SaveEvents(_events.ToList());
        }
    
        private void Add(object? sender, RoutedEventArgs e)
        {
            AddTaskWin add = new AddTaskWin();
            add.MainTasksList = _tasks;
            add.ShowDialog(this);
            add.Closed += delegate 
            { 
                TaskLB.ItemsSource = _tasks;
                TaskLB.SelectedItem = null;
            };
        }

        private void EditTask(object? sender, TappedEventArgs e)
        {
            Task selectedTask = TaskLB.SelectedItem as Task;
            EditTaskWin edit = new EditTaskWin(selectedTask);
            edit.ShowDialog(this);
            edit.Closed += delegate
            {
                TaskLB.ItemsSource = _tasks;
                TaskLB.SelectedItem = null;
            };
        }

        public void Delete(object? sender, RoutedEventArgs e)
        {
            int selectedIndex = TaskLB.SelectedIndex;
            if (selectedIndex == -1)
            {
                waveOut.Init(audioFile);
                waveOut.Play();
                return;
            }
            DelTaskWin delete = new DelTaskWin(selectedIndex);
            delete.MainTasksList = _tasks;
            delete.ShowDialog(this);
            delete.Closed += delegate
            {
                TaskLB.ItemsSource = _tasks;
                TaskLB.SelectedItem = null;
            };
        }

        public void TaskCheck(object? sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint((Visual?)sender).Properties.IsRightButtonPressed)
            {
                if (sender is TextBlock textBlock)
                {
                    if (textBlock.DataContext is Task task)
                    {
                        task.IsCompleted = !task.IsCompleted;
                    }
                }
            }
        }
        private void AddEvent(object? sender, SelectionChangedEventArgs e)
        {
            if (isEventWinOpen)
            {
                return;
            }
            timer.Stop();
            timer.Start();
        }

        private void OpenEventWin(DateTime date)
        {
            isEventWinOpen = true;

            var eventStorage = new EventStotage("event.json");
            List<Event> events = eventStorage.LoadEvents();
            Event existingEvent = events.FirstOrDefault(e => e.EventDate.Date == date.Date);

            DateTime? selDate = Calendar.SelectedDate;
            
            var eventWindow = new AddEvent(selDate, existingEvent?.EventName);
            eventWindow.Closed += delegate
            {
                isEventWinOpen = false;
                Calendar.SelectedDate = null;
            };
            eventWindow.ShowDialog(this);
        }
    }
}


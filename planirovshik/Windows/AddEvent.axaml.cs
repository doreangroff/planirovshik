using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace planirovshik.Windows;

public partial class AddEvent : Window
{
    public Event Event { get; set; }
    public string EventName { get; set; }
    private readonly Event _event;
    private readonly EventStotage _eventStotage;

    public AddEvent(DateTime? date, string existingEvent = null)
    {
        InitializeComponent();
        DataContext = this;
        _event = new Event();
        _event.EventDate = (DateTime)date;
        _eventStotage = new EventStotage("events.json");
        if (!string.IsNullOrEmpty(existingEvent))
        {
            EventName = existingEvent;
        }
        string json = File.ReadAllText("events.json");
        List<Event> events = JsonSerializer.Deserialize<List<Event>>(json);
        foreach (Event e in events)
        {
            if (e.EventDate.Date == date)
            {
                NameTB.Text = e.EventName;
            }
        }
    }

    private void Save(object sender, RoutedEventArgs e)
    {
        _event.EventName = EventName;
        List<Event> events = _eventStotage.LoadEvents();
        Event exisitingEvent = events.FirstOrDefault(e => e.EventDate.Date == _event.EventDate.Date);
        if (exisitingEvent != null)
        {
            exisitingEvent.EventName = _event.EventName;
        }
        else
        {
            events.Add(_event);
        }
        _eventStotage.SaveEvents(events);
        this.Close();
    }

    private void Back(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
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

    public AddEvent(string existingEvent = null)
    {
        InitializeComponent();
        DataContext = this;
        _event = new Event();
        _event.EventDate = DateTime.Today;
        _eventStotage = new EventStotage("events.json");
        if (!string.IsNullOrEmpty(existingEvent))
        {
            EventName = existingEvent;
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
}
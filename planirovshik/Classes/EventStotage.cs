using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

namespace planirovshik;

public class EventStotage
{
    private string _filePath;

    public EventStotage(string filePath)
    {
        _filePath = filePath;
    }

    public List<Event> LoadEvents()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Event>>(json);
        }

        return new List<Event>();
    }

    public void SaveEvents(List<Event> events)
    {
        string json = JsonSerializer.Serialize(events);
        File.WriteAllText(_filePath, json);
    }
}
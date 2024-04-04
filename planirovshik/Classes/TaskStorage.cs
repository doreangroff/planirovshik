using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace planirovshik;

public class TaskStorage
{
    private string _filePath;
    private DateTime _lastUpdate;

    public TaskStorage(string filePath)
    {
        _filePath = filePath;
        _lastUpdate = DateTime.MinValue;
    }

    public void SaveTasks(List<Task> tasks)
    {
        string json = JsonSerializer.Serialize(tasks);
        File.WriteAllText(_filePath, json);
    }

    public List<Task> LoadTasks(out DateTime lastUpdate)
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            lastUpdate = File.GetLastWriteTime(_filePath);
            return JsonSerializer.Deserialize<List<Task>>(json);
        }
        else
        {
            lastUpdate = DateTime.MinValue;
            return new List<Task>();
        }
    }
}
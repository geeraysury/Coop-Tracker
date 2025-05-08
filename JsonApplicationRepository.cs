using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class JsonApplicationRepository : IApplicationRepository
{
    private const string FilePath = "applications.json";

    public List<ApplicationEntry> LoadApplications()
    {
        if (!File.Exists(FilePath))
            return new List<ApplicationEntry>();

        try
        {
            string jsonString = File.ReadAllText(FilePath);
            List<ApplicationEntry> applications = JsonSerializer.Deserialize<List<ApplicationEntry>>(jsonString);
            return applications ?? new List<ApplicationEntry>();
        }
        catch
        {
            Console.WriteLine("Error reading the file.");
            return new List<ApplicationEntry>();
        }
    }

    public void SaveApplications(List<ApplicationEntry> applications)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(applications);
            File.WriteAllText(FilePath, jsonString);
        }
        catch
        {
            Console.WriteLine("Error writing to the file.");
        }
    }
}

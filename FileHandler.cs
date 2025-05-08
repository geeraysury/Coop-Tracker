using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
public class FileHandler
{
    private const string filePath = "applications.json";
    // Read from JSON and return a list
    public static List<ApplicationEntry> LoadApplications(){
        if (!File.Exists(filePath)){
            return new List<ApplicationEntry>();
        }

        File.ReadAllText("applications.json");
        try {
            string jsonString = File.ReadAllText(filePath);
            List<ApplicationEntry> applications = JsonSerializer.Deserialize<List<ApplicationEntry>>(jsonString);
            return applications ?? new List<ApplicationEntry>();
        } catch {
            Console.WriteLine("Error reading the file");
            return new List<ApplicationEntry>();
        }
    }
    // To save newly updated list to JSON
    public static void SaveApplications(List<ApplicationEntry> applications) {
        try{
            string jsonString = JsonSerializer.Serialize(applications);
            File.WriteAllText(filePath, jsonString);
        } catch {
            Console.WriteLine("Error writing to the file");
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace UniversityManagement
{
    public class SaveManager
    {
        private const string SaveFilePath = "faculties.json";
        public void SaveState(List<Faculty> faculties)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(faculties, options);
            File.WriteAllText(SaveFilePath, jsonString);
            Console.WriteLine("State saved successfully.");
        }

        public List<Faculty> LoadState()
        {
            if (File.Exists(SaveFilePath))
            {
                string jsonString = File.ReadAllText(SaveFilePath);
                var faculties = JsonSerializer.Deserialize<List<Faculty>>(jsonString);
                Console.WriteLine("State loaded successfully.");
                return faculties;
            }
            else
            {
                Console.WriteLine("No saved state found.");
                return new List<Faculty>();
            }
        }
    }
}
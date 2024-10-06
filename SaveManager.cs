using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace UniversityManagement
{
    public class SaveManager
    {
        private const string SaveFilePath = "faculties.json";
        private readonly Logger logger;

        public SaveManager(Logger logger)
        {
            this.logger = logger;
        }

        public void SaveState(List<Faculty> faculties)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(faculties, options);
                File.WriteAllText(SaveFilePath, jsonString);
                Console.WriteLine("State saved successfully.");
                logger.LogInfo("State saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving state: " + ex.Message);
                logger.LogError("Error saving state: " + ex.Message);
            }
        }

        public List<Faculty> LoadState()
        {
            try
            {
                if (File.Exists(SaveFilePath))
                {
                    string jsonString = File.ReadAllText(SaveFilePath);
                    var faculties = JsonSerializer.Deserialize<List<Faculty>>(jsonString);

                    if (faculties == null)
                    {
                        Console.WriteLine("Deserialization returned null. Returning an empty list.");
                        logger.LogWarning("Deserialization returned null. Returning an empty list.");
                        return new List<Faculty>();
                    }

                    Console.WriteLine("State loaded successfully.");
                    logger.LogInfo("State loaded successfully.");
                    return faculties;
                }
                else
                {
                    Console.WriteLine("No saved state found.");
                    logger.LogWarning("No saved state found.");
                    return new List<Faculty>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading state: " + ex.Message);
                logger.LogError("Error loading state: " + ex.Message);
                return new List<Faculty>();
            }
        }
    }
}
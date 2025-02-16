﻿using LeaningCentre.Models;
using System.Text.Json;

namespace LeaningCentre.Services
{
    public partial class LearningCentre1
    {
        string ApplicationPath = GetApplicationPath();
        List<Application1> Applications = new List<Application1>();
        public void LoadApplicationFromJson()
        {
            if (File.Exists(ApplicationPath))
            {
                string json = string.Empty;
                using (StreamReader sr = new StreamReader(ApplicationPath))
                {
                    json = sr.ReadToEnd();
                }
                Applications = JsonSerializer.Deserialize<List<Application1>>(json);
            }
            else
            {
                Mentors = new List<Mentor>();
            }
        }
        public void SaveApplicationToJson()
        {
            string serialized = JsonSerializer.Serialize(Applications);
            using (StreamWriter sw = new StreamWriter(ApplicationPath))
            {
                sw.WriteLine(serialized);
            }
        }


        public void AddAplication(string title)
        {
            int id = Applications.Count > 0 ? Applications.Max(s => s.Id) + 1 : 1;
            Applications.Add(new Application1 { Id = id, Title = title });
            SaveApplicationToJson();
        }

        public void GetListApplications()
        {
            if (Applications.Count > 0)
            {
                foreach (var app in Applications)
                {
                    Console.WriteLine(app.Id + " " + app.Title);
                }
            }
            else
                Console.WriteLine("Applications list is empty");
        }

        public static string GetApplicationPath()
        {
            string currentPath = Directory.GetCurrentDirectory();
            currentPath += "\\applications.json";
            return currentPath;
        }
    }
}

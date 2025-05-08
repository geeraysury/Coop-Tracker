using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        List<ApplicationEntry> applications = FileHandler.LoadApplications();
        bool running = true;

        while (running)
        {
            Console.WriteLine("==== INTERNSHIP TRACKER ====");
            Console.WriteLine("1. Add Application");
            Console.WriteLine("2. Update Application Status");
            Console.WriteLine("3. Filter Applications by Status");
            Console.WriteLine("4. Show Application Statistics");
            Console.WriteLine("5. Delete Application");
            Console.WriteLine("6. Edit Application");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ApplicationService.AddApplication(applications);
                    break;
                case "2":
                    ApplicationService.UpdateStatus(applications);
                    break;
                case "3":
                    ApplicationService.FilterByStatus(applications);
                    break;
                case "4":
                    ApplicationService.ShowStats(applications);
                    break;
                case "5":
                    ApplicationService.DeleteApplication(applications);
                    break;
                case "6":
                    ApplicationService.EditApplication(applications);
                    break;
                case "7":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        Console.WriteLine("Goodbye!");
    }
}

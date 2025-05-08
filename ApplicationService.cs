using System;

public class ApplicationService 
{

    public static void AddApplication(List<ApplicationEntry> applications){
        
        Console.Write("Enter the company name: ");
        string company = Console.ReadLine();

        Console.Write("Enter the job title: ");
        string jobTitle = Console.ReadLine();

        Console.Write("Enter the status (Applied, Interview, Offer, Rejected): ");
        string status = Console.ReadLine();

        Console.Write("Enter the application date (YYYY-MM-DD): ");
        string dateInput = Console.ReadLine();

        DateTime applicationDate;
        while (!DateTime.TryParse(dateInput, out applicationDate))
        {
            Console.Write("Invalid date format. Please enter again (YYYY-MM-DD): ");
            dateInput = Console.ReadLine();
        }

        ApplicationEntry newApp = new ApplicationEntry(company, jobTitle, status, applicationDate);
        applications.Add(newApp);
        Console.WriteLine("Application added to the list");
        FileHandler.SaveApplications(applications);
    }
    
    public static void UpdateStatus(List<ApplicationEntry> applications){
        if (applications.Count == 0)
        {
            Console.WriteLine("No applications found.");
            return;
        }
        for (int i = 0; i < applications.Count; i++){
            Console.WriteLine($"{i + 1}. {applications[i]}");
        }
        Console.Write("Enter the number of the application you would like to update: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int index) || index < 1 || index > applications.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }
        ApplicationEntry updateJob = applications[index-1];
        Console.Write("Enter the new status (Applied, Interview, Offer, Rejected): ");
        string newStatus = Console.ReadLine();
        if (newStatus == "Applied" || newStatus == "Interview" || newStatus == "Offer" || newStatus == "Rejected"){
            updateJob.Status = newStatus;
            Console.WriteLine("Status updated.");
            FileHandler.SaveApplications(applications);
        } else {
            Console.WriteLine("Please enter a valid status");
        }
    }

    public static void FilterByStatus(List<ApplicationEntry> applications){
        Console.Write("Enter the status of the jobs you want to see (Applied, Interview, Offer, Rejected): ");
        string status = Console.ReadLine();
        if (status.Equals("Applied", StringComparison.OrdinalIgnoreCase) ||
            status.Equals("Interview", StringComparison.OrdinalIgnoreCase) ||
            status.Equals("Offer", StringComparison.OrdinalIgnoreCase) ||
            status.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
        {
            int count = 0;
            for (int i = 0; i < applications.Count; i++){
                if (applications[i].Status == status){
                    Console.WriteLine($"{i + 1}. {applications[i]}");
                    count++;
                }
            }
            Console.WriteLine("The number of jobs with status = " + status + " is " + count);
        } else {
            Console.WriteLine("Please enter a valid status");
        }
    }

    public static void ShowStats(List<ApplicationEntry> applications)
    {
        int applied = 0;
        int interview = 0;
        int offer = 0;
        int rejected = 0;

        foreach (var app in applications)
        {
            switch (app.Status.Trim().ToLower())
            {
                case "applied":
                    applied++;
                    break;
                case "interview":
                    interview++;
                    break;
                case "offer":
                    offer++;
                    break;
                case "rejected":
                    rejected++;
                    break;
            }
        }

        Console.WriteLine("\n===== Application Status Summary =====");
        Console.WriteLine($"Applied:   {applied}");
        Console.WriteLine($"Interview: {interview}");
        Console.WriteLine($"Offer:     {offer}");
        Console.WriteLine($"Rejected:  {rejected}");
        Console.WriteLine("======================================\n");
    }

    public static void DeleteApplication(List<ApplicationEntry> applications)
    {
        if (applications.Count == 0)
        {
            Console.WriteLine("No applications to delete.");
            return;
        }

        for (int i = 0; i < applications.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {applications[i]}");
        }

        Console.Write("Enter the number of the application to delete: ");
        string input = Console.ReadLine();
        if (int.TryParse(input, out int index) && index >= 1 && index <= applications.Count)
        {
            applications.RemoveAt(index - 1);
            FileHandler.SaveApplications(applications);
            Console.WriteLine("Application deleted.");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

    public static void EditApplication(List<ApplicationEntry> applications)
    {
        if (applications.Count == 0)
        {
            Console.WriteLine("No applications to edit.");
            return;
        }

        for (int i = 0; i < applications.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {applications[i]}");
        }

        Console.Write("Enter the number of the application to edit: ");
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int index) || index < 1 || index > applications.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        ApplicationEntry selected = applications[index - 1];

        Console.Write($"New company name (current: {selected.Company}): ");
        string company = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(company)) selected.Company = company;

        Console.Write($"New job title (current: {selected.JobTitle}): ");
        string title = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(title)) selected.JobTitle = title;

        Console.Write($"New status (current: {selected.Status}): ");
        string status = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(status)) selected.Status = status;

        Console.Write($"New application date (YYYY-MM-DD, current: {selected.ApplicationDate.ToShortDateString()}): ");
        string dateInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(dateInput))
        {
            if (DateTime.TryParse(dateInput, out DateTime newDate))
            {
                selected.ApplicationDate = newDate;
            }
            else
            {
                Console.WriteLine("Invalid date format. Keeping existing date.");
            }
        }

        FileHandler.SaveApplications(applications);
        Console.WriteLine("Application updated.");
    }
}
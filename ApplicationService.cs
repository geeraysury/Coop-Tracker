using System;

public class ApplicationService 
{

    public static void AddApplication(List<ApplicationEntry> applications, IApplicationRepository repository){
        
        Console.Write("Enter the company name: ");
        string company = Console.ReadLine();

        Console.Write("Enter the job title: ");
        string jobTitle = Console.ReadLine();

        Console.Write("Enter the status (Not Applied, Applied, Interview, Offer, Rejected): ");
        string status = Console.ReadLine();

        DateTime applicationDate = DateTime.MinValue;

        if (!status.Equals("Not Applied", StringComparison.OrdinalIgnoreCase))
        {
            Console.Write("Enter the application date (YYYY-MM-DD): ");
            string dateInput = Console.ReadLine();

            while (!DateTime.TryParse(dateInput, out applicationDate))
            {
                Console.Write("Invalid date format. Please enter again (YYYY-MM-DD): ");
                dateInput = Console.ReadLine();
            }
        }


        Console.Write("Enter the application deadline (YYYY-MM-DD) or leave blank: ");
        string deadlineInput = Console.ReadLine();  

        DateTime? deadline = null;
        if (!string.IsNullOrWhiteSpace(deadlineInput))
        {
            if (DateTime.TryParse(deadlineInput, out DateTime parsedDeadline))
            {
                deadline = parsedDeadline;
            }
            else
            {
                Console.WriteLine("Invalid deadline format. Leaving it blank.");
            }
        }

        ApplicationEntry newApp = new ApplicationEntry(company, jobTitle, status, applicationDate, deadline);
        applications.Add(newApp);
        Console.WriteLine("Application added to the list");
        repository.SaveApplications(applications);
    }
    
    public static void UpdateStatus(List<ApplicationEntry> applications, IApplicationRepository repository)
    {
        if (applications.Count == 0)
        {
            Console.WriteLine("No applications found.");
            return;
        }

        for (int i = 0; i < applications.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {applications[i]}");
        }

        Console.Write("Enter the number of the application you would like to update: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int index) || index < 1 || index > applications.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        ApplicationEntry updateJob = applications[index - 1];
        Console.Write("Enter the new status (Not Applied, Applied, Interview, Offer, Rejected): ");
        string newStatus = Console.ReadLine();

        if (new[] { "Not Applied", "Applied", "Interview", "Offer", "Rejected" }
            .Contains(newStatus, StringComparer.OrdinalIgnoreCase))
        {
            updateJob.Status = newStatus;
            repository.SaveApplications(applications);
            Console.WriteLine("Status updated.");
        }
        else
        {
            Console.WriteLine("Please enter a valid status.");
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

    public static void DeleteApplication(List<ApplicationEntry> applications, IApplicationRepository repository)
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
            repository.SaveApplications(applications);
            Console.WriteLine("Application deleted.");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }


    public static void EditApplication(List<ApplicationEntry> applications, IApplicationRepository repository)
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

        Console.Write($"New application date (YYYY-MM-DD, current: {(selected.ApplicationDate.HasValue ? selected.ApplicationDate.Value.ToShortDateString() : "None")}): ");

        string dateInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out DateTime newDate))
        {
            selected.ApplicationDate = newDate;
        }

        Console.Write($"New deadline (YYYY-MM-DD, current: {(selected.Deadline.HasValue ? selected.Deadline.Value.ToShortDateString() : "None")}): ");
        string deadlineInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(deadlineInput))
        {
            if (DateTime.TryParse(deadlineInput, out DateTime newDeadline))
            {
                selected.Deadline = newDeadline;
            }
            else
            {
                Console.WriteLine("Invalid deadline format. Keeping existing deadline.");
            }
        }

        repository.SaveApplications(applications);
        Console.WriteLine("Application updated.");
    }


    public static void SortOrGroupApplications(List<ApplicationEntry> applications)
    {
        if (applications.Count == 0)
        {
            Console.WriteLine("No applications available.");
            return;
        }

        Console.WriteLine("Choose an option:\n1. Sort\n2. Group");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.WriteLine("Sort by:\n1. Date\n2. Company Name\n3. Status");
            string sortField = Console.ReadLine();

            switch (sortField)
            {
                case "1":
                    applications.Sort((a, b) =>
                    {
                        if (!a.ApplicationDate.HasValue && !b.ApplicationDate.HasValue) return 0;
                        if (!a.ApplicationDate.HasValue) return 1;  // Put nulls last
                        if (!b.ApplicationDate.HasValue) return -1;
                        return a.ApplicationDate.Value.CompareTo(b.ApplicationDate.Value);
                    });

                    break;
                case "2":
                    applications.Sort((a, b) => string.Compare(a.Company, b.Company, StringComparison.OrdinalIgnoreCase));
                    break;
                case "3":
                    applications.Sort((a, b) => string.Compare(a.Status, b.Status, StringComparison.OrdinalIgnoreCase));
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    return;
            }

            Console.WriteLine("\n=== Sorted Applications ===");
            for (int i = 0; i < applications.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {applications[i]}");
            }
        }
        else if (choice == "2")
        {
            Console.WriteLine("Group by:\n1. Status\n2. Company Name");
            string groupField = Console.ReadLine();

            Dictionary<string, List<ApplicationEntry>> grouped = new Dictionary<string, List<ApplicationEntry>>();

            foreach (var app in applications)
            {
                string key = groupField == "1" ? app.Status : app.Company;

                if (!grouped.ContainsKey(key))
                {
                    grouped[key] = new List<ApplicationEntry>();
                }

                grouped[key].Add(app);
            }

            Console.WriteLine("\n=== Grouped Applications ===");
            foreach (var group in grouped)
            {
                Console.WriteLine($"\n--- {group.Key} ---");
                foreach (var app in group.Value)
                {
                    Console.WriteLine($"{app}");
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }

    public static void CheckUpcomingDeadlines(List<ApplicationEntry> applications, int daysThreshold = 3)
    {
        DateTime today = DateTime.Today;
        bool found = false;

        foreach (var app in applications)
        {
            if (app.Status.Equals("Not Applied", StringComparison.OrdinalIgnoreCase) &&
                app.Deadline.HasValue &&
                (app.Deadline.Value - today).TotalDays <= daysThreshold &&
                (app.Deadline.Value - today).TotalDays >= 0)
            {
                Console.WriteLine($"Reminder: Apply to {app.Company} - {app.JobTitle} (Deadline: {app.Deadline.Value.ToShortDateString()})");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No upcoming deadlines within the next " + daysThreshold + " days.");
        }
    }


}
public class Program
{
    static void Main(string[] args)
    {
        IApplicationRepository repository = new JsonApplicationRepository();
        List<ApplicationEntry> applications = repository.LoadApplications();
        bool running = true;

        ApplicationService.CheckUpcomingDeadlines(applications); 

        while (running)
        {
            Console.WriteLine("\n==== INTERNSHIP TRACKER ====");
            Console.WriteLine("1. Add Application");
            Console.WriteLine("2. Update Application Status");
            Console.WriteLine("3. Filter Applications by Status");
            Console.WriteLine("4. Show Application Statistics");
            Console.WriteLine("5. Delete Application");
            Console.WriteLine("6. Edit Application");
            Console.WriteLine("7. Exit");
            Console.WriteLine("8. Sort or Group Applications");
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ApplicationService.AddApplication(applications, repository);
                    break;
                case "2":
                    ApplicationService.UpdateStatus(applications, repository);
                    break;
                case "3":
                    ApplicationService.FilterByStatus(applications);
                    break;
                case "4":
                    ApplicationService.ShowStats(applications);
                    break;
                case "5":
                    ApplicationService.DeleteApplication(applications, repository);
                    break;
                case "6":
                    ApplicationService.EditApplication(applications, repository);
                    break;
                case "7":
                    running = false;
                    break;
                case "8":
                    ApplicationService.SortOrGroupApplications(applications);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        Console.WriteLine("Goodbye!");
    }
}

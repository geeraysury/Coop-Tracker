using System;

public class ApplicationEntry
{
    public string Company { get; set; }
    public string JobTitle { get; set; }
    public string Status { get; set; } 
    public DateTime? ApplicationDate { get; set; }

    public DateTime? Deadline { get; set; }

    public ApplicationEntry(string company, string jobTitle, string status, DateTime? applicationDate, DateTime? deadline)
    {
        Company = company;
        JobTitle = jobTitle;
        Status = status;
        ApplicationDate = applicationDate;
        Deadline = deadline;
    }

    public override string ToString()
    {
        string appDate = ApplicationDate.HasValue ? ApplicationDate.Value.ToShortDateString() : "Not Applied";
        string deadlineStr = Deadline.HasValue ? Deadline.Value.ToShortDateString() : "No deadline";
        return $"{Company} | {JobTitle} | {appDate} | {Status} | Deadline: {deadlineStr}";
    }

}

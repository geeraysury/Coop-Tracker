using System;

public class ApplicationEntry
{
    public string Company { get; set; }
    public string JobTitle { get; set; }
    public string Status { get; set; } 
    public DateTime ApplicationDate { get; set; }

    public ApplicationEntry(string company, string jobTitle, string status, DateTime applicationDate)
    {
        Company = company;
        JobTitle = jobTitle;
        Status = status;
        ApplicationDate = applicationDate;
    }

    public override string ToString()
    {
        return $"{Company} | {JobTitle} | {ApplicationDate.ToShortDateString()} | {Status}";
    }
}

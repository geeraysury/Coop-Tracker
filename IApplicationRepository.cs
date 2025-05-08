using System.Collections.Generic;

public interface IApplicationRepository
{
    List<ApplicationEntry> LoadApplications();
    void SaveApplications(List<ApplicationEntry> applications);
}

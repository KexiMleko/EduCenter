namespace EduCenter.Domain.Enums;

public enum SessionStatus
{
    Scheduled,   // Planned and upcoming
    InProgress,  // Ongoing session
    Finished,    // Successfully completed
    Cancelled    // Called off before or during session
}


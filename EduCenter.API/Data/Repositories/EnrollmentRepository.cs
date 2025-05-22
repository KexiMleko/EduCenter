using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.Enrollments;
namespace EduCenter.API.Data.Repositories;
public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly DatabaseContext _appContext;
    public EnrollmentRepository(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public void AddEnrollment(Enrollment enrollment)
    {
        _appContext.Enrollments.Add(enrollment);
    }

    public void AddEnrollments(List<Enrollment> enrollments)
    {
        _appContext.Enrollments.AddRange(enrollments);
    }
}

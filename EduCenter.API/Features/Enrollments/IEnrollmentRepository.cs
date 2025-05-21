using EduCenter.API.Data.Models;

namespace EduCenter.API.Features.Enrollments;
public interface IEnrollmentRepository
{
    void AddEnrollment(Enrollment enrollment);
    void AddEnrollments(List<Enrollment> enrollments);
}

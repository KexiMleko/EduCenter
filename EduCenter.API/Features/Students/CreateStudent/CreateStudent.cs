using EduCenter.API.Data.Models;
using MediatR;
namespace EduCenter.API.Features.Students.CreateStudent;
public sealed record CreateStudentCommand(int levelId, string email, string firstName, string phoneNumber, string address, string lastName,
                                                                    string? note,
                                                                    int academicYear) : IRequest<Unit>;
public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, Unit>
{
    IUnitOfWork _uow;
    public CreateStudentHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<Unit> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            LevelId = request.levelId,
            Email = request.email,
            FirstName = request.firstName,
            LastName = request.lastName,
            PhoneNumber = request.phoneNumber,
            Address = request.address,
            Note = request.note,
            AcademicYear = request.academicYear,
            CreatedAt = DateTime.UtcNow
        };
        _uow.students.AddStudent(student);
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}


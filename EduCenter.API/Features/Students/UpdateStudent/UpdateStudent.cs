using EduCenter.API.Data.Models;
using MediatR;
namespace EduCenter.API.Features.Students.UpdateStudent;
public sealed record UpdateStudentCommand(int studentId, int levelId, string email, string firstName, string phoneNumber, string address, string lastName,
                                                                    string? note,
                                                                    int academicYear) : IRequest<Unit>;
public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, Unit>
{
    IUnitOfWork _uow;
    public UpdateStudentHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            Id = request.studentId,
            LevelId = request.levelId,
            Email = request.email,
            FirstName = request.firstName,
            LastName = request.lastName,
            PhoneNumber = request.phoneNumber,
            Address = request.address,
            Note = request.note,
            AcademicYear = request.academicYear
        };
        _uow.students.UpdateStudent(student);
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}


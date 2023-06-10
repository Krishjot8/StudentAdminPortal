
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Repositories
{
    public interface IStudentRepository
    {

        Task<List<Student>> GetStudentsAsync();


        Task <Student> GetStudentAsync(Guid studentId);


        Task<List<Gender>> GetGendersAsync();


        Task<bool> Exists(Guid studentid);


        Task<Student> UpdateStudent(Guid studentId, Student request);


        Task<Student> DeleteStudent(Guid studentId);


        Task<Student> AddStudent( Student request);


        Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl);
    }
}

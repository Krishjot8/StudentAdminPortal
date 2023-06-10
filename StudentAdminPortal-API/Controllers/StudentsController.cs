using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers
{


    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper,
            IImageRepository imageRepository)
        {
            if (imageRepository is null)
            {
                throw new ArgumentNullException(nameof(imageRepository));
            }

            this.studentRepository = studentRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await studentRepository.GetStudentsAsync();


            return Ok(mapper.Map<List<Student>>(students));
        }


        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            // Fetch Student Details
            var student = await studentRepository.GetStudentAsync(studentId);
            //Return  Student

            if (student == null)

            {

                return NotFound();
            }

            return Ok(mapper.Map<Student>(student));
        }




        [HttpPut]
        [Route("[controller]/{studentid:guid}")]

        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)

        {
            if (await studentRepository.Exists(studentId))
            {
                // Update Details
                var updatedStudent = await studentRepository.UpdateStudent(studentId, mapper.Map<DataModels.Student>(request));

                if (updatedStudent != null)
                {
                    return Ok(mapper.Map<Student>(updatedStudent));
                }
            }
            return NotFound();
        }




        [HttpDelete]
        [Route("[controller]/{studentid:guid}")]

        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)


        {
            if (await studentRepository.Exists(studentId))

            {
                var student = await studentRepository.DeleteStudent(studentId);

                return Ok(mapper.Map<Student>(student));

            }


            return NotFound();

        }



        [HttpPost]
        [Route("[controller]/Add")]

        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)

        {
            var student = await studentRepository.AddStudent(mapper.Map<DataModels.Student>(request));
            return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id },
                mapper.Map<Student>(student));
        }




        [HttpPost]
        [Route("[controller]/{studentid:guid}/upload-image")]

        public async Task<IActionResult> UploadImage([FromRoute] Guid studentId, IFormFile profileImage)
        {

            var validExtentions = new List<string>
            {


                ".jpeg",
                ".png",
                ".png",
                 ".jpg"

            };

            if (profileImage != null && profileImage.Length > 0 )
            {

                var extention = Path.GetExtension(profileImage.FileName);
              if (validExtentions.Contains(extention))
                {
                    if (await studentRepository.Exists(studentId))

                    {

                        var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);

                        var fileImagePath = await imageRepository.Upload(profileImage, fileName);



                        if (await studentRepository.UpdateProfileImage(studentId, fileImagePath))

                        {

                            return Ok(fileImagePath);

                        }

                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");


                    }


                }

                return BadRequest("This is not a valid Image format");
                }


            return NotFound();


        }


          


        }


    }




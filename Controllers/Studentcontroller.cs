using Microsoft.AspNetCore.Mvc;
using MyCollage_EF_Rep_AsyncAwait.DTO;
using MyCollage_EF_Rep_AsyncAwait.Models;
using MyCollage_EF_Rep_AsyncAwait.Repositories;
namespace Dotnetdf_MyCollage_Repository.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Studentcontroller : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public Studentcontroller(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            Student? student = await _studentRepository.LoginAsync(loginDto);
            return Ok(student == null ? "Login False" : student);

        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentReq addStudentReq)
        {
            var student = await _studentRepository.StoreAsync(addStudentReq);
            return Ok($"{student!.Name} {student.Family} with {student.Id} is registered");
        }
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetStudentAsync(int id)
        {
            var student = await _studentRepository.GetAsync(id);
            return Ok(student == null ? "student Not Found!!" : student);

        }
        [HttpGet]
        [Route("ReadAll")]
        public async Task<IActionResult> GetAllStudentAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return Ok(students != null ? students : "No Any students exsist");
        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateStudentAsync(int id, [FromBody] UpdateStudentReq updateStudent)
        {
            var student = await _studentRepository.UpdateAsync(id, updateStudent);
            return Ok(student == null ? "student Not Found!!" : $"{student.Name} is updated");
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            var student = await _studentRepository.DeleteAsync(id);
            return Ok(student == null ? "student Not Found!!" : $"{student.Name} is deleted");
        }



    }
}
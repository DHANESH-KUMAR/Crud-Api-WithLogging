using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using school.app.Filters;
using school.bll.Student;
using System;
using System.Threading.Tasks;

namespace school.app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[RequestResponseLogger]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;
        public StudentController(ILogger<StudentController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
            _logger.Log(LogLevel.Information, "Student Api Start");
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _studentService.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _studentService.Get(Id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentModel student)
        {
            try
            {
                var createdStudent = await _studentService.Create(student);

                if (createdStudent == null)
                {
                    _logger.LogInformation("Student Creation Failed");
                    return StatusCode(StatusCodes.Status400BadRequest, "Student Creation Failed");
                }
                return StatusCode(StatusCodes.Status200OK, createdStudent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }


        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put([FromRoute] long Id, [FromBody] StudentModel student)
        {
            if (Id != student.ID)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Student!!");
            }

            try
            {
                return StatusCode(StatusCodes.Status200OK, await _studentService.Update(student));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete([FromRoute] long Id, [FromBody] StudentModel student)
        {
            if (Id != student.ID)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Student!!");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _studentService.Delete(student));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }

    }
}

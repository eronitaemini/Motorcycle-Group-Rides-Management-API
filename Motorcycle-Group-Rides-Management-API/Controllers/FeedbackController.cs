using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using static Motorcycle_Group_Rides_Management_API.Dtos.FeedbackDto;
using Motorcycle_Group_Rides_Management_API.Services;

using Motorcycle_Group_Rides_Management_API.Dtos;
using Umbraco.Core.Persistence;
using static Umbraco.Core.Constants;

namespace Motorcycle_Group_Rides_Management_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : Controller
    {
        // GET: FeedbackController
        private IMapper _mapper;
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IMapper mapper, IFeedbackService feedbackService)
        {
            _mapper = mapper;
            _feedbackService = feedbackService;
        }



        [HttpGet]
        public async Task <IActionResult> GetAllFeedbacks()
        {

            var allFeedbacks = await _feedbackService.GetAllAsync();
            return Ok(allFeedbacks);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetfeedbackById(Guid id)
        {
            //  var feedback =  await _feedbackService.GetFeedbackByIdAsync(id);
            //  if (feedback != null)
            //  {
            //      var viewFeedbackDto = _mapper.Map<FeedbackDto>(feedback);
            //      return Ok(viewFeedbackDto);
            //  }

            //  return BadRequest();

            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }

            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                return NotFound("Feedback not found");
            }

            return Ok(feedback);
        }


      

    [HttpPost]
        public async Task<IActionResult> SubmitFeedback([FromBody] CreateFeedbackDto createFeedbackDto)
        {
            /*   var feedback = _mapper.Map<Feedback>(createFeedbackDto);
               feedback.DateSubmitted = DateTime.Now;
               _repo.Create(feedback);
               _repo.SaveChanges();*/
          //  await _feedbackService.CreateFeedbackAsync(createFeedbackDto);
            
         //   return new CreatedResult("location", createFeedbackDto.Comments);
            try
            {
                await _feedbackService.CreateFeedbackAsync(createFeedbackDto);
                return Ok("Feedback Submited Successfully !"); // Or appropriate response
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }



        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(Guid id, [FromBody] UpdateFeedbackDto updateFeedbackDto)
        {
            //   try
            //   {
            //       await _feedbackService.UpdateFeedbackAsync(id, updateFeedbackDto);
            //       return NoContent();

            //   }
            //   catch (ArgumentException argumentException)
            //   {
            //       Console.WriteLine(argumentException);
            //       return BadRequest("ID Mismatch");
            //   }
            //   catch (KeyNotFoundException keyNotFoundException)
            //   {
            //       Console.WriteLine(keyNotFoundException);
            //       return NotFound("Feedback not found");
            //   }

            if (updateFeedbackDto == null)
            {
                return BadRequest("updateFeedbackDto is required.");
            }

            try
            {
                await _feedbackService.UpdateFeedbackAsync(id, updateFeedbackDto);
                return NoContent();
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException);
                return BadRequest("ID Mismatch");
            }
            catch (KeyNotFoundException keyNotFoundException)
            {
                Console.WriteLine(keyNotFoundException);
                return NotFound("Feedback not found");
            }

           
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeedback(Guid id)
        {
            try
            {
                await _feedbackService.DeleteFeedbackAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException keyNotFoundExeption)
            {
                Console.WriteLine(keyNotFoundExeption);
                return NotFound("The feedback was not found and couldn't be deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "An error occurred while deleting the feedback");
            }

        }



        [HttpGet("search")]
        public async Task<ActionResult> GetFeedbacks(string searchQuery = "", string sortBy = "Comments", bool ascending = true, int pageNumber = 1, int pageSize = 10, int? minRating = null, int? maxRating = null)
        {
            var feedbacks = await _feedbackService.GetFeedbacksAsync(searchQuery, sortBy, ascending, pageNumber, pageSize, minRating, maxRating);
            return Ok(feedbacks);
        }
    }
}

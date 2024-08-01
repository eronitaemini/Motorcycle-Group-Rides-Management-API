using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using static Motorcycle_Group_Rides_Management_API.Dtos.GroupDto;
using static Motorcycle_Group_Rides_Management_API.Dtos.FeedbackDto;

namespace Motorcycle_Group_Rides_Management_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : Controller
    {
        // GET: FeedbackController
        private IMapper _mapper;
        private IFeedbackRepository _repo;
        public FeedbackController(IMapper mapper, IFeedbackRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }



        [HttpGet]
        public ActionResult<IEnumerable<ViewFeedbackDto>> GetAllFeedbacks(Guid groupRideId)
        {
            try
            {
                var feedbacks = _repo.GetAllFeedbackByGroupRidesId(groupRideId);

                var viewFeedbackDtoList = _mapper.Map<IEnumerable<ViewFeedbackDto>>(feedbacks);

                return Ok(viewFeedbackDtoList);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        public ActionResult<ViewGroupDto> GetfeedbackById(Guid id)
        {
            var feedback = _repo.GetById(id);
            if (feedback != null)
            {
                var viewFeedbackDto = _mapper.Map<ViewFeedbackDto>(feedback);
                return Ok(viewFeedbackDto);
            }

            return BadRequest();
        }

        [HttpPost]
        public ActionResult SubmitFeedback([FromBody] CreateFeedbackDto createFeedbackDto)
        {
         /*   var feedback = _mapper.Map<Feedback>(createFeedbackDto);
            feedback.DateSubmitted = DateTime.Now;
            _repo.Create(feedback);
            _repo.SaveChanges();*/
            return new CreatedResult("location", createFeedbackDto.Comments);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateFeedback(int id, [FromBody] UpdateFeedbackDto updateFeedbackDto)
        {
            if (id != updateFeedbackDto.FeedbackId)
            {
                return BadRequest();
            }

            var feedback = _mapper.Map<Feedback>(updateFeedbackDto);
            _repo.Update(feedback);
            _repo.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        public ActionResult DeleteFeedback(Guid id)
        {
            var feedback = _repo.GetById(id);

            if (feedback != null)
            {
                _repo.Delete(id);
                _repo.SaveChanges();
                return NoContent();
            }
            return BadRequest();

        }

    }
}

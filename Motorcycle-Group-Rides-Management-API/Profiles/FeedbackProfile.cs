using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Models;
using System.Security.Cryptography.X509Certificates;
using static Motorcycle_Group_Rides_Management_API.Dtos.FeedbackDto;


namespace Motorcycle_Group_Rides_Management_API.Profiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile() {

            CreateMap<Feedback, ViewFeedbackDto>();
            CreateMap<CreateFeedbackDto, Feedback>();
            CreateMap<UpdateFeedbackDto, Feedback>();
        }
    }
}

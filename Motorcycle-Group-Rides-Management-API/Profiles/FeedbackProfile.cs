using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Models;
using System.Security.Cryptography.X509Certificates;
using static Motorcycle_Group_Rides_Management_API.Dtos.FeedbackDto;


namespace Motorcycle_Group_Rides_Management_API.Profiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile() {

            CreateMap<ViewFeedbackDto, Feedback>();
            CreateMap<Feedback, ViewFeedbackDto>();
            CreateMap<CreateFeedbackDto, Feedback>().ReverseMap();
         //   CreateMap<UpdateFeedbackDto, Feedback>();
         //   CreateMap<Feedback, UpdateFeedbackDto>();

            CreateMap<Feedback, FeedbackDto>().ReverseMap();
            CreateMap<Feedback, UpdateFeedbackDto>().ReverseMap();
        }
    }
}

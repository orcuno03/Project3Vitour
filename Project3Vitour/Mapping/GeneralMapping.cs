using AutoMapper;
using Project3Vitour.Dtos.CategoryDtos;
using Project3Vitour.Dtos.DestinationDtos;
using Project3Vitour.Dtos.ReservationDtos;
using Project3Vitour.Dtos.ReviewDtos;
using Project3Vitour.Dtos.TourDtos;
using Project3Vitour.Dtos.TourImageDtos;
using Project3Vitour.Dtos.TourPlanDtos;
using Project3Vitour.Entities;

namespace Project3Vitour.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();

            CreateMap<Tour, CreateTourDto>().ReverseMap();
            CreateMap<Tour, ResultTourDto>().ReverseMap();
            CreateMap<Tour, UpdateTourDto>().ReverseMap();
            CreateMap<Tour, GetTourByIdDto>().ReverseMap();

            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, ResultReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();
            CreateMap<Review, GetReviewByIdDto>().ReverseMap();
            CreateMap<Review, ResultReviewByTourIdDto>().ReverseMap();

            CreateMap<Destination, CreateDestinationDto>().ReverseMap();
            CreateMap<Destination, ResultDestinationDto>().ReverseMap();
            CreateMap<Destination, UpdateDestinationDto>().ReverseMap();
            CreateMap<Destination, GetDestinationByIdDto>().ReverseMap();

            CreateMap<TourPlan, CreateTourPlanDto>().ReverseMap();
            CreateMap<TourPlan, ResultTourPlanDto>().ReverseMap();
            CreateMap<TourPlan, UpdateTourPlanDto>().ReverseMap();
            CreateMap<TourPlan, GetTourPlanByIdDto>().ReverseMap();
            CreateMap<TourPlan, ResultTourPlanByTourIdDto>().ReverseMap();

            CreateMap<TourImage, CreateTourImageDto>().ReverseMap();
            CreateMap<TourImage, ResultTourImageDto>().ReverseMap();
            CreateMap<TourImage, UpdateTourImageDto>().ReverseMap();
            CreateMap<TourImage, GetTourImageByIdDto>().ReverseMap();
            CreateMap<TourImage, ResultTourImageByTourIdDto>().ReverseMap();

            CreateMap<Reservation, CreateReservationDto>().ReverseMap();
            CreateMap<Reservation, ResultReservationDto>().ReverseMap();
            CreateMap<Reservation, UpdateReservationDto>().ReverseMap();
            CreateMap<Reservation, GetReservationByIdDto>().ReverseMap();
            CreateMap<Reservation, ResultReservationByTourIdDto>().ReverseMap();
        }
    }
}

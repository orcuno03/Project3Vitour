namespace Project3Vitour.Dtos.ReviewDtos
{
    public class GetReviewByIdDto
    {
        public string ReviewId { get; set; }
        public string NameSurname { get; set; }
        public string Detail { get; set; }
        public int GuideScore { get; set; }
        public int AccommodationScore { get; set; }
        public int TransportScore { get; set; }
        public int ComfortScore { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool Status { get; set; }
        public string TourId { get; set; }

        public double AverageScore => (GuideScore + AccommodationScore + TransportScore + ComfortScore) / 4.0;
    }
}

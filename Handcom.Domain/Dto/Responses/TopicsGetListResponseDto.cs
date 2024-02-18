using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.Dto.Responses
{
    public class TopicsGetListResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public double AverageCompanyRating { get; set; }
        public int ReviewsQuantity { get; set; }

        public TopicsGetListResponseDto(Guid id, string name,  double averageCompanyRating, int reviewsQuantity)
        {
            Id = id;
            Name = name;          
            AverageCompanyRating = averageCompanyRating;
            ReviewsQuantity = reviewsQuantity;
        }
    }
}

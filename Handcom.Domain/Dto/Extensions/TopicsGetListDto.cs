using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.Dto.Extensions
{
    public class TopicsGetListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<int> AverageUserRating { get; set; } = new List<int>();
        public int ReviewsQuantity { get; set; }
    }
}

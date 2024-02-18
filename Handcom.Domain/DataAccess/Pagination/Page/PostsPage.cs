using Handcom.Domain.DataAccess.Pagination.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.DataAccess.Pagination.Page
{
    public class PostsPage : Pageable
    {
        public string? Topic { get; set; }
    }
}

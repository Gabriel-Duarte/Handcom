﻿using Handcom.Domain.DataAccess.Interfaces.Base;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Domain.DataAccess.Interfaces
{
    public interface ICommentsRepository : IRepository<Comments>
    {
        Task<Page<Comments>> GetCommentsAsync(CommentsPage commentsPage, CancellationToken cancellationToken);
    }
}
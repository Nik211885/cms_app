﻿using backend.Core.Entities.CMS;
using backend.DTOs.CMS.Request;
using UC.Core.Models.FormData;

namespace backend.Infrastructure.Repository.CMS.FeedBack
{
    public interface IFeedBackRepository
    {
        Task AddAsync(cms_feedbacks feedBack);
        Task<dynamic> SearchFeedBackAsync(OSearch search, bool isPagination = false, int currentPage = 1, int pageSize = 20);
    }
}

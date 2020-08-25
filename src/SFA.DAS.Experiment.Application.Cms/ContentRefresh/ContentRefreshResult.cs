using System;
using System.Collections.Generic;

namespace SFA.DAS.Experiment.Application.Cms.ContentRefresh
{
    public class ContentRefreshResult
    {
        public List<string> ArticlesStored { get; set; }
        public bool Success { get; set; }
        public Exception Exception {get;set;}
    }
}
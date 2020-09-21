using System;
using System.Collections.Generic;

namespace SFA.DAS.Experiment.Application.Cms.ContentPublish
{
    public class ContentPublishResponse
    {
        public bool Success { get; set; }
        public List<string> ArticlesPublished { get; set; }
        public Exception Exception {get;set;}
        public string Message { get; internal set; }
    }
}


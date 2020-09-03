using System;
using System.Collections.Generic;

namespace SFA.DAS.Experiment.Application.Cms.ContentRemove
{
    public class ContentRemoveResponse
    {
        public bool Success { get; set; }
        public List<string> ArticlesRemoved { get; set; }
        public Exception Exception {get;set;}
        public string Message { get; internal set; }
    }
}


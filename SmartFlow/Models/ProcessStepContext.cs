﻿using System.Collections.Generic;
using SmartFlow.Models.Flow;

namespace SmartFlow.Models
{
    public class ProcessStepContext
    {
        internal ProcessExecution ProcessExecution { get; set; }
        internal ProcessExecutionStep ProcessExecutionStep { get; set; }
        internal ProcessExecutionStepDetail ProcessExecutionStepDetail { get; set; }
        internal ProcessUser ProcessUser { get; set; }
        internal ProcessStepInput ProcessStepInput { get; set; }
        public ProcessAction Action { get; set; }
        public Dictionary<string, object> Data { get; set; }

        internal ProcessStepContext()
        {
            Data = new Dictionary<string, object>();
        }

        internal void ClearDictionary()
        {
            Data = new Dictionary<string, object>();
        }
    }
}

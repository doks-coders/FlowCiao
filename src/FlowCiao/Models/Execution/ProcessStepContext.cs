﻿using System.Collections.Generic;

namespace FlowCiao.Models.Execution
{
    public class ProcessStepContext
    {
        internal ProcessExecution ProcessExecution { get; set; }
        internal ProcessExecutionStep ProcessExecutionStep { get; set; }
        internal ProcessExecutionStepDetail ProcessExecutionStepDetail { get; set; }
        public Dictionary<object, object> Data { get; set; }

        internal ProcessStepContext()
        {
            Data = new Dictionary<object, object>();
        }

        internal void ClearDictionary()
        {
            Data = new Dictionary<object, object>();
        }
    }
}

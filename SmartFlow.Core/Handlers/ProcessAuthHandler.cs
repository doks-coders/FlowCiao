﻿using SmartFlow.Core.Models;
using System;
using SmartFlow.Core.Persistence.Interfaces;

namespace SmartFlow.Core.Handlers
{
    internal class ProcessAuthHandler : WorkflowHandler
    {
        public ProcessAuthHandler(IProcessRepository processRepository
            , IProcessStepService processStepManager) : base(processRepository, processStepManager)
        {
        }

        public override ProcessResult Handle(ProcessStepContext processStepContext)
        {
            try
            {
                return NextHandler.Handle(processStepContext);
            }
            catch (Exception exception)
            {
                return new ProcessResult
                {
                    Status = ProcessResultStatus.Failed,
                    Message = exception.Message
                };
            }
        }

        public override ProcessResult RollBack(ProcessStepContext processStepContext)
        {
            return PreviousHandler?.RollBack(processStepContext) ?? new ProcessResult
            {
                Status = ProcessResultStatus.Failed
            };
        }
    }
}

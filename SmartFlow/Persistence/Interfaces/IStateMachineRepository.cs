﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartFlow.Interfaces;
using SmartFlow.Models;
using SmartFlow.Models.Flow;
using ProcessAction = SmartFlow.Models.Flow.ProcessAction;

namespace SmartFlow.Persistence.Interfaces
{
    public interface IStateMachineRepository
    {
        Task<List<TransitionAction>> GetStateTransitions(Process process, State state);
        Task<List<TransitionAction>> GetActiveTransitions(ProcessEntity entityp, Guid ProcessId);
        Task<List<Activity>> GetStateActivities(State state, Group group);
        Task<bool> RemoveActiveProcessStep(ProcessEntity entity);
        Task<Process> GetProcess(Guid userId, Guid requestTypeId);
        Task<ISmartFlow> GetProcess(Guid processId = default, string key = default);
        Task<Guid> Create(Process entity);
        Task<bool> CreateProcessStep(ProcessExecutionStep entity);
        Task<bool> CompleteProgressAction(ProcessExecutionStep processStep, ProcessAction action);
        Task<State> GetState(Guid stateId);
        Task<ProcessExecutionStep> GetActiveProcessStep(ProcessEntity entity);
        Task<bool> SetToHistory(Guid EntityId);
        Task<bool> SetToHistoryForGoToStep(Guid newId, Guid entityId, Guid targetStatus, Guid processGuid);
        Task<ProcessExecutionStep> GetLastProcessStepHistoryItem(Guid ticketId);
        Task<bool> AddProcessStepHistoryActivity(ProcessStepHistoryActivity processStepHistoryActivity);
        Task<List<Activity>> GetTransitionActivities(Transition transition);
        Task<State> GetStatusById(Guid statusId);
        Task<FuncResult> RemoveEntireFlow(ProcessExecutionStep processStep);
        Task<bool> ProcessStepCommentRead(ProcessEntity entity);
        Task<FuncResult> ApplyProcessStepComment(ProcessEntity entity, ProcessUser processUser, string comment, List<Guid> attachmentIds = default);
        Task<bool> ApplyProcessStepCommentAttachment(Guid processStepCommentId, Guid attachmentId);
        State GetStartState();
    }
}
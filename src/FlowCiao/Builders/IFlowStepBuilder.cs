﻿using System;
using System.Collections.Generic;
using FlowCiao.Interfaces;
using FlowCiao.Models.Builder.Json;
using FlowCiao.Models.Core;

namespace FlowCiao.Builders
{
    public interface IFlowStepBuilder
    {
        public State InitialState { get; set; }
        internal List<Action<Transition>> AllowedTransitionsBuilders { get; set; }
        public IProcessActivity OnEntryActivity { get; set; }
        public IProcessActivity OnExitActivity { get; set; }
        public IFlowStepBuilder For(State state);
        public IFlowStepBuilder AllowSelf(List<int> triggers);
        public IFlowStepBuilder Allow(State state, List<int> triggers);
        public IFlowStepBuilder Allow(State state, int trigger, Func<bool> condition = null);
        public IFlowStepBuilder OnEntry<TActivity>() where TActivity : IProcessActivity, new();
        public IFlowStepBuilder OnEntry(string activityName);
        public IFlowStepBuilder OnExit<TActivity>() where TActivity : IProcessActivity, new();
        public IFlowStepBuilder OnExit(string activityName);
        public IFlowStepBuilder AssignToUser(Func<string> userId);
        public IFlowStepBuilder Build(List<State> states, JsonStep jsonStep);
    }
}

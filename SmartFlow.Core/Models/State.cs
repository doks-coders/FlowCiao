﻿using System;

namespace SmartFlow.Core.Models
{
    public class State
    {
        public State(int code, string name)
        {
            Code = code;
            Name = name;
        }

        public State()
        {

        }

        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StateType TypeId { get; set; }
        public Guid ProcessId { get; set; }
        public bool IsFinal { get; set; }
        public bool IsInitial { get; set; }

    }
}

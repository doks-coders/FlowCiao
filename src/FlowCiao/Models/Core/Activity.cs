﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using FlowCiao.Interfaces;
using Newtonsoft.Json;

namespace FlowCiao.Models.Core
{
    public class Activity : BaseEntity
    {
        public string Name { get; set; }

        public int ActivityType { get; set; } = 1;

        [MaxLength(500)]
        public string ActorName { get; set; }

        [MaxLength(1000000)]
        public byte[] ActorContent { get; set; }
        
        public List<State> States { get; set; } = null!;

        public List<Transition> Transitions { get; set; } = null!;

        public List<TransitionActivity> TransitionActivities { get; set; } = null!;

        public List<StateActivity> StateActivities { get; set; } = null!;

        [JsonIgnore]
        [NotMapped]
        public IFlowActivity Actor { get; set; }

        public Activity()
        {
        }

        public Activity(IFlowActivity actor)
        {
            Actor = actor;
            var actorType = actor.GetType();
            if (string.IsNullOrWhiteSpace(actorType.FullName))
            {
                return;
            }

            Name = actorType.FullName.Split('.').Last();
            ActorName = actorType.FullName;
        }
    }
}
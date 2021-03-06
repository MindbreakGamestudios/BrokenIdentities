﻿using Assets.Scripts.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.HoveEffects
{
    [UpdateAfter(typeof(ClickInteractionSystem))]
    public class HoverAnimationSystem : ComponentSystem
    {
        struct Data
        {
            public readonly int Length;
            public ComponentArray<ClickInteractionComponent> Clicks;
            public ComponentArray<Animator> Animators;
            public ComponentArray<HoverAnimationData> HoverAnimations;
        }

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            for (int i = 0; i < data.Length; i++)
            {
                var click = data.Clicks[i];
                var animator = data.Animators[i];
                var hover = data.HoverAnimations[i];

                var enabled = click.IsHovered && hover.Enabled;

                if (enabled != click.WasHovered)    //Don't remove. Setting animation parameters is not cheap.
                {
                    animator.SetBool(hover.HoverParameterName, enabled);
                }
            }
        }
    }
}

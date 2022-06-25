﻿using System.Diagnostics.CodeAnalysis;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components.General;
using ECS.Game.Systems.General;
using ECS.Views.General;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Game.Systems.GameCycle
{
    public class TriggersDistanceSystem : IEcsUpdateSystem
    {
        private readonly EcsFilter<DistanceTriggerComponent, LinkComponent>.Exclude<IsDelayCleanUpComponent> _triggers;
        private readonly EcsFilter<InteractWithTriggerComponent, LinkComponent> _interactors;

        private EcsEntity _triggerEntity;
        private Transform _interactor;
        private DistanceTriggerView _distanceTriggerView;

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Run()
        {
            foreach (var i in _triggers)
            {
                _triggerEntity = _triggers.GetEntity(i);
                _distanceTriggerView = _triggers.Get2(i).Get<DistanceTriggerView>();
            
                if (!_distanceTriggerView.gameObject.activeSelf ||
                    !_distanceTriggerView.gameObject.activeInHierarchy)
                    continue;
                
                foreach (var j in _interactors)
                {
                    _interactor = _interactors.Get2(j).View.Transform;
                    
                    if (Vector3.Distance(_interactor.position, _distanceTriggerView.Transform.position) > _distanceTriggerView.GetTriggerDistance())
                        continue;
                    
                    foreach (var unlockable in _distanceTriggerView.GetUnlockable())
                        unlockable?.SetActive(true);
                    foreach (var lockable in _distanceTriggerView.GetLockable())
                        lockable?.SetActive(false);
                    foreach (var linkableView in _distanceTriggerView.GetActivatedViews())
                        linkableView?.Entity.Get<EventTriggerActivateComponent>();
                    
                    _triggerEntity.Get<IsDelayCleanUpComponent>().Delay = 1.5f;
                }
            }
        }
    }

    public struct InteractWithTriggerComponent : IEcsIgnoreInFilter
    {
        
    }
}
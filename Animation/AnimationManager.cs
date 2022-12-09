using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.Animation
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator AnimatorController;
        public event Action OnBeforeStateChanged;
        public event Action OnAfterStateChanged;

        public virtual void Play(string stateName, float normalizedTransitionDuration = .5f, int layer = -1, float normalizedTimeOffset = float.NegativeInfinity, float normalizedTransitionTime = 0.0f)
        {
            OnBeforeStateChanged?.Invoke();
            AnimatorController.CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset);
            OnAfterStateChanged?.Invoke();
        }
        public virtual void Play(int stateHashName, float normalizedTransitionDuration = .5f, int layer = -1, float normalizedTimeOffset = 0.0f, float normalizedTransitionTime = 0.0f)
        {
            OnBeforeStateChanged?.Invoke();
            AnimatorController.CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset);
            OnAfterStateChanged?.Invoke();
        }

        public virtual void Play(int stateHashName, int layer = -1, float fixedTimeOffset = 0.0f, float normalizedTransitionTime = 0.0f, float fixedTransitionDuration = .5f)
        {
            OnBeforeStateChanged?.Invoke();
            AnimatorController.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset);
            OnAfterStateChanged?.Invoke();
        }
        public virtual void Play(string stateName, int layer = -1, float fixedTimeOffset = 0.0f, float normalizedTransitionTime = 0.0f, float fixedTransitionDuration = .5f)
        {
            OnBeforeStateChanged?.Invoke();
            AnimatorController.CrossFadeInFixedTime(stateName, fixedTransitionDuration, layer, fixedTimeOffset);
            OnAfterStateChanged?.Invoke();
        }

        public void FireTrigger(string trigger)
        {
            AnimatorController.SetTrigger(trigger);
        }
        public void FireTrigger(int trigger)
        {
            AnimatorController.SetTrigger(trigger);
        }
    }
}

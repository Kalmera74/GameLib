using System;
using System.Collections;
using System.Collections.Generic;
using GameLib.ScriptableObjectBases.EventDelegates;
using UnityEngine;

namespace GameLib.Animation
{
    [RequireComponent(typeof(Animator))]
    public class AnimationManager : MonoBehaviour
    {
        /// <summary>
        /// An event that is triggered before the animation state is changed.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO OnBeforeStateChanged;
        /// <summary>
        /// An event that is triggered after the animation state is changed.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO OnAfterStateChanged;
        /// <summary>
        /// The Animator controller attached to the game object.
        /// </summary>
        private Animator _animatorController;

        void Start()
        {
            _animatorController = GetComponent<Animator>();
        }

        /// <summary>
        /// Plays the specified animation state.
        /// </summary>
        /// <param name="stateName">The name of the animation state to play.</param>
        /// <param name="normalizedTransitionDuration">The duration of the transition between the current state and the new state, normalized to the range [0, 1].</param>
        /// <param name="layer">The layer on which the state is played. If the layer is negative, the state is played on the default layer.</param>
        /// <param name="normalizedTimeOffset">The offset of the state's normalized time, in the range [0, 1].</param>
        /// <param name="normalizedTransitionTime">The normalized time of the transition between the current state and the new state, in the range [0, 1].</param>
        public virtual void Play(string stateName, float normalizedTransitionDuration = .5f, int layer = -1, float normalizedTimeOffset = float.NegativeInfinity, float normalizedTransitionTime = 0.0f)
        {
            OnBeforeStateChanged?.FireEvent();
            _animatorController.CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset);
            OnAfterStateChanged?.FireEvent();
        }
        /// <summary>
        /// Plays the specified animation state.
        /// </summary>
        /// <param name="stateHashName">The hash code of the name of the animation state to play.</param>
        /// <param name="normalizedTransitionDuration">The duration of the transition between the current state and the new state, normalized to the range [0, 1].</param>
        /// <param name="layer">The layer on which the state is played. If the layer is negative, the state is played on the default layer.</param>
        /// <param name="normalizedTimeOffset">The offset of the state's normalized time, in the range [0, 1].</param>
        /// <param name="normalizedTransitionTime">The normalized time of the transition between the current state and the new state, in the range [0, 1].</param>
        public virtual void Play(int stateHashName, float normalizedTransitionDuration = .5f, int layer = -1, float normalizedTimeOffset = 0.0f, float normalizedTransitionTime = 0.0f)
        {
            OnBeforeStateChanged?.FireEvent();
            _animatorController.CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset);
            OnAfterStateChanged?.FireEvent();
        }
        /// <summary>
        /// Plays the specified animation state in fixed time.
        /// </summary>
        /// <param name="stateHashName">The hash code of the name of the animation state to play.</param>
        /// <param name="layer">The layer on which the state is played. If the layer is negative, the state is played on the default layer.</param>
        /// <param name="fixedTimeOffset">The offset of the state's fixed time, in seconds.</param>
        /// <param name="normalizedTransitionTime">The normalized time of the transition between the current state and the new state, in the range [0, 1].</param>
        /// <param name="fixedTransitionDuration">The duration of the transition between the current state and the new state, in seconds.</param>
        public virtual void Play(int stateHashName, int layer = -1, float fixedTimeOffset = 0.0f, float normalizedTransitionTime = 0.0f, float fixedTransitionDuration = .5f)
        {
            OnBeforeStateChanged?.FireEvent();
            _animatorController.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset);
            OnAfterStateChanged?.FireEvent();
        }
        /// <summary>
        /// Plays the specified animation state in fixed time.
        /// </summary>
        /// <param name="stateName">The name of the animation state to play.</param>
        /// <param name="layer">The layer on which the state is played. If the layer is negative, the state is played on the default layer.</param>
        /// <param name="fixedTimeOffset">The offset of the state's fixed time, in seconds.</param>
        /// <param name="normalizedTransitionTime">The normalized time of the transition between the current state and the new state, in the range [0, 1].</param>
        /// <param name="fixedTransitionDuration">The duration of the transition between the current state and the new state, in seconds.</param>
        public virtual void Play(string stateName, int layer = -1, float fixedTimeOffset = 0.0f, float normalizedTransitionTime = 0.0f, float fixedTransitionDuration = .5f)
        {
            OnBeforeStateChanged?.FireEvent();
            _animatorController.CrossFadeInFixedTime(stateName, fixedTransitionDuration, layer, fixedTimeOffset);
            OnAfterStateChanged?.FireEvent();
        }
        /// <summary>
        /// Fires the specified trigger on the Animator controller.
        /// </summary>
        /// <param name="trigger">The name of the trigger to fire.</param>
        public void FireTrigger(string trigger)
        {
            _animatorController.SetTrigger(trigger);
        }
        /// <summary>
        /// Fires the specified trigger on the Animator controller.
        /// </summary>
        /// <param name="trigger">The hash code of the name of the trigger to fire.</param>
        public void FireTrigger(int trigger)
        {
            _animatorController.SetTrigger(trigger);
        }
    }
}

using GameLib.ScriptableObjectBases.EventDelegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.Bridges
{
    /// <summary>
    /// ScriptableObject class that represents a haptic controller.
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Vibration/Default", fileName = "Haptic_Controller")]
    public class HapticSO : ScriptableObject
    {
        /// <summary>
        /// Event delegate for playing a selection haptic effect.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlaySelectionHapticEvent;
        /// <summary>
        /// Event delegate for playing a success haptic effect.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlaySuccessHapticEvent;
        /// <summary>
        /// Event delegate for playing a warning haptic effect.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlayWarningHapticEvent;
        /// <summary>
        /// Event delegate for playing a failure haptic effect.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlayFailureHapticEvent;
        /// <summary>
        /// Event delegate for playing a light impact haptic effect.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlayLightImpactHapticEvent;
        /// <summary>
        /// Event delegate for playing a medium impact haptic effect.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlayMediumImpactEvent;
        /// <summary>
        /// Event delegate for playing a heavy impact haptic effect.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlayHeavyImpactHapticEvent;
        /// <summary>
        /// Event delegate for playing a rigid impact haptic effect.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlayRigidImpactEvent;
        /// <summary>
        /// Event delegate for playing a soft impact haptic effect.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlaySoftImpactEvent;

        /// <summary>
        /// Fires the event delegate for playing a selection haptic effect.
        /// </summary>
        public void PlaySelectionHaptic()
        {
            PlaySelectionHapticEvent.FireEvent();
        }

        /// <summary>
        /// Fires the event delegate for playing a success haptic effect.
        /// </summary>
        public void PlaySuccessHaptic()
        {
            PlaySuccessHapticEvent.FireEvent();
        }

        /// <summary>
        /// Fires the event delegate for playing a warning haptic effect.
        /// </summary>
        public void PlayWarningHaptic()
        {
            PlayWarningHapticEvent.FireEvent();
        }

        /// <summary>
        /// Fires the event delegate for playing a failure haptic effect.
        /// </summary>
        public void PlayFailureHaptic()
        {
            PlayFailureHapticEvent.FireEvent();
        }

        /// <summary>
        /// Fires the event delegate for playing a light impact haptic effect.
        /// </summary>
        public void PlayLightImpactHaptic()
        {
            PlayLightImpactHapticEvent.FireEvent();
        }

        /// <summary>
        /// Fires the event delegate for playing a medium impact haptic effect.
        /// </summary>
        public void PlayMediumImpactHaptic()
        {
            PlayMediumImpactEvent.FireEvent();
        }

        /// <summary>
        /// Fires the event delegate for playing a heavy impact haptic effect.
        /// </summary>
        public void PlayHeavyImpactHaptic()
        {
            PlayHeavyImpactHapticEvent.FireEvent();
        }

        /// <summary>
        /// Fires the event delegate for playing a rigid impact haptic effect.
        /// </summary>
        public void PlayRigidImpactHaptic()
        {
            PlayRigidImpactEvent.FireEvent();
        }

        /// <summary>
        /// Fires the event delegate for playing a soft impact haptic effect.
        /// </summary>
        public void PlaySoftImpactHaptic()
        {
            PlaySoftImpactEvent.FireEvent();
        }
    }
}

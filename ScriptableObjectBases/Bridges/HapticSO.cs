using GameLib.ScriptableObjectBases.EventDelegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.Bridges
{
    [CreateAssetMenu(menuName = "Mobiversite/Vibration/Default", fileName = "Haptic_Controller")]
    public class HapticSO : ScriptableObject
    {
        [SerializeField] private VoidEventDelegateSO PlaySelectionHapticEvent;
        [SerializeField] private VoidEventDelegateSO PlaySuccessHapticEvent;
        [SerializeField] private VoidEventDelegateSO PlayWarningHapticEvent;
        [SerializeField] private VoidEventDelegateSO PlayFailureHapticEvent;
        [SerializeField] private VoidEventDelegateSO PlayLightImpactHapticEvent;
        [SerializeField] private VoidEventDelegateSO PlayMediumImpactEvent;
        [SerializeField] private VoidEventDelegateSO PlayHeavyImpactHapticEvent;
        [SerializeField] private VoidEventDelegateSO PlayRigidImpactEvent;
        [SerializeField] private VoidEventDelegateSO PlaySoftImpactEvent;



        public void PlaySelectionHaptic()
        {
            PlaySelectionHapticEvent.FireEvent();

        }
        public void PlaySuccessHaptic()
        {
            PlaySuccessHapticEvent.FireEvent();
        }
        public void PlayWarningHaptic()
        {
            PlayWarningHapticEvent.FireEvent();
        }
        public void PlayFailureHaptic()
        {
            PlayFailureHapticEvent.FireEvent();
        }
        public void PlayLightImpactHaptic()
        {
            PlayLightImpactHapticEvent.FireEvent();
        }
        public void PlayMediumImpactHaptic()
        {
            PlayMediumImpactEvent.FireEvent();
        }
        public void PlayHeavyImpactHaptic()
        {
            PlayHeavyImpactHapticEvent.FireEvent();
        }
        public void PlayRigidImpactHaptic()
        {
            PlayRigidImpactEvent.FireEvent();
        }
        public void PlaySoftImpactHaptic()
        {
            PlaySoftImpactEvent.FireEvent();
        }
    }
}

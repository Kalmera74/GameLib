using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lofelt.NiceVibrations;
using static Lofelt.NiceVibrations.HapticPatterns;

using UnityEngine;

namespace Mobiversite
{

    public class VibrationManager : MonoBehaviour
    {

        [SerializeField] private HapticSource Source;
        [SerializeField] private BooleanEventDelegateSO StateChangeRequest;
        [SerializeField] private VoidEventDelegateSO PlaySelectionHapticRequest;
        [SerializeField] private VoidEventDelegateSO PlaySuccessHapticRequest;
        [SerializeField] private VoidEventDelegateSO PlayWarningHapticRequest;
        [SerializeField] private VoidEventDelegateSO PlayFailureHapticRequest;
        [SerializeField] private VoidEventDelegateSO PlayLightImpactHapticRequest;
        [SerializeField] private VoidEventDelegateSO PlayMediumImpactRequest;
        [SerializeField] private VoidEventDelegateSO PlayHeavyImpactHapticRequest;
        [SerializeField] private VoidEventDelegateSO PlayRigidImpactRequest;
        [SerializeField] private VoidEventDelegateSO PlaySoftImpactRequest;

        private bool _canPlayHaptic = true;

        void Awake()
        {
            PlaySelectionHapticRequest.Subscribe(PlaySelectionHaptic);
            PlaySuccessHapticRequest.Subscribe(PlaySuccessHaptic);
            PlayWarningHapticRequest.Subscribe(PlayWarningHaptic);
            PlayFailureHapticRequest.Subscribe(PlayFailureHaptic);
            PlayLightImpactHapticRequest.Subscribe(PlayLightImpactHaptic);
            PlayMediumImpactRequest.Subscribe(PlayMediumImpactHaptic);
            PlayHeavyImpactHapticRequest.Subscribe(PlayHeavyImpactHaptic);
            PlayRigidImpactRequest.Subscribe(PlayRigidImpactHaptic);
            PlaySoftImpactRequest.Subscribe(PlaySoftImpactHaptic);
            StateChangeRequest.Subscribe(ChangeState);

            // ! Get and set the _canPlayHaptic from save system
        }

        private void ChangeState(bool canPlay)
        {
            _canPlayHaptic = canPlay;
        }

        private void PlaySelectionHaptic()
        {

            PlayHaptic(PresetType.Selection);
        }
        private void PlaySuccessHaptic()
        {
            PlayHaptic(PresetType.Success);
        }
        private void PlayWarningHaptic()
        {
            PlayHaptic(PresetType.Warning);
        }
        private void PlayFailureHaptic()
        {
            PlayHaptic(PresetType.Failure);
        }
        private void PlayLightImpactHaptic()
        {
            PlayHaptic(PresetType.LightImpact);
        }
        private void PlayMediumImpactHaptic()
        {
            PlayHaptic(PresetType.MediumImpact);
        }
        private void PlayHeavyImpactHaptic()
        {
            PlayHaptic(PresetType.HeavyImpact);
        }
        private void PlayRigidImpactHaptic()
        {
            PlayHaptic(PresetType.RigidImpact);
        }
        private void PlaySoftImpactHaptic()
        {
            PlayHaptic(PresetType.SoftImpact);
        }
        private void PlayHaptic(PresetType pattern)
        {

            if (!_canPlayHaptic)
            {
                return;
            }

            Source.fallbackPreset = pattern;
            Source.Play();

        }

        void OnDestroy()
        {
            PlaySelectionHapticRequest.UnSubscribe(PlaySelectionHaptic);
            PlaySuccessHapticRequest.UnSubscribe(PlaySuccessHaptic);
            PlayWarningHapticRequest.UnSubscribe(PlayWarningHaptic);
            PlayFailureHapticRequest.UnSubscribe(PlayFailureHaptic);
            PlayLightImpactHapticRequest.UnSubscribe(PlayLightImpactHaptic);
            PlayMediumImpactRequest.UnSubscribe(PlayMediumImpactHaptic);
            PlayHeavyImpactHapticRequest.UnSubscribe(PlayHeavyImpactHaptic);
            PlayRigidImpactRequest.UnSubscribe(PlayRigidImpactHaptic);
            PlaySoftImpactRequest.UnSubscribe(PlaySoftImpactHaptic);
        }
    }
}

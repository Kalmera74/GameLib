using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lofelt.NiceVibrations;
using static Lofelt.NiceVibrations.HapticPatterns;

using UnityEngine;
using GameLib.ScriptableObjectBases.EventDelegates;
using GameLib.ScriptableObjectBases.Saveables;

namespace GameLib.Managers.VibrationManager
{

    public class VibrationManager : MonoBehaviour
    {


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
        [SerializeField] private VibrationManagerSaveableSO VibrationData;
        [SerializeField] private VoidEventDelegateSO SaveRequestDelegate;


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


        }

        private void ChangeState(bool canPlay)
        {
            VibrationData.CanVibrate = canPlay;
            Save();
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

            if (!VibrationData.CanVibrate)
            {
                return;
            }

            HapticPatterns.PlayPreset(pattern);

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
        private void Save()
        {
            SaveRequestDelegate?.FireEvent();
        }
    }
}

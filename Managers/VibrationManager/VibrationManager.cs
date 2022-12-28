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
    /// <summary>
    /// A component that manages the haptic feedback of the game.
    /// </summary>
    public class VibrationManager : MonoBehaviour
    {
        /// <summary>
        /// A scriptable object delegate that allows changing the haptic feedback state (on/off).
        /// </summary>
        [SerializeField] private BooleanEventDelegateSO StateChangeRequest;

        /// <summary>
        /// A scriptable object delegate that allows playing the "selection" haptic pattern.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlaySelectionHapticRequest;

        /// <summary>
        /// A scriptable object delegate that allows playing the "success" haptic pattern.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlaySuccessHapticRequest;

        /// <summary>
        /// A scriptable object delegate that allows playing the "warning" haptic pattern.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlayWarningHapticRequest;

        /// <summary>
        /// A scriptable object delegate that allows playing the "failure" haptic pattern.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlayFailureHapticRequest;

        /// <summary>
        /// A scriptable object delegate that allows playing the "light impact" haptic pattern.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlayLightImpactHapticRequest;

        /// <summary>
        /// A scriptable object delegate that allows playing the "medium impact" haptic pattern.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlayMediumImpactRequest;

        /// <summary>
        /// A scriptable object delegate that allows playing the "heavy impact" haptic pattern.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlayHeavyImpactHapticRequest;

        /// <summary>
        /// A scriptable object delegate that allows playing the "rigid impact" haptic pattern.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlayRigidImpactRequest;

        /// <summary>
        /// A scriptable object delegate that allows playing the "soft impact" haptic pattern.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PlaySoftImpactRequest;

        /// <summary>
        /// A scriptable object that holds the state and settings of the vibration manager.
        /// </summary>
        [SerializeField] private VibrationManagerSaveableSO VibrationData;
        /// <summary>
        /// A scriptable object delegate that allows saving the vibration manager data.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO SaveRequestDelegate;

        /// <summary>
        /// Subscribes to the event delegates when the component awakens.
        /// </summary>
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
        /// <summary>
        /// Sets the state of the CanVibrate (on/off).
        /// </summary>
        /// <param name="isActive">Whether the vibration should be turned on or off.</param>
        private void ChangeState(bool canPlay)
        {
            VibrationData.CanVibrate = canPlay;
            Save();
        }



        /// <summary>
        /// Plays the "selection" haptic pattern.
        /// </summary>
        private void PlaySelectionHaptic()
        {
            PlayHaptic(PresetType.Selection);
        }

        /// <summary>
        /// Plays the "success" haptic pattern.
        /// </summary>
        private void PlaySuccessHaptic()
        {
            PlayHaptic(PresetType.Success);
        }

        /// <summary>
        /// Plays the "warning" haptic pattern.
        /// </summary>
        private void PlayWarningHaptic()
        {
            PlayHaptic(PresetType.Warning);
        }

        /// <summary>
        /// Plays the "failure" haptic pattern.
        /// </summary>
        private void PlayFailureHaptic()
        {
            PlayHaptic(PresetType.Failure);
        }

        /// <summary>
        /// Plays the "light impact" haptic pattern.
        /// </summary>
        private void PlayLightImpactHaptic()
        {
            PlayHaptic(PresetType.LightImpact);
        }

        /// <summary>
        /// Plays the "medium impact" haptic pattern.
        /// </summary>
        private void PlayMediumImpactHaptic()
        {
            PlayHaptic(PresetType.MediumImpact);
        }

        /// <summary>
        /// Plays the "heavy impact" haptic pattern.
        /// </summary>
        private void PlayHeavyImpactHaptic()
        {
            PlayHaptic(PresetType.HeavyImpact);
        }

        /// <summary>
        /// Plays the "rigid impact" haptic pattern.
        /// </summary>
        private void PlayRigidImpactHaptic()
        {
            PlayHaptic(PresetType.RigidImpact);
        }

        /// <summary>
        /// Plays the "soft impact" haptic pattern.
        /// </summary>
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
        /// <summary>
        /// Unsubscribes from the event delegates when the component is destroyed.
        /// </summary>
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
        /// <summary>
        /// Saves the data for the sound manager.
        /// </summary>
        private void Save()
        {
            SaveRequestDelegate?.FireEvent();
        }
    }
}

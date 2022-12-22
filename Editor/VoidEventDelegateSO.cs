using GameLib.ScriptableObjectBases.EventDelegates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameLib.EditorScripts
{

    [CustomEditor(typeof(VoidEventDelegateSO), true)]
    public class VoidEventDelegateEditor : Editor
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private VoidEventDelegateSO _delegate;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private void OnEnable()
        {
            _delegate = (VoidEventDelegateSO)target;
        }
        public override void OnInspectorGUI()
        {

            if (GUILayout.Button("Fire The Event"))
            {
                _delegate?.FireEvent();

            }

            base.OnInspectorGUI();

        }
    }
}

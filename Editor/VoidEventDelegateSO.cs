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
        private VoidEventDelegateSO _delegate;

        private void OnEnable()
        {
            _delegate = target as VoidEventDelegateSO;
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

using GameLib.Managers.SceneManager;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameLib.Editor
{

    [InitializeOnLoad]
    [CustomEditor(typeof(SceneManager<>), true)]
    public class SceneManagerEditor : Editor
    {
        private dynamic _sceneManager;
        private void OnEnable()
        {
            _sceneManager = target;
        }
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Load Levels To Scene Build"))
            {
                _sceneManager.LoadScenesToList();
            }

            base.OnInspectorGUI();

        }
    }
}

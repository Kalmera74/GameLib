using UnityEngine;
using UnityEditor;
using Mobiversite.GameLib.DevLib.Core;

namespace Mobiversite
{
    [CustomEditor(typeof(SceneManager))]
    public class SceneManagerEditor : Editor
    {
        private SceneManager _sceneManager;
        private void OnEnable()
        {
            _sceneManager = target as SceneManager;
        }
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Load Levels"))
            {
                _sceneManager.LoadScenesToList();
            }
          
            base.OnInspectorGUI();

        }
    }
}

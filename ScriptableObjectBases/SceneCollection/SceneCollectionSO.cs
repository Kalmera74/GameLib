using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameLib.Managers.SceneManager;
using UnityEngine;
using Object = UnityEngine.Object;
namespace GameLib.ScriptableObjectBases.SceneCollection
{
    [CreateAssetMenu(menuName = "Mobiversite/Data Containers/SceneCollection", fileName = "Scene_Collection_Container")]
    public class SceneCollectionSO : ScriptableObject
    {
        [SerializeField] protected List<SceneDefinition> SceneList = new List<SceneDefinition>();

        public  void SetList(List<SceneDefinition> list)
        {
            SceneList = new List<SceneDefinition>(list);
        }
        public SceneDefinition GetItemAt(int index)
        {            
            return SceneList[index];
        }
        public  int GetLevelCount()
        {
            return SceneList.Count;
        }

        public  SceneDefinition GetLastItem()
        {
            SceneDefinition item = SceneList.Last();
            return item;
        }
        public  SceneDefinition GetFirstItem()
        {
            SceneDefinition item = SceneList.First();
            return item;
        }

        public  void Clear()
        {
            SceneList?.Clear();
        }
    }
}

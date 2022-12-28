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

        public virtual void SetList(List<SceneDefinition> list)
        {
            SceneList = new List<SceneDefinition>(list);
        }
        public virtual SceneDefinition GetItemAt(int index)
        {
            dynamic item = null;
            if (index < SceneList.Count)
            {
                item = SceneList[index];
            }
            return item;
        }
        public virtual int GetLevelCount()
        {
            return SceneList.Count;
        }

        public virtual SceneDefinition GetLastItem()
        {
            SceneDefinition item = SceneList.Last();
            return item;
        }
        public virtual SceneDefinition GetFirstItem()
        {
            SceneDefinition item = SceneList.First();
            return item;
        }

        public virtual void Clear()
        {
            SceneList?.Clear();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
namespace Mobiversite
{
    public abstract class SceneCollectionSO<T> : ScriptableObject
    {
        [SerializeField] protected List<T> SceneList = new List<T>();

        public virtual void SetList(List<T> list)
        {
            SceneList = new List<T>(list);
        }
        public virtual T GetItemAt(int index)
        {
            dynamic item = null;
            if (index < SceneList.Count)
            {
                item = SceneList[index];
            }
            return (T)item;
        }
        public virtual int GetLevelCount()
        {
            return SceneList.Count;
        }

        public virtual T GetLastItem()
        {
            dynamic item = SceneList.Last();
            return (T)item;
        }
        public virtual T GetFirstItem()
        {
            dynamic item = SceneList.First();
            return (T)item;
        }

        public virtual void Clear()
        {
            SceneList?.Clear();
        }
    }
}

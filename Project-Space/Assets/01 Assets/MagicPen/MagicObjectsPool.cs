using System.Collections.Generic;
using UnityEngine;

namespace MagicPen
{
    public class MagicObjectsPool : MonoBehaviour
    {
        [SerializeField] private List<Pool> pools;

        public GameObject GetObjectFromPool(int index)
        {
            if (pools[index].GetObject(out GameObject gettedObject))
                return gettedObject;
            return null;
        }

        public Sprite GetObjectIconFromPool(int index) => pools[index].icon;

        public int GetPoolSize(int index) => pools[index].ObjectsCount();

        public int GetNumberOfPools() => pools.Count;

        public void ReturnObjectToPool(out int indexUsed, GameObject @object)
        {
            indexUsed = 0;
            for (int i = 0; i < pools.Count; i++)
            {
                if (pools[i].ReturnObject(@object))
                {
                    indexUsed = i;
                    return;
                }
            }
        }

        [System.Serializable]
        public class Pool
        {
            [SerializeField] private List<GameObject> objects;
            public Sprite icon;

            public bool GetObject(out GameObject returningObject)
            {
                returningObject = null;
                for (int i = 0; i < objects.Count; i++)
                {
                    if (!objects[i].activeInHierarchy)
                    {
                        returningObject = objects[i];
                        objects[i].SetActive(true);
                        return true;
                    }
                }
                return false;
            }

            public bool ReturnObject(GameObject @object)
            {
                if (!objects.Contains(@object)) return false;
                @object.transform.position = Vector3.zero;
                @object.SetActive(false);
                return true;
            }

            public int ObjectsCount() => objects.Count;
        }
    }
}
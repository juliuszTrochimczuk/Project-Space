using System.Collections.Generic;
using UnityEngine;

namespace MagicPen
{
    public class MagicObjectsPool : MonoBehaviour
    {
        public List<Pool> pools;

        public bool GetObjectFromPool(int index, Vector3 placement)
        {
            GameObject gettedObject = pools[index].GetObject();
            if (gettedObject == null) return false;
            gettedObject.SetActive(true);
            gettedObject.transform.position = placement;
            return true;
        }

        [System.Serializable]
        public class Pool
        {
            [SerializeField] private List<GameObject> objects;
            public Sprite icon;

            public GameObject GetObject()
            {
                foreach (GameObject @object in objects)
                {
                    if (!@object.activeInHierarchy) return @object;
                }
                return null;
            }

            public void ReturnObject()
            {
                Debug.Log("Returning object");
            }

            public int ObjectsCount() => objects.Count;
        }
    }
}
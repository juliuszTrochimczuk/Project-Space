using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MagicPen
{
    public class MagicPenUI : MonoBehaviour
    {
        [Inject] private MagicObjectsPool pool;

        [SerializeField] private List<MagicObjectUI> objectsUI;

        private void Awake()
        {
            for (int i = 0; i < pool.pools.Count; i++)
            {
                objectsUI[i].icon.sprite = pool.pools[i].icon;
                objectsUI[i].counter.text = pool.pools[i].ObjectsCount().ToString();
                if (i == objectsUI.Count)
                {
                    Debug.LogError("There are to many pools, not enough UI places");
                    return;
                }
            }
        }

        public void ControllCounter(int index) => objectsUI[index].counter.text = (int.Parse(objectsUI[index].counter.text) - 1).ToString();

        public void ActualizeBackground(int activeIndex)
        {
            for (int i = 0; i < objectsUI.Count; i++)
            {
                if (i == activeIndex) objectsUI[i].ControllBackgroundState(true);
                else objectsUI[i].ControllBackgroundState(false);
            }
        }

        [System.Serializable]
        public class MagicObjectUI
        {
            public Image icon;
            public TextMeshProUGUI counter;
            [SerializeField] private Image background;
            [SerializeField] private Color backgroundInactive;
            [SerializeField] private Color backgroundActive;

            public void ControllBackgroundState(bool isActive)
            {
                if (isActive) background.color = backgroundActive;
                else background.color = backgroundInactive;
            }
        }
    }
}
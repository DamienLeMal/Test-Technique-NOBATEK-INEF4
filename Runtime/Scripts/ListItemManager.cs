
using UnityEngine;
namespace DamienLeMal.TearOffMenu {
    public class ListItemManager : MonoBehaviour
    {
        public static ListItemManager Current;
        public Transform InstantiatedItemParent;
        public GameObject DragPositionHelper;
        
        void Start()
        {
            Current = this;
            HidePositionHelper();
        }

        public void ShowItem (GameObject item = null) {
            foreach(Transform child in InstantiatedItemParent)
            {
                Destroy(child.gameObject);
            }
            if (item is null) return;
            Instantiate(item, InstantiatedItemParent);
        }

        public int GetDragPosition (float yPos) {
            Transform listParent = transform.GetChild(1);
            for (int i = 0; i < listParent.childCount; i++) {
                if (yPos > listParent.GetChild(i).position.y) {
                    return i;
                }
            }
            return listParent.childCount;
        }

        // The Position Helper is there to show where the dragged object would end up if dropped off

        public void ShowPositionHelper (int index) {
            DragPositionHelper.SetActive(true);
            DragPositionHelper.transform.SetSiblingIndex(index);
        }

        public void HidePositionHelper () {
            DragPositionHelper.SetActive(false);
        }
    }
}
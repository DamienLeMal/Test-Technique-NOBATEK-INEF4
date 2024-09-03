
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Linq;

namespace DamienLeMal.TearOffMenu {
    public class AddressableListManager : MonoBehaviour {
        // The label is used to identify the addressable folder
        public AssetLabelReference AssetLabelReference;
        public GameObject ListItemPrefab;
        public Transform ListTransform;

        void Start()
        {
            LoadAddressables();
        }

        public void LoadAddressables () {
            AsyncOperationHandle opHandle = Addressables.LoadAssetsAsync<ItemSO>(AssetLabelReference, (item) => { 
                // Will be called for each item with the label reference (under the labelled folder)

                // Stop the loading if the item is already in the list
                foreach (Transform child in ListTransform)
                    if (child.GetComponent<ListItemUI>()?.itemSO == item) return;

                // Create a new item in the list    
                ListItemUI i = Instantiate(ListItemPrefab, ListTransform).GetComponent<ListItemUI>();
                i.Populate(item);
            });
            //Addressables.ReleaseInstance(opHandle);
        }

        public void SortByValue () {

            // Get a list of all the children
            List<ListItemUI> list = new List<ListItemUI>();
            foreach (Transform child in ListTransform) {
                ListItemUI c = child.GetComponent<ListItemUI>();
                if (c != null) list.Add(c);
            }
            // Sort the list using the sorting value
            list = list.OrderBy(x => x.itemSO.SortingValue).ToList();

            for (int i = 0; i < list.Count; i++) {
                list[i].transform.SetSiblingIndex(i);
            }
        }
    }
}
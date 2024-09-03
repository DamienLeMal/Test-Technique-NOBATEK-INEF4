
using UnityEngine;

namespace DamienLeMal.TearOffMenu {

    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/Item", order = 0)]
    public class ItemSO : ScriptableObject {

        // User Specified

        public GameObject prefab;
        public string Name = "Cowboy Hat";
        public Sprite Icon;

        // Automatically Generated

        [HideInInspector]
        public int SortingValue;
    }
}
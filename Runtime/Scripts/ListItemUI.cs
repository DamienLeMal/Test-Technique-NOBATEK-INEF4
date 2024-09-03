
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.EventSystems;
namespace DamienLeMal.TearOffMenu {
    public class ListItemUI : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {
        public TextMeshProUGUI NameText;
        public TextMeshProUGUI ValueText;
        public Image Icon;
        public RectTransform DragHandle;
        
        [HideInInspector]
        public ItemSO itemSO;
        
        private RectTransform _rectTransform;
        private bool _canDrag;
        private int _dragHandleIndex;
        private Vector3 _dragOffset;

        // Initialize the gameobject with the data from the scriptable object
        public void Populate (ItemSO item) {
            _rectTransform = GetComponent<RectTransform>();
            itemSO = item;
            NameText.text = item.Name;
            item.SortingValue = item.prefab.transform.hierarchyCount;
            ValueText.text = "Pivots : " + item.SortingValue.ToString();
            Icon.sprite = item.Icon;
            //PreviewImage(item);
        }

        /* Does not work outside editor

        // Sets the icon for the scriptable object using the asset preview of the prefab
        async void PreviewImage (ItemSO item) {
            Texture2D tex = null;

            // Wait for the preview to load
            while (tex == null) {
                tex = AssetPreview.GetAssetPreview(item.prefab);
                await Task.Yield();
            }

            // Convert the texture2D to a sprite to be displayed
            item.preview = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
            Icon.sprite = item.preview;
        }

        */

        // X button on the UI
        public void RemoveFromList () {
            ListItemManager.Current.ShowItem(); // Also remove the item shown in case we are removing the one that is on display
            Destroy(gameObject);
        }

        // Play button on the UI, show the item's prefab on screen
        public void ShowModel () {
            ListItemManager.Current.ShowItem(itemSO.prefab);
        }

        // Bars button on the UI, start the dragging of the object
        public void AllowDrag () {
            _canDrag = true;
        }
        // Avoid being able to drag from anywhere after a simple click on the button
        public void DisallowDrag () {
            _canDrag = false;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_canDrag is false) return;
            transform.SetParent(ListItemManager.Current.transform.parent);

            // Get offset for the mouse drag
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
            {
                _dragOffset = _rectTransform.position - globalMousePos;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_canDrag is false) return;

            // Move the object with the mouse
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
            {
                _rectTransform.position = globalMousePos + _dragOffset;
            }

            // Display would be position if dropped off
            _dragHandleIndex = ListItemManager.Current.GetDragPosition(transform.position.y);
            ListItemManager.Current.ShowPositionHelper(_dragHandleIndex);

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_canDrag is false) return;
            transform.SetParent(ListItemManager.Current.transform.GetChild(1));
            transform.SetSiblingIndex(_dragHandleIndex);
            ListItemManager.Current.HidePositionHelper();
            _canDrag = false;
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Canvas canvas; // Reference to the canvas for proper scaling
    private Transform inventoryPanel; // Reference to the inventory panel
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>(); // Get the parent canvas

        GameObject inventoryPanelObject = GameObject.Find("Spell Inventory Panel"); // Replace "InventoryPanel" with the correct name
        if (inventoryPanelObject != null)
        {
            inventoryPanel = inventoryPanelObject.transform;
        }
        else
        {
            Debug.LogError("InventoryPanel not found! Make sure there is a GameObject named 'InventoryPanel' in the scene.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; // Save the original parent
        canvasGroup.blocksRaycasts = false; // Allow the object to pass through raycasts while dragging
        transform.SetParent(canvas.transform); // Move to the root of the canvas to follow the mouse properly
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update position based on the mouse movement, adjusted for canvas scale
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Enable raycasts again

        // Check if dropped on a valid slot
        if (eventData.pointerEnter != null && eventData.pointerEnter.TryGetComponent(out SlotDropHandler slotHandler))
        {
            // Snap to the slot
            transform.SetParent(slotHandler.transform);

            // Center the spell in the slot
            RectTransform spellRect = GetComponent<RectTransform>();
            spellRect.anchorMin = new Vector2(0.5f, 0.5f);
            spellRect.anchorMax = new Vector2(0.5f, 0.5f);
            spellRect.pivot = new Vector2(0.5f, 0.5f);
            spellRect.anchoredPosition = Vector2.zero;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel.GetComponent<RectTransform>(), Input.mousePosition, null)){
                // Move the spell back to the inventory
                transform.SetParent(inventoryPanel);
            }
        else
        {
            // Snap back to original position
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
public class SpellUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject tooltipPanel; // Accepts the TooltipPanel GameObject
    private SpellTooltipUI tooltip;
    private RectTransform rectTransform;
    [SerializeField] private Image spellIconImage; // Reference to the Image component

    public Spell assignedSpell; // The spell associated with this UI element
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        tooltip = FindObjectOfType<SpellTooltipUI>(true); // 'true' ensures it finds inactive components

        if (tooltip != null)
        {
            tooltipPanel = tooltip.gameObject; // Get the TooltipPanel GameObject
        }
        else
        {
            Debug.LogError("Tooltip component not found! Make sure a GameObject with the Tooltip component exists in the scene.");
        }
    }
    public void SetSpell(Spell spell)
    {
        assignedSpell = spell;

        // Set the icon image if the spell has one
        if (spell != null && spell.Icon != null)
        {
            spellIconImage.sprite = spell.Icon; // Assign the spell's icon to the UI image
            spellIconImage.enabled = true;     // Ensure the image is visible
        }
        else
        {
            spellIconImage.sprite = null;      // Clear the image
            spellIconImage.enabled = false;    // Hide the image if there's no spell
        }
    }

    public Spell GetAssignedSpell()
    {
        return assignedSpell;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (assignedSpell != null && tooltip != null)
        {
            Vector2 tooltipPosition = rectTransform.position + new Vector3(100, -50); // Offset for better visibility
            tooltip.ShowTooltip(assignedSpell, tooltipPosition);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip?.HideTooltip(); // Safely hide tooltip only if it's not null
    }
}

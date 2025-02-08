using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class MainMenuButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private Vector2 originalButtonSize;
    private RectTransform rectTransform;
    public float hoverScaleFactor = 1.1f;
    public float clickScaleFactor = 0.9f;
    public float animationDuration = 0.1f;

    private Coroutine currentCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ScaleButton(originalButtonSize * hoverScaleFactor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ScaleButton(originalButtonSize));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        StartCoroutine(ClickEffect());
    }

    private IEnumerator ScaleButton(Vector2 targetSize)
    {
        Vector2 currentSize = rectTransform.sizeDelta;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            rectTransform.sizeDelta = Vector2.Lerp(currentSize, targetSize, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.sizeDelta = targetSize;
    }

    private IEnumerator ClickEffect()
    {
        yield return ScaleButton(originalButtonSize * clickScaleFactor);
        yield return ScaleButton(originalButtonSize * hoverScaleFactor);
    }
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalButtonSize = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
    }
}

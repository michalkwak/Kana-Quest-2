using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonColor : MonoBehaviour
{
    private Image buttonImage;
    private Color normalColor;
    private Color hoverColor;

    private float transitionSpeed = 5f;

    void Start()
    {

        buttonImage = GetComponent<Image>();

        // Store the normal color and create a new color with adjusted alpha for hover effect
        normalColor = buttonImage.color;
        hoverColor = new Color(normalColor.r, normalColor.g, normalColor.b, 0.7f); // You can adjust the alpha value
    }

    public void OnPointerEnter()
    {
        // Start a smooth transition to hover color
        StopAllCoroutines(); // Stop any ongoing transitions
        StartCoroutine(TransitionColor(normalColor, hoverColor));
    }

    public void OnPointerExit()
    {
        // Start a smooth transition back to normal color
        StopAllCoroutines(); // Stop any ongoing transitions
        StartCoroutine(TransitionColor(hoverColor, normalColor));
    }

    IEnumerator TransitionColor(Color startColor, Color targetColor)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * transitionSpeed;
            buttonImage.color = Color.Lerp(startColor, targetColor, elapsedTime);
            yield return null;
        }

        // Ensure the final color is exactly the target color
        buttonImage.color = targetColor;
    }
}

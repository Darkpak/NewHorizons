using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image panel;
    public float fadeOutSpeed;
    private bool fadeOut =false;

    private void Update()
    {
        if (fadeOut)
        {
            Color cutentColor = panel.color;
            cutentColor.a += (fadeOutSpeed*Time.deltaTime)/2;
            panel.color = cutentColor;
        }
    }

    private void OnMouseDown()
    {
        fadeOut = true;
    }
}

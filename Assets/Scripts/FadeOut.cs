using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image panel;
    public float fadeOutSpeed;
    public float devide = 2;
    private bool fadeOut = false;

    private void Update()
    {
        if (fadeOut)
        {
            Transition();
            fadeOut = false;
        }
        
    }

    public void Transition()
    {
        StartCoroutine(TransitionIEnumerator());
    }

    private IEnumerator TransitionIEnumerator()
    {
        while (panel.color.a < 1)
        {
            Color cutentColor = panel.color;
            cutentColor.a += (fadeOutSpeed * Time.deltaTime) / devide;
            panel.color = cutentColor;
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnMouseDown()
    {
        print(fadeOut);
        fadeOut = true;
        print(fadeOut);
    }
}

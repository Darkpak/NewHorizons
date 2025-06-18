using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Tooltip("Name of the scene you want to load")]
    [SerializeField] string sceneToLoad;

    [Tooltip("Time before loading the next scene.")]
    [SerializeField] float loadingTime;

    public enum ButtonType { Quit, LoadScene}
    [Tooltip("Button's purpose")] public ButtonType buttonType;

    private IEnumerator OnMouseDown()
    {
        switch (buttonType)
        {
            case ButtonType.Quit:
                Debug.Log("Quit Game");
                Application.Quit();
                break;
            case ButtonType.LoadScene:
                Debug.Log("Loading Scene");
                yield return new WaitForSeconds(loadingTime);
                SceneManager.LoadScene(sceneToLoad);
                break;
            default:
                break;
        }

     
    }
}

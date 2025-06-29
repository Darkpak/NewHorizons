using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OnClicked : MonoBehaviour
{
    [SerializeField] float delay;
    public UnityEvent onClick;

    private IEnumerator OnMouseDown()
    {
        yield return new WaitForSeconds(delay); 
        onClick.Invoke();
    }
}

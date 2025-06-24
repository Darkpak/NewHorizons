using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TorchBehaviour : MonoBehaviour //, IPointerDownHandler
{
    public TorchManager TorchManager;
    public int TorchIndex;

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    OnInteract();
    //}

    private void OnMouseDown()
    {
        //OnInteract();
    }

    private void OnMouseUpAsButton()
    {
        OnInteract();
    }

    //private void Update()
    //{
    //    if(Input.to)
    //}

    private void OnInteract()
    {
        TorchManager.OnClicked(TorchIndex);
    }
}

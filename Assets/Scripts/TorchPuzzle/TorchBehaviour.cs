using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TorchBehaviour : MonoBehaviour //, IPointerDownHandler
{
    public TorchManager TorchManager;
    public int TorchIndex;
    public PlaySoundEffect sound_effect;

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    OnInteract();
    //}

    private void OnMouseDown()
    {
        TorchManager.OnClicked(TorchIndex);
        sound_effect.PlaySound();
    }

    //private void OnMouseUpAsButton()
    //{
    //    OnInteract();
    //}

    //private void Update()
    //{
    //    if (Input.to)
    //}

    //private void OnInteract()
    //{
    //    TorchManager.OnClicked(TorchIndex);
    //}
}

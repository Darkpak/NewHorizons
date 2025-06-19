using UnityEngine;

public class TorchBehaviour : MonoBehaviour
{
    public TorchManager TorchManager;
    public int TorchIndex;
    private void OnMouseDown()
    {
        TorchManager.OnClicked(TorchIndex);
    }
}

using DG.Tweening.Core.Easing;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TorchManager : MonoBehaviour
{
    bool[] torchState = new bool[6];
    [SerializeField] bool areAllFlamed = false;
    public UnityEvent onFlamed;
    public UnityEvent onCameraChanged;
    public UnityEvent onDoorPlaced;
    [SerializeField] float delay;
    (int, int)[] connections = new (int, int)[] {
        (0,3), (1,5), (2,4), (3,2), (4,1), (5,0)
    };

    private void Start()
    {
        InitializePuzzle();
    }
    void InitializePuzzle()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var torch = transform.GetChild(i).GetComponent<TorchBehaviour>();
            torch.TorchManager = this;
            torch.TorchIndex = i;
        }
    }

    public void OnClicked(int torchIndex)
    { 
        torchState[torchIndex] = !torchState[torchIndex];
        OnEnableDisable(torchIndex);

        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].Item1 == torchIndex)
            {
                int connectedIndex = connections[i].Item2;
                torchState[connectedIndex] = !torchState[connectedIndex];

                OnEnableDisable(connectedIndex);
            }
        }
       StartCoroutine(CheckIfSolved());
    }

    void OnEnableDisable(int index)
    {
        if (torchState[index])
        {
           transform.GetChild(index).GetChild(0).gameObject.SetActive(true);
        }
        else 
        {
            transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
        }
    }

    private IEnumerator CheckIfSolved()
    {
        foreach (bool state in torchState)
        {
            if (!state)
            {
                areAllFlamed = false;
                yield break;
            }
        }

        areAllFlamed = true;
        yield return new WaitForSeconds(delay);
        onFlamed.Invoke();
        yield return new WaitForSeconds(0.5f);
        onCameraChanged.Invoke();
        yield return new WaitForSeconds(0.5f);
        onDoorPlaced.Invoke();
        
    }
}

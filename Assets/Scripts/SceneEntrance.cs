using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    public string lastExitName;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


        if (PlayerPrefs.GetString("LastExitName") == lastExitName)
        {

            player.transform.position = transform.position;
            player.transform.eulerAngles = transform.eulerAngles;
        }
    }
}

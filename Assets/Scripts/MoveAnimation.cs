using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float waveAmplitude;
    [SerializeField] float waveFrequency;

    // Update is called once per frame
    void Update()
    {
        SinWay();
    }

    void SinWay()
    { 
        float offset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;


        Vector3 movement = new Vector3(0, offset, 0f);
        transform.Translate(movement * Time.deltaTime * speed);
    }
}

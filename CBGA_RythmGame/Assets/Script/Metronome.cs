using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    AudioSource audio;
    float time;
    float waitTime;

    int bpm = 60;
    int tempoUp;
    int tempoDown;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        tempoUp = 4;
        tempoDown = 4;
        waitTime = (60 / bpm) * (tempoUp/tempoDown);
        //Debug.Log("#Metronome - waitTime : " + waitTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time > waitTime)
        {
            audio.Play();
            //Debug.Log("#Metronome - time : " + time);
            time -= waitTime;
            RotateMetronome();
        }
        else
        {
            time += Time.deltaTime;
        }
        
    }

    void RotateMetronome()
    {
        //Debug.Log("#Metronome - transform.localRotation = " + transform.localRotation);
        transform.Rotate(new Vector3(0,0,45));
    }
}

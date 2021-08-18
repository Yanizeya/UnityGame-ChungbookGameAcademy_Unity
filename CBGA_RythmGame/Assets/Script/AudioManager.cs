using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audio;
    public AudioClip[] audioFile;
    string[] validKeyArr;
    bool audioPlayState;
    
    // Start is called before the first frame update
    void Start()
    {
        validKeyArr = InputManager.validKeyArr;
        audio = GetComponent<AudioSource>();
        audioPlayState = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayAudio(bool audioTrigger, string keyString)
    {
        if (audioTrigger == true)
        {
            audio.volume = 1;
            if (audioPlayState == false)
            {
                audio.Play();
                audioPlayState = true;
            }
        }
        else
        {
            audioPlayState = false;
            if (audio.volume > 0)
            {
                audio.volume -= Time.deltaTime * 2;
            }
            else
            {
                audio.Stop();
            }
        }
    }
}

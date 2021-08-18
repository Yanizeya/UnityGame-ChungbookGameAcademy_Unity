using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //string[] PianoScales = { "do", "re", "mi", "fa", "sol" };
    AudioSource audio;
    public AudioClip[] audioClip;
    string[] validKeyArr;
    bool audioPlayState;
    bool audioing = false;
    string keyString;
    string[][] keyboardSpell = KeyboardSponer.keyboardSpell;
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
        if(audioPlayState == true)
        {
            
            if (audioing == false)
            {
                for (int i = 0; i < 5; i++)
                    if (keyboardSpell[0][i] == keyString)
                    {
                        audio.clip = audioClip[i];
                    }
                audio.volume = 1;
                audio.Play();
                audioing = true;
            }
        }
        else
        {
            audioing = false;
            if (audio.volume > 0)
                audio.volume -= Time.deltaTime * 2;
            else
                audio.Stop();
        }
    }

    public void PlayAudio(string keyString)
    {
        this.keyString = keyString;
        audioPlayState = true;
    }
    public void StopAudio(string keyString)
    {
        audioPlayState = false;
    }
}

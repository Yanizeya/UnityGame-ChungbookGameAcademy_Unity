using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class PianoScaleArr
    {
        public AudioClip[] pianoScale;
    }
    public PianoScaleArr[] audioClipArr;

    AudioSource audio;
    string[] validKeyArr;
    bool audioPlayState;
    bool audioing = false;
    string keyString;
    string[][] abledKeyboardSpell = KeyboardSponer.abledKeyboardSpell; //{ new string[] { "a", "s", "d", "f", "j", "k", "l", ";" } , new string[] { "w", "e", "t", "i", "o" } };

    int pianoCCode = 4-1; //기본은 c4. 배열은 0부터 시작하므로 -1
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
                for (int i = 0; i < abledKeyboardSpell.Length; i++)
                    for(int j = 0; j< abledKeyboardSpell[i].Length; j++)
                        if (abledKeyboardSpell[i][j] == keyString)
                        {
                            Debug.Log("#AudioManager - j : " + j);
                            audio.clip = audioClipArr[pianoCCode].pianoScale[i*8+j];
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

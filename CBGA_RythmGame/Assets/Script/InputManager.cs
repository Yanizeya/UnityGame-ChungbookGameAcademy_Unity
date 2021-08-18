using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static string keyString;
    public static string[] validKeyArr = new string[] { "a", "s", "d", "f", "j", "k", "l", ";" };
    public static string[] scoreZoneArr = new string[] { "Perfect", "Nice", "Good", "Bad" };

    static AudioManager audioManager;
    EffectManager effectManager;

    bool holdingDown = false;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            holdingDown = true;
            if (Input.anyKeyDown)
            {
                keyString = Input.inputString;

                for (int i = 0; i < validKeyArr.Length; i++)
                    if (keyString == validKeyArr[i])
                    {
                        GameObject curNote = GameObject.FindGameObjectWithTag("curNote");
                        Note_TouchSenser curNoteTouchSenser = curNote.transform.Find("TouchSenser").GetComponent<Note_TouchSenser>();
                        curNoteTouchSenser.TouchSenserOn(keyString);
                        string scoreZone = curNoteTouchSenser.getScoreZone();
                        PlayEffect(scoreZone);
                        audioManager.PlayAudio(keyString);
                        //Debug.Log("#InputManager - keyString : " + keyString);
                    }
            }
        }
        //Input.anyKeyDown을 구현하기 위한 코드
        if (!Input.anyKey && holdingDown)
        {
            Debug.Log("#InputManager - anyKeyDown");
            holdingDown = false;
            audioManager.StopAudio(keyString);
        }


        
    }
    public void PlayEffect(string scoreZone)
    {
        effectManager = GameObject.Find(keyString).GetComponent<EffectManager>();
        effectManager.Effect(scoreZone);
    }
}

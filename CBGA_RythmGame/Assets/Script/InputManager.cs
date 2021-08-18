using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static string keyString;
    public static string[] validKeyArr = new string[] { "a", "s", "d", "f", "j", "k", "l", ";" };
    public static string[] scoreZoneArr = new string[] { "Perfect", "Nice", "Good", "Bad" };

    static AudioManager audioManager;

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
            audioManager.PlayAudio(true, keyString);
            keyString = Input.inputString;

            for (int i = 0; i < validKeyArr.Length; i++)
                if (keyString == validKeyArr[i])
                {

                    GameObject curNote = GameObject.FindGameObjectWithTag("curNote");
                    curNote.transform.Find("TouchSenser").GetComponent<Note_TouchSenser>().TouchSenserOn(keyString);
                    Debug.Log("#InputManager - keyString : " + keyString);
                    //Debug.Log("#InputManager - touchSenser trigger : " + curNote.transform.Find("TouchSenser").GetComponent<Note_TouchSenser>().touchSenserTrigger);

                }
        }
        else
        {
            audioManager.PlayAudio(false, keyString);
        }
    }

}

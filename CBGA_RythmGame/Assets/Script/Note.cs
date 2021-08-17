using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    static int allNoteNum = 0;
    public int thisNoteNum = 0;
    public static int currentNoteNum = 0;

    public static string keyString;
    public static string[] validKeyArr = new string[] { "a", "s", "d", "f", "j", "k", "l", ";" };
    public static string[] scoreZoneArr = new string[] { "Perfect", "Nice", "Good", "Bad" };

    EffectManager effectManager;
    void Start()
    {
        thisNoteNum = allNoteNum;
        allNoteNum++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            keyString = Input.inputString;
            for (int i = 0; i < validKeyArr.Length; i++)
                if (keyString == validKeyArr[i])
                {
                    if (thisNoteNum == Note.currentNoteNum)
                    {
                        transform.Find("TouchSenser").GetComponent<BoxCollider>().enabled = true;
                        //Debug.Log("this num : " + thisNoteNum + "cur Num : " + Note.currentNoteNum + "keystring : " + keyString);
                    }
                }
        }
    }

    public void HitButton(string scoreZone)
    {
        Note.currentNoteNum++;
        effectManager = GameObject.Find(keyString).GetComponent<EffectManager>();
        effectManager.effect(scoreZone);
        //Debug.Log(keyString);
        keyString = null;
        Destroy(transform.parent.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_TouchSenser : MonoBehaviour
{
    string[] scoreZoneArr = InputManager.scoreZoneArr;
    Note note;
    EffectManager effectManager;
    string keyString;

    public bool touchSenserTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        note = GetComponentInParent<Note>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
{
        for (int i = 0; i < scoreZoneArr.Length; i++)
        {
            if (other.name == scoreZoneArr[i])
            {
            //Debug.Log("#Note_TouchSenser - destroyNote");
            PlayEffect(scoreZoneArr[i]);
            note.DestroyNote();
            }
        }
    }

    public void TouchSenserOn(string keyString)
    {
        this.keyString = keyString;
        GetComponent<BoxCollider>().enabled = true;
    }

    void PlayEffect(string scoreZone)
    {
        //Debug.Log("#Note_TouchSenser - keyString : " + keyString);
        effectManager = GameObject.Find(keyString).GetComponent<EffectManager>();
        effectManager.Effect(scoreZone);
    }
}

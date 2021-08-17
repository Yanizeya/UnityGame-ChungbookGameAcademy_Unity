using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_TouchSenser : MonoBehaviour
{
    string[] scoreZoneArr = Note.scoreZoneArr;
    string keyString = Note.keyString;
    Note note;

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
        for(int i = 0; i<scoreZoneArr.Length; i++)
        {
            if(other.name == scoreZoneArr[i])
            {
                note.HitButton(scoreZoneArr[i]);
            }
        }
    }

}

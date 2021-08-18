using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_TouchSenser : MonoBehaviour
{
    string[] scoreZoneArr = InputManager.scoreZoneArr;
    Note note;
    InputManager inputManager;
    string keyString;
    string scoreZone;
    public bool touchSenserTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        note = GetComponentInParent<Note>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
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
                setScoreZone(scoreZoneArr[i]);
                  note.DestroyNote();
            }
        }
    }

    public void TouchSenserOn(string keyString)
    {
        this.keyString = keyString;
        GetComponent<BoxCollider>().enabled = true;
    }

    public void setScoreZone(string scoreZone)
    {
        this.scoreZone = scoreZone;
    }
    public string getScoreZone()
    {
        return this.scoreZone;
    }
}

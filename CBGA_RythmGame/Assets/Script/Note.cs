using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{

    Rigidbody this_Rigid;
    bool endZone = true;
    Vector2 noteSize;

    static int allNoteNum = 0;
    public int thisNoteNum = 0;
    public static int currentNoteNum = 0;
    GameObject touchSenser;
    Rigidbody touchSenser_Rigid;
    void Start()
    {
        this_Rigid = GetComponent<Rigidbody>();
        noteSize = transform.Find("Image").GetComponent<RectTransform>().offsetMax;
        touchSenser = transform.Find("TouchSenser").gameObject;
        touchSenser_Rigid = touchSenser.GetComponent<Rigidbody>();
        thisNoteNum = allNoteNum;
        allNoteNum++;
    }

    // Update is called once per frame
    void Update()
    {
        if (endZone == true)
            this_Rigid.velocity = Vector3.down * 100;
        else
        {
            this_Rigid.velocity = Vector3.zero;
            touchSenser_Rigid.isKinematic = false;
            touchSenser_Rigid.velocity = Vector3.down * 100;
            if (noteSize.y > -102)
            {
                noteSize.y -= Time.deltaTime * 170;
                transform.Find("Image").GetComponent<RectTransform>().offsetMax = new Vector2(noteSize.x, noteSize.y);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "EndZone")
        {
            endZone = false;
        }
    }

}

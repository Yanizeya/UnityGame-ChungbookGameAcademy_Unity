using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Move : MonoBehaviour
{
    public bool endZoneTrigger = true;
    Rigidbody noteRigid;
    Vector2 noteSize;
    GameObject touchSenser;
    Rigidbody touchSenserRigid;
     
    // Start is called before the first frame update
    void Start()
    {
        noteRigid = GetComponent<Rigidbody>();
        noteSize = transform.Find("Image").GetComponent<RectTransform>().offsetMax;
        touchSenser = transform.Find("TouchSenser").gameObject;
        touchSenserRigid = touchSenser.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (endZoneTrigger == true)
            noteRigid.velocity = Vector3.down * 100;
        else
        {
            noteRigid.velocity = Vector3.zero;
            touchSenserRigid.isKinematic = false;
            touchSenserRigid.velocity = Vector3.down * 100;
            if (noteSize.y > -102)
            {

                noteSize.y -= Time.deltaTime * 170;
                transform.Find("Image").GetComponent<RectTransform>().offsetMax = new Vector2(noteSize.x, noteSize.y);
            }
            else
            {
                Note.currentNoteNum++;
                Destroy(gameObject);
            }
        }
    }
}

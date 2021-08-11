using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{

    Rigidbody this_rigid;
    bool endZone = true;
    
    Vector2 noteSize;

    void Start()
    {
        this_rigid = GetComponent<Rigidbody>();
        noteSize = transform.Find("Image").GetComponent<RectTransform>().offsetMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (endZone == true)
            this_rigid.velocity = Vector3.down * 100;
        else
        {
            this_rigid.velocity = Vector3.zero;
            if (noteSize.y > -102)
            {
                noteSize.y -= Time.deltaTime * 100;
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

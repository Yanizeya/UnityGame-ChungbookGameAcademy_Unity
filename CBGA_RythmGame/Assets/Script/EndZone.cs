using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "note" || other.gameObject.tag == "curNote")
        {
            Debug.Log("#EndZone - touch endZone");
            other.GetComponent<Note_Move>().endZoneTrigger = false;
        }
    }
}

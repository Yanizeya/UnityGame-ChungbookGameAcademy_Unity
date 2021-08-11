using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTouchSenser : MonoBehaviour
{

    string[] scoreZoneName = new string[] { "Perfect", "Nice", "Good", "Bad" };
    string[] validKey = new string[] { "a", "s", "d", "f", "j", "k", "l", ";" };

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        string keyNum = Input.inputString;
        for (int i = 0; i < validKey.Length; i++)
            if (keyNum == validKey[i])
            {
                GetComponent<BoxCollider>().enabled = true;
                Debug.Log(keyNum);
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i<scoreZoneName.Length; i++)
        {
            if(other.name == scoreZoneName[i])
            {
                Debug.Log(scoreZoneName[i]);
                Destroy(transform.parent.gameObject);
            }
        }
                

    }

}

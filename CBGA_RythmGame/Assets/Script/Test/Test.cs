using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Nice = " + GameObject.Find("Nice").GetComponent<RectTransform>().position);
        Debug.Log("Good = " + GameObject.Find("Good").GetComponent<RectTransform>().position);
        Debug.Log("Bad = " + GameObject.Find("Bad").GetComponent<RectTransform>().position);
    }
}

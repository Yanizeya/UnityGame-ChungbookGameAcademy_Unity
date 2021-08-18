using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    static int allNoteNum = 0;
    public int thisNoteNum = 0;
    public static int currentNoteNum = 0;

    
    void Start()
    {
        thisNoteNum = allNoteNum;
        allNoteNum++;
    }

    // Update is called once per frame
    void Update()
    {
        if (thisNoteNum == currentNoteNum)
            gameObject.tag = "curNote";
    }
    public void DestroyNote()
    {
        Note.currentNoteNum++;
        Destroy(gameObject);
    }

}

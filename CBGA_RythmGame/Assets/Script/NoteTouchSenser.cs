using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTouchSenser : MonoBehaviour
{

    string[] scoreZoneName = new string[] { "Perfect", "Nice", "Good", "Bad" };
    public static string[] validKey = new string[] { "a", "s", "d", "f", "j", "k", "l", ";" };
    public static string keyString;
    int thisNoteNum;
    float wait = 0;
    bool triger_currentNumPlus = false;
    EffectManager effectManager;
    // Start is called before the first frame update
    void Start()
    {
        thisNoteNum = GetComponentInParent<Note>().thisNoteNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            keyString = Input.inputString;
            //Debug.Log(keyString);
            for (int i = 0; i < validKey.Length; i++)
                if (keyString == validKey[i])
                {
                    if (thisNoteNum == Note.currentNoteNum)
                    {
                            GetComponent<BoxCollider>().enabled = true;                            triger_currentNumPlus = true;
                        //Debug.Log("this num : " + thisNoteNum + "cur Num : " + Note.currentNoteNum + "keystring : " + keyString);
                            
                    }
                }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i<scoreZoneName.Length; i++)
        {
            if(other.name == scoreZoneName[i])
            {
                Debug.Log(scoreZoneName[i]);
                Note.currentNoteNum++;
                //scoreZone�� ���� ��� keyString�� ���� �ϴ� Ű���带 ã��, �� ���� ����Ʈ�Լ��� keyString �Ű������� �Ѱ���
                effectManager = GameObject.Find(keyString).transform.Find("Effect").GetComponent<EffectManager>();
                Debug.Log("effectOn : " + Time.time);
                effectManager.noteHitEffect();
                //keyString ���� �ʱ�ȭ����
                keyString = null;
                Destroy(transform.parent.gameObject);
            }
        }
                

    }

}

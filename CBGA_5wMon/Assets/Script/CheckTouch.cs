using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckTouch : MonoBehaviour
{
    //MeshRenderer mr;
    public string MusicScale;
    private AudioSource audiosource;   //다음팟 플레이어
    public AudioClip[] audioclip = new AudioClip[6]; //mp4파일, 위의 AudioSource에 넣어서 플레이

    // Start is called before the first frame update
    void Start()
    {
        // mr = GetComponent<MeshRenderer>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Note")
        {
            if (MusicScale == "Do")
            {
                //Debug.Log("Do");
                if (Input.GetKey(KeyCode.A))
                {
                    Debug.Log("hit a!");
                    Destroy(other.gameObject);
                    audiosource.PlayOneShot(audioclip[0]);
                }
            }
            if (MusicScale == "Re")
            {
               // Debug.Log("Re");
                if (Input.GetKey(KeyCode.S))
                {
                    Debug.Log("hit a!");
                    Destroy(other.gameObject);
                    audiosource.PlayOneShot(audioclip[1]);
                }
            }
            if (MusicScale == "Mi")
            {
                //Debug.Log("Mi");
                if (Input.GetKey(KeyCode.D))
                {
                    Debug.Log("hit a!");
                    Destroy(other.gameObject);
                    audiosource.PlayOneShot(audioclip[2]);
                }
            }
            if (MusicScale == "Fa")
            {
                //Debug.Log("Fa");
                if (Input.GetKey(KeyCode.F))
                {
                    Debug.Log("hit a!");
                    Destroy(other.gameObject);
                    audiosource.PlayOneShot(audioclip[3]);
                }
            }
            if (MusicScale == "Sol")
            {
               // Debug.Log("Sol");
                if (Input.GetKey(KeyCode.G))
                {
                    Debug.Log("hit a!");
                    Destroy(other.gameObject);
                    audiosource.PlayOneShot(audioclip[4]);
                }
            }
            if (MusicScale == "La")
            {
                //Debug.Log("La");
                if (Input.GetKey(KeyCode.H))
                {
                    Debug.Log("hit a!");
                    Destroy(other.gameObject);
                    audiosource.PlayOneShot(audioclip[5]);
                }
            }
        }
    }

}

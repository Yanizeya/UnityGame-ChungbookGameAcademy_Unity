using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNote : MonoBehaviour
{
    public GameObject note;
    public float btm;
    private float currentTime;
    private int i;
    public int[] MusicScale = new int[20];
    public float[] MusicTime = new float[20];
    Vector3 def_position;
    float def_btm;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        def_position = transform.position;
        def_btm = btm;
        btm *= MusicTime[i];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(btm);

        currentTime += Time.deltaTime;
        if (currentTime >= btm)
        {
            transform.position += Vector3.right * MusicScale[i] * 20;
            Instantiate(note, transform.position, transform.rotation);
            currentTime -= btm;
            i++;
            transform.position = def_position;
            btm = def_btm;
            btm *= MusicTime[i];
        }
    }
}

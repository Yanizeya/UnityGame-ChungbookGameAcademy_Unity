using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public Animator ani;
    string hit = "Hit";
    string[] validKey = NoteTouchSenser.validKey;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void noteHitEffect()
    {
        Debug.Log("noteAniOn : " + Time.time);
        ani.SetTrigger("Hit");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    public Animator noteHitAni;
    public Animator scoreAni;
    string hit = "Hit";
    string[] validKey = InputManager.validKeyArr;
    public Sprite[] scoreSprite;

    enum scoreType{
       Perfect,
       Good,
       Cool,
       Normal,
       Bad,
       Miss,
       EnumMax
    }
    // Start is called before the first frame update
    void Start()
    {
        noteHitAni = transform.Find("noteHitEffect").GetComponent<Animator>();
        scoreAni = transform.Find("scoreEffect").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Effect(string scoreZone)
    {
        NoteHitEffect();
        ScoreEffect(scoreZone);
    }
    public void NoteHitEffect()
    {
        //Debug.Log("noteAniOn : " + Time.time);
        noteHitAni.SetTrigger("Hit");
    }

    public void ScoreEffect(string scoreZone)
    {
        int type = 0;
        switch (scoreZone)
        {
            case "Perfect":
                type = (int)scoreType.Perfect;
                break;
            case "Nice":
                type = (int)scoreType.Cool;
                break;
            case "Good":
                type = (int)scoreType.Good;
                break;
            case "Bad":
                type = (int)scoreType.Bad;
                break;
        }

        transform.Find("scoreEffect").GetComponent<Image>().sprite = scoreSprite[type];
        scoreAni.SetTrigger("Hit");
    }
}

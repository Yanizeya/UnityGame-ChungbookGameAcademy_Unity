using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardSponer : MonoBehaviour
{

    public GameObject keyboardPrefab;
    public GameObject canvas;
    public static float keyboardSize = 102 * 0.5f; //이미지의 픽셀크기 * localScale의 크기
    public static float keyboardSpace = 10;
    Vector3 sponeLocation;
    Quaternion sponeRotation;
    Vector3 currentLocation;
   

    string[][] keyboardSpell = { new string[]{ "a", "s", "d", "f", "g", "h", "j", "k", "l", ";" }, new string[]{ "z", "x", "c", "v", "b", "n", "m", ",", "." }, new string[]{ } };
    // Start is called before the first frame update
    void Start()
    {
        sponeLocation = transform.position;
        sponeRotation = transform.rotation;
        canvas = GameObject.Find("1LayerCanvas");


        for (int i = 0; i < 3; i++)
        {
            currentLocation = sponeLocation;
            for (int j = 0; j < keyboardSpell[i].Length; j++)
            {
                if (j == 0) //처음 스포너 위치 설정
                {
                    switch (i)
                    {
                        case 0:
                            currentLocation.x -= keyboardSize * 4.5f + keyboardSpace * 4f;
                            break;
                        case 1:
                            currentLocation.x -= keyboardSize * 4f + keyboardSpace * 4f;
                            currentLocation.y -= keyboardSize + keyboardSpace;
                            break;
                    }
                   
                }
                keyboardPrefab.transform.Find("A").GetComponent<Text>().text = keyboardSpell[i][j].ToUpper();
                GameObject keyboard = Instantiate(keyboardPrefab, currentLocation, sponeRotation);
                keyboard.transform.SetParent(canvas.transform);
                //키보드에 태그를 부여함
                keyboard.name = keyboardSpell[i][j];
                currentLocation.x += keyboardSize + keyboardSpace;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}

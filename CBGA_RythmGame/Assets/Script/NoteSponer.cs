using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSponer : MonoBehaviour
{
    public GameObject notePrefab;
    public GameObject canvas;
    float noteSize = 96 * 0.5f; //이미지의 픽셀크기 * localScale의 크기
    float noteSpace; //(키보드 사이즈 - 노트 사이즈)/2
    float keyboardSize;
    float keyboardSpace;
    Vector3 sponeLocation;
    Quaternion sponeRotation;
    Vector3 currentLocation;


    int i = 0;
    
    float wait = 0;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("0LayerCanvas");
        keyboardSize = KeyboardSponer.keyboardSize;
        keyboardSpace = KeyboardSponer.keyboardSpace;
        noteSpace = 10 + ((keyboardSize - 96) / 2);

        sponeLocation = transform.position;
        sponeRotation = transform.rotation;

        currentLocation = sponeLocation;
        currentLocation.x -= keyboardSize * 4.5f + keyboardSpace * 4f;
        currentLocation.y += 300;
        
    }

    // Update is called once per frame
    void Update()
    {
        wait += Time.deltaTime;
        if (wait > 1.5f)
        {
            if (i < 10)
            {
                if (i != 4 && i != 5)
                {
                    Instantiate(notePrefab, currentLocation, sponeRotation).transform.parent = canvas.transform;
                    wait -= 1.5f;
                }
                currentLocation.x += keyboardSize + keyboardSpace;
                i++;
            }
        }
    }
}

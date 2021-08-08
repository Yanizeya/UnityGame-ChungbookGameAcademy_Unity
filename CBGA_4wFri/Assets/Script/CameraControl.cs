using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    GameObject parent;
    Vector3 defPosition;     //defaultPosition
    Quaternion defRotation;  //Quaternion은 rotation값을 저장하는 클래스
    float defZoom;           //fieldOfView 값은 float

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;

        // 기본 위치 지정
        defPosition = transform.position;
        defRotation = parent.transform.rotation; 
        defZoom = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        // 왼쪽 드래그로 카메라 이동
        if (Input.GetMouseButton(0)) 
        {
            transform.Translate(Input.GetAxis("Mouse X") / 10, Input.GetAxis("Mouse Y") / 10, 0);
            // Camera.main.transform.Translate()에서 위와 같이 변경
            // 그냥 transform.Translate()를 쓰면 이 스크립트가 적용된 오브젝트에 기능이 적용됨
        }

        // 오른쪽 드래그로 카메라 회전
        if(Input.GetMouseButton(1))
        {
            parent.transform.Rotate(-Input.GetAxis("Mouse Y") * 10, Input.GetAxis("Mouse X") * 10, 0);
            // Camera.main.transform.Rotate()에서 위와 같이 변경
        }

        // 휠 회전으로 확대/축소
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Camera.main.fieldOfView += (20 * -Input.GetAxis("Mouse ScrollWheel"));
            if (Camera.main.fieldOfView < 10)
                Camera.main.fieldOfView = 10;
            else if (Camera.main.fieldOfView > 100)
                Camera.main.fieldOfView = 100;
        }

        // 휠 클릭으로 설정 초기화
        if(Input.GetMouseButton(2))
        {
            transform.position = defPosition;
            parent.transform.rotation = defRotation;
            Camera.main.fieldOfView = defZoom;
        }
    }
}

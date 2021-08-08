using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 360f;
    public float mouseSensitiv = 0.5f;
    CharacterController charCtrl;
    Animator anim;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 moveX = Input.GetAxisRaw("Horizontal") * transform.right;
        Vector3 moveZ = Input.GetAxisRaw("Vertical") * transform.forward;
        Vector3 velocity = moveX + moveZ;
        transform.Rotate(0, Input.GetAxis("Mouse X") * 10 * mouseSensitiv, 0);

        charCtrl.Move(velocity * 5f * Time.deltaTime);

        anim.SetFloat("Speed", charCtrl.velocity.magnitude);

        if (GameObject.FindGameObjectsWithTag("Dot").Length < 1)
            Destroy(door);

    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Dot":
                Destroy(other.gameObject);
                break;
            case "Enemy":
                SceneManager.LoadScene("Lose");
                break;
            case "EndZone":
                SceneManager.LoadScene("Win");
                break;
        }
    }
}


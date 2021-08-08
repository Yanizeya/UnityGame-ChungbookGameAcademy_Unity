using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent agent;
    Animator anim;
    bool Grogy = false;
    float GrogyTime = 3f;
    //public Transform Aimtr;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        //Aimtr = GameObject.Find("Crosshair").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2); 
        
        agent.destination = target.transform.position;
        anim.SetFloat("Speed", agent.velocity.magnitude);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(center); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("shot");
                if (hit.collider.tag == "Enemy")
                {
                    Debug.Log("hit");
                    agent.isStopped = true;
                    Grogy = true;
                }
            }
        }
        if (Grogy == true)
        {
            if (GrogyTime > 0)
                GrogyTime -= Time.deltaTime;
            else
            {
                agent.isStopped = false;
                Grogy = false;
                GrogyTime = 3f;
            }
        }
    }
}

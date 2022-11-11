using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            float distanceToMove = .2f * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position,
                this.transform.position + new Vector3(0f, distanceToMove, 0f), distanceToMove);
        }
    }
}

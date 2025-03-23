using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _laserspeed = 12f;

    void Start()
    {
    }

    void Update()
    {

        transform.Translate(new Vector3(0,1,0) * _laserspeed * Time.deltaTime);


        if (transform.position.y > 11.24f)
        {
         
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }

        
           
    }
}

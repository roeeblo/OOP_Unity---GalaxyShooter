using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private float _laserspeed = -6f;

    void Start()
    {
    }

    void Update()
    {

        transform.Translate(new Vector3(0, 1, 0) * _laserspeed * Time.deltaTime);


        if (transform.position.y < -11.24f)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }




    }
   
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _powerupspeed = 3.0f;

    [SerializeField]
    private int powerupID; // 0 = tripleL, 1 = speed, 2 = shield

    [SerializeField]
    private AudioClip _PUsound;

    void Start()
    {
        if (_PUsound == null)
        {
            Debug.LogError("pusound is NULL");
        }
    }

    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * _powerupspeed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);

        }
     
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)

            {
                switch (powerupID) 
                {
                    case 0:
                        player.GetTripleShot();
                        break;
                    case 1:
                        player.GetSpeedBoost(); 
                        break;
                    case 2:
                        player.GetShield();
                        break;
                }

            }
            
            Destroy(this.gameObject);
            AudioSource.PlayClipAtPoint(_PUsound, transform.position);
        }
    }


}

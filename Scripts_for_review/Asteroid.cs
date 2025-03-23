using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Asteroid : MonoBehaviour
{

    private SpawnManager _spawnManager;
    [SerializeField]
    private float _RotationSpeed = 30f;

    [SerializeField]
    public GameObject _ExplosionPrefab;

    void Start()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    void Update()
    {
        transform.Rotate(0, 0, _RotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Laser")
        {
            Instantiate(_ExplosionPrefab,transform.position,Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject);

        }

    }
}




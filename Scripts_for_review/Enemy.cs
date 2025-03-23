using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _EnemySpeed = 4;
    private Player _player;
    private Animator _animator;
    private bool _isDead = false;
    [SerializeField]
    private AudioClip _explosionaudio;
    private AudioSource _audiosource;
    [SerializeField]
    private AudioClip _laseraudio;
    private AudioSource _laseraudiosource;

    [SerializeField]
    private GameObject _enemylaserprefab;

    public float firerate = 0.5f;
    private float FireFlag = -1f;




    void Start()
    {
        _audiosource = GetComponent<AudioSource>();
        if (_audiosource == null)
        {
            _audiosource = gameObject.AddComponent<AudioSource>();
        }

        _laseraudiosource = GetComponent<AudioSource>();
        if (_laseraudiosource == null)
        {
            _laseraudiosource = gameObject.AddComponent<AudioSource>();
        }

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("animator is NULL");
        }

        transform.position = new Vector3(Random.Range(-8, 8), 10.24f, 0);

    }

    void Update()
    {
        if (_isDead)
            return; 

        transform.Translate(new Vector3(0, -1, 0) * _EnemySpeed * Time.deltaTime);

        
        if (transform.position.y < -8.0f)
        {
            transformrandomizer();
        }

        if (Time.time > FireFlag)
        {
            Laser();
            _laseraudiosource.PlayOneShot(_laseraudio);
        }



    }
    void transformrandomizer()
    {
        transform.position = new Vector3(Random.Range(-8, 8), 10.24f, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (_isDead)
            return;

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {    
                player.Damage();
            }

            HandleDeath();

        }


        if (other.CompareTag("Laser"))
        {
            Debug.Log("Laser hit: " + other.name);

            Destroy(other.gameObject);

            HandleDeath();

            if (_player != null)
            {
                _player.ScoreAdder(10);

            }
            

        }
        
    }
    private void HandleDeath()
    {
        if (_isDead)
            return;

        _isDead = true;
        _animator.SetTrigger("EnemyDeath");
        _EnemySpeed = 0;
        Destroy(gameObject, 2.7f);
        _audiosource.PlayOneShot(_explosionaudio);
        FireFlag = float.MaxValue;
    }
    void Laser()
    {

        if (_isDead)
            return;

        firerate = Random.Range(3.0f, 7.0f); 
        FireFlag = Time.time + firerate;
        Instantiate(_enemylaserprefab, transform.position, Quaternion.identity);
 
    }



}


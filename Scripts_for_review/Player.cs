using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour

{
    float _speed = 8f;
    float _speedMulti = 2f;
    [SerializeField]
    public float firerate = 0.5f;
    private float FireFlag = -1f;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _TripleLaserPreFab;
    [SerializeField]
    private GameObject _shieldvisual;
    [SerializeField]
    private GameObject GameOverText;
    [SerializeField]
    private GameObject _rightfire;
    [SerializeField]
    private GameObject _leftfire;
    [SerializeField]
    private AudioSource _laseraudio;

    UIManager _uimanager;

    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;

    private SpawnManager _spawnmanager;
    private GameManager _gamemanager;


    private bool isTripleShot = false;
    private bool isSpeedBoost = false;
    private bool isShield = false;

    public bool isplayer1 = false;
    public bool isplayer2 = false;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnmanager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_spawnmanager == null )
        {
            Debug.LogError("Null SpawnManager");
        }
        _shieldvisual.SetActive(false);
        _leftfire.SetActive(false);
        _rightfire.SetActive(false);

        if (_gamemanager.isCoopMode == false)
        {
            transform.rotation = Quaternion.identity;
            transform.position = new Vector3(0, 0, 0);
        }
        _animator.speed = 1f;


    }

    void Update()

    {
        if (isplayer1 == true)
        {
            CalcMovement();
            HandleAnimation();


            if (Input.GetKeyDown(KeyCode.Space) && Time.time > FireFlag)
            {
                Laser();
            }
        }
       if (isplayer2 == true)
        {
            CalcMovement2();
            HandleAnimation();


            if (Input.GetKeyDown(KeyCode.Return) && Time.time > FireFlag)
            {
                Laser2();
            }
        }

    }

    void Laser()
    {
        FireFlag = Time.time + firerate;
       
        if (isTripleShot == true)
        {
            Instantiate(_TripleLaserPreFab,transform.position, Quaternion.identity);

        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _laseraudio.Play();
    }

    void CalcMovement()
    {
        float verticalInp = Input.GetAxis("Vertical");
        float horizontalInp = Input.GetAxis("Horizontal");

        if (isSpeedBoost == false) transform.Translate(new Vector3(horizontalInp, verticalInp, 0) * _speed * Time.deltaTime);
        else transform.Translate(new Vector3(horizontalInp, verticalInp, 0) * _speed * _speedMulti * Time.deltaTime);

        if (transform.position.y >= 5.86)
        {
            transform.position = new Vector3(transform.position.x, 5.86f, 0);
        }
        else if (transform.position.y <= -3.79f) 
        {
            transform.position = new Vector3(transform.position.x, -3.79f, 0);

        }




        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);

        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);

        }
    }
    void Laser2()
    {
        FireFlag = Time.time + firerate;

        if (isTripleShot == true)
        {
            Instantiate(_TripleLaserPreFab, transform.position, Quaternion.identity);

        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _laseraudio.Play();
    }

    void CalcMovement2()
    {
        float verticalInp = Input.GetAxis("VerticalP2");
        float horizontalInp = Input.GetAxis("HorizontalP2");

        if (isSpeedBoost == false) transform.Translate(new Vector3(horizontalInp, verticalInp, 0) * _speed * Time.deltaTime);
        else transform.Translate(new Vector3(horizontalInp, verticalInp, 0) * _speed * _speedMulti * Time.deltaTime);

        if (transform.position.y >= 5.86)
        {
            transform.position = new Vector3(transform.position.x, 5.86f, 0);
        }
        else if (transform.position.y <= -3.79f)
        {
            transform.position = new Vector3(transform.position.x, -3.79f, 0);

        }




        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);

        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);

        }
    }

    void HandleAnimation()
    {
        if (isplayer1)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            _animator.SetBool("TurnRight", horizontalInput > 0);
            _animator.SetBool("TurnLeft", horizontalInput < 0);
        }
        else if (isplayer2)
        {
            float horizontalInput = Input.GetAxis("HorizontalP2");

            _animator.SetBool("TurnRight", horizontalInput > 0);
            _animator.SetBool("TurnLeft", horizontalInput < 0);
        }
    }



    public void Damage()
    {
        if (isShield == true)
        {
            isShield = false;
            _shieldvisual.SetActive(false);
            return;
        }

        _lives--;
        _uimanager.UpdateLives(_lives);

        if (_lives == 2)
        {
            _leftfire.SetActive(true);
        }
        if (_lives == 1)
        {
            _rightfire.SetActive(true);
        }
        if (_lives == 0)
        {
            _spawnmanager.GetIsDead();
            _uimanager.GameOverScene();
            Destroy(this.gameObject);
            
           
        }
    }
    public void GetShield()
    {
        isShield = true;
        _shieldvisual.SetActive(true);
    }


    public void GetTripleShot()
    {
        isTripleShot = true;
        StartCoroutine(TripleShotRotuine());
    }

    IEnumerator TripleShotRotuine()
    {
        while (isTripleShot)
        {

            yield return new WaitForSeconds(5.0f);
            isTripleShot = false;

        }
    }

    public void GetSpeedBoost()
    {
        isSpeedBoost = true;
        StartCoroutine(SpeedBoostPURoutine());
    }

    IEnumerator SpeedBoostPURoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoost = false;
    }    


    public void ScoreAdder(int points)
   {
       _score += points;
        _uimanager.UpdateScoreSys(_score);
   }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyLaser")
        {
            Damage();

            Destroy(other.gameObject);
        }
    }
}

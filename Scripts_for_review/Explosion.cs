using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private AudioClip _explosionsound;
    [SerializeField]
    private AudioSource _audiosource;
    void Start()
    {
        _audiosource = GetComponent<AudioSource>();
        if (_audiosource == null )
        {
            _audiosource = gameObject.AddComponent<AudioSource>();
        }
        Destroy(this.gameObject, 3.0f);
        _audiosource.PlayOneShot(_explosionsound);
    }

}

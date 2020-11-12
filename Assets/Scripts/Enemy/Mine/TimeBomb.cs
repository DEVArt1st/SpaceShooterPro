using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomb : MonoBehaviour
{

    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private float _timeLeft = 6.0f;

    [SerializeField]
    private GameObject _explosionPrefab;
    private GameObject _target;

    [SerializeField]
    private AudioClip _fastBeep;

    private Animator _anim;
    private AudioSource _audioSource;

    private bool _hasExploded = false;
    private bool _fastAudioHasPlayed = false;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _target = GameObject.Find("Player");

        if (_target == null)
        {
            Debug.LogError("Player(Target) is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CountDown();
    }


    void CountDown()
    {
        _timeLeft -= Time.deltaTime;

        if (_timeLeft < 1f && _fastAudioHasPlayed == false)
        {
            _audioSource.clip = _fastBeep;
            _audioSource.loop = false;
            _audioSource.Play();
            _fastAudioHasPlayed = true;
        }

        if (_timeLeft <= 0 && _hasExploded == false)
        {
            _hasExploded = true;

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(this.gameObject, 0.15f);
            Destroy(this.GetComponent<CircleCollider2D>());
            Destroy(this.GetComponent<Rigidbody2D>());
        }
    }

    void Movement()
    {
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime); //moves toward the player
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.15f);
            _speed = 0f;
        }

        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.15f);
            Destroy(other.gameObject);
            _speed = 0f;
        }
    }

}


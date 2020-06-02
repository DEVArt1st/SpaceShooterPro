using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanMine : MonoBehaviour
{

    [SerializeField]
    private float _speed = 1;
    [SerializeField]
    private float _diffSpeed = 0.01f;
    private float _rotationSpeedMin = -60f;
    private float _rotationSpeedMax = 60f;
    [SerializeField]
    private float _timeLeft = 6.0f;

    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private GameObject _explosionPrefab;

    private Animator _anim;

    private bool _hasExploded = false;

    [SerializeField]
    private AudioSource _beepSlow;
    [SerializeField]
    private AudioSource _beepFast;
    [SerializeField]
    private AudioClip _fastAudio;

    private bool _fastAudioHasPlayed = false;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _target = GameObject.Find("Player");
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

        if (_timeLeft <= 1f)
        {
            _beepSlow.Stop();

            if (!_fastAudioHasPlayed)
            {
                _beepFast.PlayOneShot(_fastAudio);
                _fastAudioHasPlayed = true;
            }
        }

        if (_timeLeft <= 0 && !_hasExploded)
        {
            _hasExploded = true;

            _beepSlow.Stop();
            _fastAudioHasPlayed = true;
            _beepFast.Stop();
            _anim.SetTrigger("OnTargetHit");
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 3f);
            _diffSpeed = 0;
            Destroy(GameObject.FindWithTag("Mine"));
            Destroy(this.GetComponent<CircleCollider2D>());
            Destroy(this.GetComponent<Rigidbody2D>());
        }
    }

    void Movement()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _diffSpeed); //moves toward the player
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

            _anim.SetTrigger("OnTargetHit");
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 3f);
            _diffSpeed = 0;
            Destroy(GameObject.FindWithTag("Mine"));
            Destroy(this.GetComponent<CircleCollider2D>());
            Destroy(this.GetComponent<Rigidbody2D>());
        }

        if (other.tag == "Laser")
        {
            _anim.SetTrigger("OnTargetHit");
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 3f);
            Destroy(other.gameObject);
            _diffSpeed = 0;
            Destroy(GameObject.FindWithTag("Mine"));
            Destroy(this.GetComponent<CircleCollider2D>());
            Destroy(this.GetComponent<Rigidbody2D>());
        }
    }

}


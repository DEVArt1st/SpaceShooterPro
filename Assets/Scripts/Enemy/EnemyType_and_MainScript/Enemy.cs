using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private float _canFire = 1f;
    private float _fireRate = 3.0f;
    [SerializeField]
    private int _shieldLife;
    [SerializeField]
    private float _diffSpeed = 0.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _mine;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _courseDiverter;

    private Player _player;
    private Animator _anim;

    [SerializeField]
    private AudioClip _explosionSoundClip;
    private AudioSource _audioSource;

    private bool _isShootingActive = true;
    private bool _isShieldActive = false;

    [SerializeField]
    private int _enemyID;
    
    private float _rotateSpeed = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        
        if (_player == null)
        {
            Debug.LogError("Player is Null");
        }

        //assign the componet to Anim
        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire)
        {
            EnemyLaserFire();
        }

    }

    private void Awake()
    {
        EnemyType();
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        float randomX = Random.Range(-8f, 8f);
        float randomZ = Random.Range(-65, 65);

        Quaternion to = Quaternion.LookRotation(Vector3.zero);

        if (transform.position.y <= -5)
        {
            transform.position = new Vector3(randomX, 7, 0);
            transform.rotation = Quaternion.Euler(0, 0, randomZ);
        }

        if (transform.rotation.z <= 80)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, to, _rotateSpeed * Time.deltaTime);
        }
        if (transform.rotation.z >= -80)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, to, _rotateSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            if (_isShieldActive == true)
            {
                _shieldLife--;

                if (_shieldLife <= 0)
                {
                    _shieldVisualizer.SetActive(false);
                    _isShieldActive = false;
                }

                return;
            }

            _anim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
            Destroy(GetComponent<Collider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            _isShootingActive = false;
            _courseDiverter.SetActive(false);
        }
    }



    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _shieldLife--;

            if (_shieldLife <= 0)
            {
                _shieldVisualizer.SetActive(false);
                _isShieldActive = false;
            }

            return;
        }
        _anim.SetTrigger("OnEnemyDeath");
        _audioSource.Play();
        _speed = 0;
        Destroy(this.gameObject, 2.8f);



        if (_player != null)
        {
            _player.AddScore(10);
        }

        Destroy(GetComponent<Collider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        _isShootingActive = false;
        //_courseDiverter.SetActive(false);

    }

    public void EnemyLaserFire()
    {
        _fireRate = Random.Range(3f, 7f);
        _canFire = Time.time + _fireRate;


        if (_isShootingActive == true)
        {

            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.gameObject.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }


    void EnemyMineDeployer()
    {
        _isShootingActive = false;
        StartCoroutine(MineDeployer());
    }

    IEnumerator MineDeployer()
    {
        yield return new WaitForSeconds(1.5f);

        Instantiate(_mine, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(6f);
    }

    void AggressiveEnemy()
    {
        _isShootingActive = false;
    }

    void EnemyType()
    {
        switch (_enemyID)
        {
            case 0:
                EnemyLaserFire();
                break;
            case 1:
                EnemyMineDeployer();
                break;
            case 2:
                EnemyMineDeployer();
                _isShieldActive = true;
                _shieldVisualizer.SetActive(true);
                break;
            case 3:
                EnemyLaserFire();
                _isShieldActive = true;
                _shieldVisualizer.SetActive(true);
                break;
            case 4:
                AggressiveEnemy();
                _isShieldActive = true;
                _shieldVisualizer.SetActive(true);
                break;
            default:
                Debug.Log("Default Value");
                break;
        }
    }
}

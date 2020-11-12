using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum EnemyType
    {
        LaserCannon,
        LaserCannonShielded,
        TimeBombDeployer,
        TimeBombDeployerShielded,
        Agressive
    }

    [SerializeField]
    private EnemyType _enemyType;

    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _anim;
    private ShieldedEnemy _shieldedEnemy;
    private EnemyTimeBomber _enemyTimeBomber;
    private EnemyLaserCannon _enemyLaserCannon;
    private EnemyAggressive _enemyAggressive;

    [SerializeField]
    private AudioClip _explosionSoundClip;
    private AudioSource _audioSource;

    [SerializeField]
    private bool _isShieldActive = false;


    private float _rotateSpeed = 0.5f;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _shieldedEnemy = GetComponentInParent<ShieldedEnemy>();
        _enemyTimeBomber = GetComponentInParent<EnemyTimeBomber>();
        _enemyLaserCannon = GetComponentInParent<EnemyLaserCannon>();
        _enemyAggressive = GetComponentInChildren<EnemyAggressive>();

        _isShieldActive = true;

        if (_player == null)
        {
            Debug.LogError("Player is Null");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL");
        }
    }

    void Update()
    {
        CalculateMovement();

        SwitchEnemyType();
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
                _shieldedEnemy.Shield();
                _isShieldActive = false;
            }

            if (_enemyType == EnemyType.Agressive)
            {
                _enemyAggressive.Disable();
            }

            if (_enemyType == EnemyType.LaserCannon || _enemyType == EnemyType.TimeBombDeployerShielded)
            {
                _enemyLaserCannon.StopFiring();
            }

            if (_enemyType == EnemyType.TimeBombDeployer || _enemyType == EnemyType.LaserCannonShielded)
            {
                _enemyTimeBomber.StopDeployment();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
            Destroy(GetComponent<Collider2D>());
            Destroy(GetComponent<Rigidbody2D>());

        }
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _shieldedEnemy.Shield();
            _isShieldActive = false;

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

        if (_enemyType == EnemyType.Agressive)
        {
            _enemyAggressive.Disable();
        }

        if (_enemyType == EnemyType.LaserCannon || _enemyType == EnemyType.LaserCannonShielded)
        {
            _enemyLaserCannon.StopFiring();
        }

        if (_enemyType == EnemyType.TimeBombDeployer || _enemyType == EnemyType.TimeBombDeployerShielded)
        {
            _enemyTimeBomber.StopDeployment();
        }

        Destroy(GetComponent<Collider2D>());
        Destroy(GetComponent<Rigidbody2D>());
    }

    public void DodgeRight()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    public void DodgeLeft()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    private void SwitchEnemyType()
    {
        switch (_enemyType)
        {
            case EnemyType.LaserCannon:
                _enemyLaserCannon.FireLaser();
                _isShieldActive = false;
                break;
            case EnemyType.TimeBombDeployer:
                _enemyTimeBomber.DeployBomb();
                _isShieldActive = false;
                break;
            case EnemyType.TimeBombDeployerShielded:
                _enemyTimeBomber.DeployBomb();
                break;
            case EnemyType.LaserCannonShielded:
                _enemyLaserCannon.FireLaser();
                break;
            case EnemyType.Agressive:
                _enemyAggressive.RamPlayer();
                break;
            default:
                Debug.Log("Default Value");
                break;
        }
    }
}

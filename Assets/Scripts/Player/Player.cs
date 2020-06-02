using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _misslePrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _rightEnginePrefab;
    [SerializeField]
    private GameObject _leftEnginePrefab;
    [SerializeField]
    private float _fireRate = 1.0f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _shieldLives = 0;
    private SpawnManager _spawnManager;
    private CameraShake _camera;

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    private bool _isMissleActive = false;
    private bool _laserFire = true;
    private bool _isThrustActive = false;

    [SerializeField]
    private int _score;
    [SerializeField]
    private int _ammo;
    private float _maxThrust = 100f;
    [SerializeField]
    private float _currentThrust;
    private Coroutine _regen;

    [SerializeField]
    private GameObject _shieldVisualizer;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioClip _powerupSoundClip;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _camera = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        _currentThrust = _maxThrust;

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is NULL");
        }

        if (_camera == null)
        {
            Debug.LogError("The CameraShaker is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalcualteMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
            FireMissile();
        }
    }

    void CalcualteMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);


        transform.Translate(direction * _speed * Time.deltaTime);



        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0));

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

        SpeedBooster();

    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        //Change these if and else statements to Switch Statements Later;
        if (_laserFire == true)
        {
            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                _audioSource.Play();
            }
            else if (_ammo > 0)
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
                _ammo = _ammo - 1;
                _uiManager.UpdateAmmo(_ammo);
                _audioSource.Play();
            }
        }
    }

    //Missle Fire Method
    public void FireMissile()
    {
        if (_isMissleActive == true)
        {
            for (var i = 0; i < 1; i++)
            {
                float randomX = (Random.Range(0, 2) == 0) ? 0.55f : -0.55f;
                Instantiate(_misslePrefab, transform.position + new Vector3(randomX, -1.25f, 0), Quaternion.identity);
            }

        }
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {

            _shieldLives -= 1;

            if (_shieldLives == 2)
            {
                _shieldVisualizer.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else if (_shieldLives == 1)
            {
                _shieldVisualizer.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                _isShieldActive = false;
                _shieldVisualizer.SetActive(false);
            }

            return;
        }

        _lives -= 1;
        _camera.TriggerShake();

        if (_lives == 2)
        {
            _rightEnginePrefab.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEnginePrefab.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            Destroy(GetComponent<BoxCollider2D>());
            _uiManager.GameOverTextFlicker();
            _uiManager.RestartGameText();
        }

    }

    public void TripleShotActive()
    {
        _audioSource.PlayOneShot(_powerupSoundClip);
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _audioSource.PlayOneShot(_powerupSoundClip);
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _audioSource.PlayOneShot(_powerupSoundClip);
        _shieldLives = 3;
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
        _shieldVisualizer.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void AddScore(int points)
    {
        _score = _score + points;
        _uiManager.UpdateScore(_score);
    }

    public void AmmoCollected()
    {
        _audioSource.PlayOneShot(_powerupSoundClip);
        _ammo = 15;
        _uiManager.UpdateAmmo(_ammo);
    }

    public void HealthCollected()
    {
        _audioSource.PlayOneShot(_powerupSoundClip);
        if (_lives < 3)
        {
            _lives += 1;
        }

        if (_lives == 2)
        {
            _leftEnginePrefab.SetActive(false);
        }
        else if (_lives == 3)
        {
            _leftEnginePrefab.SetActive(false);
            _rightEnginePrefab.SetActive(false);
        }
        _uiManager.UpdateLives(_lives);
    }

    public void MissileShotActive()
    {
        _laserFire = false;
        _isMissleActive = true;
        _audioSource.PlayOneShot(_powerupSoundClip);
        StartCoroutine(MissilePowerDownRoutine());
    }

    IEnumerator MissilePowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isMissleActive = false;
        _laserFire = true;
    }

    void SpeedBooster()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _currentThrust > 0)
        {
            _speed += 5f;
            _isThrustActive = true;

            StartCoroutine(CurrentThrustSubtract());
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isThrustActive = false;
            _speed = 5f;

            if (_regen != null)
            {
                StopCoroutine(_regen);
            }

            _regen = StartCoroutine(CurrentThrustRegen());
        }
    }

    IEnumerator CurrentThrustSubtract()
    {
        while (_currentThrust > 0 && _isThrustActive == true)
        {
            yield return new WaitForSeconds(0.03f);
            _currentThrust--;
            _uiManager.UpdateThruster(_currentThrust);

            if (_currentThrust == 0)
            {
                _isThrustActive = false;
                _speed = 5f;
            }
        }
    }

    IEnumerator CurrentThrustRegen()
    {

        yield return new WaitForSeconds(3.5f);

        while (_currentThrust < _maxThrust)
        {
                _currentThrust += _maxThrust / 100;
                _uiManager.UpdateThruster(_currentThrust);
                yield return new WaitForSeconds(0.01f);
        }
        _regen = null;
    }

    public void PowerDownSpeed()
    {
        if (_currentThrust >= 100)
        {
            _currentThrust = 50;
            _uiManager.UpdateThruster(_currentThrust);
        }
    }
}

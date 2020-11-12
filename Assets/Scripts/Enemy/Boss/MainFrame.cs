using System.Collections;
using UnityEngine;

public class MainFrame : MonoBehaviour
{
    [SerializeField]
    private int _health = 20;
    [SerializeField]
    private float _speed = 1;

    private bool _isExploding;
    private bool _isBossDead;

    //Shown in Inspector
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _finalExplosionPrefab;
    [SerializeField]
    private GameObject _shipFireOne;
    [SerializeField]
    private GameObject _shipFireTwo;
    [SerializeField]
    private GameObject _shipFireThree;
    [SerializeField]
    private GameObject _shipFireFour;

    //Hidden from Inspector
    private GameObject[] _thrusters;
    private GameObject[] _mkOneTurrets;
    private GameObject[] _mkFourTurrets;
    private GameObject[] _mainGunTurrets;

    private Animator _anim;
    private SpawnManager _spawnManager;
    private BoxCollider2D _boxCollider;
    private UIManager _uiManager;
    private GameManager _gameManager;
   

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is NULL");
        }

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    private void Update()
    {
        _thrusters = GameObject.FindGameObjectsWithTag("BossShipThrusters");
        _mkOneTurrets = GameObject.FindGameObjectsWithTag("MKOneTurret");
        _mkFourTurrets = GameObject.FindGameObjectsWithTag("MKFourTurret");
        _mainGunTurrets = GameObject.FindGameObjectsWithTag("MainGunTurret");

        MoveDown();
        Stop();
        ActivateCollider();
    }

    public void Damage()
    {
        _health -= 10;

        if (_health <= 0)
        {
            Destroy(this.gameObject,3f);
            _anim.SetTrigger("OnTargetHit");
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject.GetComponent<PolygonCollider2D>());
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            _spawnManager.OnBossDestroyed();
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    void Stop()
    {
        if (transform.position.y <= 5)
        {
            _speed = 0;

            foreach (var thrusters in _thrusters)
            {
                thrusters.gameObject.SetActive(false);
            }

            foreach (var mkOneTurret in _mkOneTurrets)
            {
                if (mkOneTurret != null)
                {
                    mkOneTurret.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    mkOneTurret.gameObject.GetComponentInChildren<MKOneTurrentFire>().gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }

            foreach (var mkFourTurret in _mkFourTurrets)
            {
                if (mkFourTurret != null)
                {
                    mkFourTurret.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    mkFourTurret.gameObject.GetComponentInChildren<MKFourTurrentFire>().gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }

            foreach (var mainGunTurret in _mainGunTurrets)
            {
                if (mainGunTurret != null)
                {
                    mainGunTurret.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    mainGunTurret.gameObject.GetComponentInChildren<MainGunFire>().gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
    }

    private void ActivateCollider()
    {
        if (_mkOneTurrets.Length == 0 && _mkFourTurrets.Length == 0 & _mainGunTurrets.Length == 0)
        {
            if (_boxCollider != null)
            {
                _boxCollider.enabled = true;
            }
        }
    }

    public void Damage(int amount)
    {
        _health -= amount;

        if (_health <= 0 && _boxCollider == true)
        {
            _isExploding = true;
            _health = 0;
            Destroy(this._boxCollider);
            StartCoroutine(Exploding());
        }
    }

    IEnumerator Exploding()
    {
        float timeToWait = 1.0f;
        float shipFireActiveTime = 0.5f;

        while (_isExploding == true)
        {
            Instantiate(_explosionPrefab, transform.position + Position(-6.5f, -1.5f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(shipFireActiveTime);
            _shipFireOne.SetActive(true);
            yield return new WaitForSeconds(timeToWait);
            Instantiate(_explosionPrefab, transform.position + Position(2.8f, 0.5f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(shipFireActiveTime);
            _shipFireTwo.SetActive(true);
            yield return new WaitForSeconds(timeToWait);
            Instantiate(_explosionPrefab, transform.position + Position(-2.6f, 0.1f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(shipFireActiveTime);
            _shipFireThree.SetActive(true);
            yield return new WaitForSeconds(timeToWait);
            Instantiate(_explosionPrefab, transform.position + Position(6.6f, -1.52f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(shipFireActiveTime);
            _shipFireFour.SetActive(true);
            yield return new WaitForSeconds(2f);
            Destroy(this.gameObject, 0.25f);
            Instantiate(_finalExplosionPrefab, transform.position, Quaternion.identity);
            Instantiate(_finalExplosionPrefab, transform.position + Position(-4.9f, -1f, 0f), Quaternion.identity);
            Instantiate(_finalExplosionPrefab, transform.position + Position(4.6f, -1f, 0f), Quaternion.identity);

            _isExploding = false;
            _uiManager.ButtonActivation();
            _uiManager.RestartGameText();
            _uiManager.WinningText();
            _gameManager.GameWon();
        }
    }

    Vector3 Position(float x, float y, float z)
    {
        return new Vector3(x, y, z);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;

public class MainFrame : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _turrents;

    [HideInInspector]
    public int _health = 10;

    public float _speed = 1;

    [SerializeField]
    private GameObject[] _turrentFireBig;
    [SerializeField]
    private GameObject[] _turrentFireMKOne;
    [SerializeField]
    private GameObject[] _turrentFireMKFour;

    [SerializeField]
    private GameObject[] _turrentBig;
    [SerializeField]
    private GameObject[] _turrentMKOne;
    [SerializeField]
    private GameObject[] _turrentMKFour;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject _leftThrust;
    [SerializeField]
    private GameObject _rightThrust;

    private Animator _anim;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private Player _player;

    [HideInInspector]
    public bool bossDefeated = false;

    private void Start()
    {
        _anim = GetComponent<Animator>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
    }

    private void Update()
    {
        if (_turrents[0] == null && _turrents[1] == null && _turrents[2] == null && _turrents[3] == null && _turrents[4] == null && _turrents[5] == null && _turrents[6] == null && _turrents[7] == null && _turrents[8] == null)
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }

        MoveDown();
        Stop();
    }

    public void Damage()
    {
        _health -= 10;

        if (_health <= 0)
        {
            Destroy(this.gameObject,3f);
            _anim.SetTrigger("OnTargetHit");
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            _leftThrust.SetActive(false);
            _rightThrust.SetActive(false);
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            _spawnManager.OnBossDestroyed();
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    public void Stop()
    {
        if (transform.position.y <= 5)
        {
            _speed = 0;

            //Player Spoter
            for (var i = 0; i < _turrentFireBig.Length; i++)
            {
                _turrentFireBig[i].SetActive(true);
            }

            for (var i = 0; i < _turrentFireMKOne.Length; i++)
            {
                _turrentFireMKOne[i].SetActive(true);
            }

            for (var i = 0; i < _turrentFireMKFour.Length; i++)
            {
                _turrentFireMKFour[i].SetActive(true);
            }



            //Turrent Colliders
            for (var i = 0; i < _turrentBig.Length; i++)
            {
                _turrentBig[i].GetComponent<PolygonCollider2D>().enabled = true;
            }

            for (var i = 0; i < _turrentMKOne.Length; i++)
            {
                _turrentMKOne[i].GetComponent<PolygonCollider2D>().enabled = true;
            }

            for (var i = 0; i < _turrentMKFour.Length; i++)
            {
                _turrentMKFour[i].GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TurrentFireBig : MonoBehaviour
{

    private float _canFire = 1f;
    private float _fireRate = 1.5f;

    [SerializeField]
    private GameObject _redBullet;

    [SerializeField]
    private bool _isInSight = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Time.time > _canFire)
        {
            ShootPlayer();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _isInSight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isInSight = false;
    }

    void ShootPlayer()
    {
        _fireRate = Random.Range(4f, 3f);
        _canFire = Time.time + _fireRate;

        if (_isInSight == true)
        {
            Instantiate(_redBullet, transform.position, transform.rotation);
        }
    }
}

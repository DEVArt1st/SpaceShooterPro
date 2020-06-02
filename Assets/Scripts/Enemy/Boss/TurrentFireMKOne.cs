using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TurrentFireMKOne : MonoBehaviour
{
    private float _canfire = 1f;
    private float _fireRate;

    [SerializeField]
    private GameObject _laser;

    [SerializeField]
    private bool _isInSight = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Time.time > _canfire)
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
        _canfire = Time.time + _fireRate;

        if (_isInSight == true)
        {
            Instantiate(_laser, transform.position, transform.rotation);
        }
    }
}

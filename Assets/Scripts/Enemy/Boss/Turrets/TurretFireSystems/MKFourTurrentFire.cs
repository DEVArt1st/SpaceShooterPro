using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKFourTurrentFire : MonoBehaviour
{
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _fireCountDown = 0f;
    [SerializeField]
    private bool _isInSight = false;
    [SerializeField]
    private GameObject _mkLaserPrefab;

    private void Update()
    {
        ShootPlayer();
    }

    void ShootPlayer()
    {
        if (_isInSight == true && _fireCountDown <= 0f)
        {
            Instantiate(_mkLaserPrefab, transform.position, transform.rotation);
            _fireCountDown = 1f / _fireRate;
        }
        _fireCountDown -= Time.deltaTime;
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
}

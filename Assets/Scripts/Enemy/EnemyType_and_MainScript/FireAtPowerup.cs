using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAtPowerup : MonoBehaviour
{

    [SerializeField]
    private bool _ispowerUpDetected;
    [SerializeField]
    private GameObject _laserPrefab;

    private float _canFire = 1f;
    private float _fireRate = 3.0f;

    private void Start()
    { 
        _ispowerUpDetected = false;
    }

    private void Update()
    {
        if (Time.time > _canFire)
        {
            DestroyPowerup();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Powerup")
        {
            _ispowerUpDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Powerup")
        {
            _ispowerUpDetected = false;
        }
    }

    void DestroyPowerup()
    {

        _fireRate = 1.5f;
        _canFire = Time.time + _fireRate;

        if (_ispowerUpDetected == true)
        {
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.gameObject.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

}
using UnityEngine;

public class EnemyLaserCannon : MonoBehaviour
{
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _fireCountDown = 0f;
    [SerializeField]
    private GameObject _laserPrefab;

    public void FireLaser()
    {
        if (_fireCountDown <= 0f)
        {
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position + new Vector3(-0.07f, -1.2f, 0f), Quaternion.identity);
            Laser laser = enemyLaser.GetComponentInParent<Laser>();
            laser.AssignEnemyLaser();
            _fireCountDown = 1f / _fireRate;
        }

        _fireCountDown -= Time.deltaTime;
    }

    public void StopFiring()
    {
        _fireRate = 0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    public void MoveUp()
    {

        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }

        if (other.CompareTag("Enemy") && _isEnemyLaser == false)
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Damage();
            }

            Destroy(gameObject);
        }

        if (other.tag == "Powerup" && _isEnemyLaser == true)
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("MKOneTurret"))
        {
            MKOneTurret mkOneTurret = other.GetComponent<MKOneTurret>();

            if (mkOneTurret != null)
            {
                mkOneTurret.Damage(5);
            }

            Destroy(this.gameObject);
        }

        if (other.CompareTag("MKFourTurret"))
        {
            MKFourTurret mkFourTurret = other.GetComponent<MKFourTurret>();

            if (mkFourTurret != null)
            {
                mkFourTurret.Damage(3);
            }

            Destroy(this.gameObject);
        }

        if (other.CompareTag("MainGunTurret"))
        {
            MainGunTurret mainGunTurret = other.GetComponent<MainGunTurret>();

            if (mainGunTurret != null)
            {
                mainGunTurret.Damage(2);
            }

            Destroy(this.gameObject);
        }

        if (other.CompareTag("BossShipMainFrame"))
        {
            MainFrame mainFrame = other.GetComponent<MainFrame>();

            if (mainFrame != null)
            {
                mainFrame.Damage(5);
            }

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("RightDetector") && _isEnemyLaser == false)
        {
            Dodge dodge = other.GetComponent<Dodge>();

            if (dodge != null)
            {
                dodge.PlayerLaserInSight(true);
            }
        }

        if (other.CompareTag("LeftDetector") && _isEnemyLaser == false)
        {
            Dodge dodge = other.GetComponent<Dodge>();

            if (dodge != null)
            {
                dodge.PlayerLaserInSight(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("RightDetector") && _isEnemyLaser == false)
        {
            Dodge dodge = other.GetComponent<Dodge>();

            if (dodge != null)
            {
                dodge.PlayerLaserInSight(false);
            }
        }

        if (other.CompareTag("LeftDetector") && _isEnemyLaser == false)
        {
            Dodge dodge = other.GetComponent<Dodge>();

            if (dodge != null)
            {
                dodge.PlayerLaserInSight(false);
            }
        }
    }
}

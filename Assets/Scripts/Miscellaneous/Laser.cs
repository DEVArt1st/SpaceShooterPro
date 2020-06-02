using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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

        if (other.tag == "Enemy" && _isEnemyLaser == false)
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Damage();
            }

            Destroy(this.gameObject);
        }

        if (other.tag == "Powerup" && _isEnemyLaser == true)
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }

        if (other.tag == "SpaceshipTurrentCenter")
        {
            BossTurrent bossTurrent = other.GetComponent<BossTurrent>();

            if (bossTurrent != null)
            {
                bossTurrent.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "SpaceshipTurrentRight")
        {
            BossTurrent bossTurrent = other.GetComponent<BossTurrent>();

            if (bossTurrent != null)
            {
                bossTurrent.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "SpaceshipTurrentLeft")
        {
            BossTurrent bossTurrent = other.GetComponent<BossTurrent>();

            if (bossTurrent != null)
            {
                bossTurrent.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "MK1LeftOne")
        {
            TurrentMKOne turrentMKOne = other.GetComponent<TurrentMKOne>();

            if (turrentMKOne != null)
            {
                turrentMKOne.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag  == "MK1LeftTwo")
        {
            TurrentMKOne turrentMKOne = other.GetComponent<TurrentMKOne>();

            if (turrentMKOne != null)
            {
                turrentMKOne.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "MK1RightTwo")
        {
            TurrentMKOne turrentMKOne = other.GetComponent<TurrentMKOne>();

            if (turrentMKOne != null)
            {
                turrentMKOne.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "MK1RightOne")
        {
            TurrentMKOne turrentMKOne = other.GetComponent<TurrentMKOne>();

            if (turrentMKOne != null)
            {
                turrentMKOne.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "MK4Left")
        {
            TurrentMKFour turrentMKFour = other.GetComponent<TurrentMKFour>();

            if (turrentMKFour != null)
            {
                turrentMKFour.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "MK4Right")
        {
            TurrentMKFour turrentMKFour = other.GetComponent<TurrentMKFour>();

            if (turrentMKFour != null)
            {
                turrentMKFour.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "BossShipMainFrame")
        {
            MainFrame mainFrame = other.GetComponent<MainFrame>();

            if (mainFrame != null)
            {
                mainFrame.Damage();
            }
            Destroy(this.gameObject);
        }
    }
}

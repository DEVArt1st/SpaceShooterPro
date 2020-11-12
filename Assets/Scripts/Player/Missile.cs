using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private float _rotateSpeed = 10f;
    private GameObject _closetMissilEnemy;
    private GameObject _closetMissilMKOneTurret;
    private GameObject _closetMissilMKFourTurret;
    private GameObject _closetMissilMainGunTurret;
    private Transform _target;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        _closetMissilEnemy = FindClosestEnemy();
        _closetMissilMKOneTurret = FindClosestMKOneTurret();
        _closetMissilMKFourTurret = FindClosestMKFourTurret();
        _closetMissilMainGunTurret = FindClosestMainGunTurret();

        if (_closetMissilEnemy)
        {
            _target = _closetMissilEnemy.transform;
            Vector3 directionToFace = _target.position - transform.position;
            var angle = Mathf.Atan2(directionToFace.y, directionToFace.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Slerp(transform.rotation, (Quaternion.AngleAxis(angle, Vector3.forward)), _rotateSpeed * Time.deltaTime);
        }

        if (_closetMissilMKOneTurret)
        {
            _target = _closetMissilMKOneTurret.transform;
            Vector3 directionToFace = _target.position - transform.position;
            var angle = Mathf.Atan2(directionToFace.y, directionToFace.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Slerp(transform.rotation, (Quaternion.AngleAxis(angle, Vector3.forward)), _rotateSpeed * Time.deltaTime);
        }

        if (_closetMissilMKFourTurret)
        {
            _target = _closetMissilMKFourTurret.transform;
            Vector3 directionToFace = _target.position - transform.position;
            var angle = Mathf.Atan2(directionToFace.y, directionToFace.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Slerp(transform.rotation, (Quaternion.AngleAxis(angle, Vector3.forward)), _rotateSpeed * Time.deltaTime);
        }

        if (_closetMissilMainGunTurret)
        {
            _target = _closetMissilMainGunTurret.transform;
            Vector3 directionToFace = _target.position - transform.position;
            var angle = Mathf.Atan2(directionToFace.y, directionToFace.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Slerp(transform.rotation, (Quaternion.AngleAxis(angle, Vector3.forward)), _rotateSpeed * Time.deltaTime);
        }

        if (transform.position.y > 8)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.x > 12)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.x < -12)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if (other.CompareTag("MKOneTurret"))
        {
            MKOneTurret mkOneTurret = other.GetComponent<MKOneTurret>();

            if (mkOneTurret != null)
            {
                mkOneTurret.Damage(20);
            }
            Destroy(this.gameObject);
        }

        if (other.CompareTag("MKFourTurret"))
        {
            MKFourTurret mkFourTurret = other.GetComponent<MKFourTurret>();

            if (mkFourTurret != null)
            {
                mkFourTurret.Damage(15);
            }
            Destroy(this.gameObject);
        }

        if (other.CompareTag("MainGunTurret"))
        {
            MainGunTurret mainGunTurret = other.GetComponent<MainGunTurret>();

            if (mainGunTurret != null)
            {
                mainGunTurret.Damage(10);
            }
            Destroy(this.gameObject);
        }
    }

   GameObject FindClosestEnemy()
    {
        GameObject[] gameObjectTag;
        gameObjectTag = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closest = null;
        float distance = Mathf.Infinity;

        Vector3 position = transform.position;
        
        foreach (GameObject go in gameObjectTag)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if(curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    GameObject FindClosestMKOneTurret()
    {
        GameObject[] gameObjectTag;
        gameObjectTag = GameObject.FindGameObjectsWithTag("MKOneTurret");

        GameObject closest = null;
        float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        foreach (GameObject go in gameObjectTag)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    GameObject FindClosestMKFourTurret()
    {
        GameObject[] gameObjectTag;
        gameObjectTag = GameObject.FindGameObjectsWithTag("MKFourTurret");

        GameObject closest = null;
        float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        foreach (GameObject go in gameObjectTag)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    GameObject FindClosestMainGunTurret()
    {
        GameObject[] gameObjectTag;
        gameObjectTag = GameObject.FindGameObjectsWithTag("MainGunTurret");

        GameObject closest = null;
        float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        foreach (GameObject go in gameObjectTag)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}


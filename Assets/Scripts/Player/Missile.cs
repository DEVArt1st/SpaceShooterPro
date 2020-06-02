using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private float _rotateSpeed = 10f;
    private GameObject _closetMissile;
    private Transform _target;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        _closetMissile = FindClosestEnemy();

        if (_closetMissile)
        {
            _target = _closetMissile.transform;
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
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
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
}


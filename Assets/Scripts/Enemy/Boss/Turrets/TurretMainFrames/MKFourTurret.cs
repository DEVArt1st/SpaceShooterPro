using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKFourTurret : MonoBehaviour
{

    [SerializeField]
    private int _health = 40;
    [SerializeField]
    private GameObject _explosionPrefab;

    public void Damage(int amount)
    {
        _health -= amount;

        if (_health <= 0)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.GetComponent<Rigidbody2D>());
            Destroy(this.GetComponent<BoxCollider2D>());
            Destroy(this.gameObject, 0.25f);
        }
    }
}

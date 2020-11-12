using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKLaser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    private void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
    }
}

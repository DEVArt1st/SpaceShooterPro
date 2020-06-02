using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKLaser : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4f;

    private bool _isTripleShot;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y <= -8)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
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

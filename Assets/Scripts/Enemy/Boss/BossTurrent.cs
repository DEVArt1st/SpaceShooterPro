using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossTurrent : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerSpot;
    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private float _rotateSpeed = 2f;
    [SerializeField]
    private int _health = 50;

    [SerializeField]
    private Animator _anim;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();

        if (_player == null)
        {
            Debug.LogError("Player is NULL!");
        }

        _anim = GetComponent<Animator>();

        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    }

    private void Update()
    {
        FollowPlayerPosition();
    }

    void FollowPlayerPosition()
    {
        Vector3 directionToFace = _player.position - transform.position;
        var angle = Mathf.Atan2(directionToFace.y, directionToFace.x) * Mathf.Rad2Deg - 270;
        transform.rotation = Quaternion.Slerp(transform.rotation, (Quaternion.AngleAxis(angle, Vector3.forward)), _rotateSpeed * Time.deltaTime);
    }

    public void Damage()
    {
        _health -= 5;

        if (_health == 0)
        {
            _anim.SetTrigger("OnTargetHit");
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(_playerSpot);
            Destroy(this.GetComponent<Rigidbody2D>());
            Destroy(this.GetComponent<PolygonCollider2D>());
            Destroy(this.gameObject, 3f);
            Destroy(_playerSpot.GetComponent<BoxCollider2D>());
        }
    }
}

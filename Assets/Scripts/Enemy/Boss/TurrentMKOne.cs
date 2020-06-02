using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentMKOne : MonoBehaviour
{

    [SerializeField]
    private GameObject _playerSpot;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private float _rotateSpeed = 2f;

    [SerializeField]
    private int _health = 25;

    private Animator _anim;


    private void Start()
    {
        _anim = GetComponent <Animator>();


        _player = GameObject.Find("Player").GetComponent<Transform>();

        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayerPosition();
    }

    void FollowPlayerPosition()
    {
        Vector3 directionToFace = _player.position - transform.position;
        var angle = Mathf.Atan2(directionToFace.y, directionToFace.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Slerp(transform.rotation, (Quaternion.AngleAxis(angle, Vector3.forward)), _rotateSpeed * Time.deltaTime);
    }

    public void Damage()
    {
        _health -= 8;

        if (_health <= 0)
        {
            _anim.SetTrigger("OnTargetHit");
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.GetComponent<Rigidbody2D>());
            Destroy(this.GetComponent<PolygonCollider2D>());
            Destroy(this.gameObject, 3f);
            Destroy(_playerSpot.GetComponent<BoxCollider2D>());
        }
    }
}

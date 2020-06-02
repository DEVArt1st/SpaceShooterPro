using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Transform _parent;
    [SerializeField]
    private bool _isPlayerSeen;
    [SerializeField]
    private float _diffSpeed = 0.5f;


    private void Start()
    {
        _isPlayerSeen = false;
        _target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        DivertCourse();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _isPlayerSeen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _isPlayerSeen = false;
            _diffSpeed = 0f;
        }
    }

    void DivertCourse()
    {
        if (_isPlayerSeen == true && _target != null)
        {
            _parent.transform.position = Vector3.MoveTowards(_parent.transform.position, _target.transform.position, _diffSpeed);
        }
    }
}

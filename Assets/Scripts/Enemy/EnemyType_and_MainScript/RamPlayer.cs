using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private bool _isPlayerSeen;
    [SerializeField]
    private float _diversionSpeed = 2f;


    private void Start()
    {
        _isPlayerSeen = false;
        _target = GameObject.Find("Player").transform;
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
            _diversionSpeed = 0f;
        }
    }

    public void DivertCourse(Transform pos)
    {
        if (_isPlayerSeen == true && _target != null)
        {
            pos.transform.position = Vector3.MoveTowards(pos.transform.position, _target.transform.position, _diversionSpeed * Time.deltaTime);
        }
    }

    public void DisableOnDestroyed()
    {
        this.gameObject.SetActive(false);
    }
}

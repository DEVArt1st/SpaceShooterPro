using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField]
    private float _turnSpeed = 10f;

    private Transform _playerPos;

    private void Start()
    {
        _playerPos = GameObject.Find("Player").GetComponent<Transform>();

        if (_playerPos == null)
        {
            Debug.LogError("Player Tranform is NULL, FIX NEEDED!");
        }
    }

    void Update()
    {

        if (_playerPos != null)
        {
            Vector3 directionToLook = _playerPos.position - transform.position;
            Vector3 rotatedToTarget = Quaternion.Euler(0, 0, transform.rotation.z) * directionToLook;

            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedToTarget);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
        }
    }
}

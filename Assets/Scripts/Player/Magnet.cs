using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using UnityEngine;

public class Magnet : MonoBehaviour
{

    [SerializeField]
    private bool _isInsiteField;

    [SerializeField]
    private float _diffSpeed = 0.02f;

    [SerializeField]
    private GameObject[] _powerups;

    private void Update()
    {
        ActiveMagnet();

        _powerups = GameObject.FindGameObjectsWithTag("Powerup");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Powerup")
        {
            _isInsiteField = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Powerup")
        {
            _isInsiteField = false;
        }
    }

    void ActiveMagnet()
    {
        if (_isInsiteField == true && Input.GetKey(KeyCode.C) && _powerups != null)
        {

            foreach (GameObject go in _powerups) //looks through the array to find a gameobject inside of that array.
            {
                go.transform.position = Vector3.MoveTowards(go.transform.position, transform.position, _diffSpeed); //sucks the object toward the player if "C" is held down.
            }
        }
    }

}
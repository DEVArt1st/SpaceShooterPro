using System.Net.Sockets;
using TMPro.EditorUtilities;
using UnityEditorInternal;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private bool _isPlayerLaserInSight;
    [SerializeField]
    private Transform _parent;

    private void Update()
    {
       AvoidLaser();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            _isPlayerLaserInSight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isPlayerLaserInSight = false;
    }


    //In working progress....
    void AvoidLaser()
    {
        //Fix the movement of enemy so that it doges the laser based off of the side of the map

        if (_isPlayerLaserInSight == true && gameObject.tag == "LeftDetector")
        {
            _parent.transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        else if (_isPlayerLaserInSight == true && gameObject.tag == "RightDetector")
        {
            _parent.transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
    }
}

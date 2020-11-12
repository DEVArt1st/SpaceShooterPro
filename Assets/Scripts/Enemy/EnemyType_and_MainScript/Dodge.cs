using UnityEngine;

public class Dodge : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private bool _isPlayerLaserInSight;

    private void Update()
    {
       AvoidLaser();
    }

    void AvoidLaser()
    {
        Enemy enemy = gameObject.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            if (_isPlayerLaserInSight == true && gameObject.tag == "LeftDetector")
            {
                enemy.DodgeRight();
            }
            else if (_isPlayerLaserInSight == true && gameObject.tag == "RightDetector")
            {
                enemy.DodgeLeft();
            }
        }
    }

    public void PlayerLaserInSight(bool isInSight)
    {
        _isPlayerLaserInSight = isInSight;
    }
}

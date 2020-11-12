using UnityEngine;

public class EnemyTimeBomber : MonoBehaviour
{
    [SerializeField]
    private GameObject _timeBomb;

    [SerializeField]
    private float _deploymentRate = 0.1f;
    private float _deploymentCountDown = 0f;

    public void DeployBomb()
    {
        if (_deploymentCountDown <= 0)
        {
            Instantiate(_timeBomb, transform.position, Quaternion.identity);
            _deploymentCountDown = 1f / _deploymentRate;
        }

        _deploymentCountDown -= Time.deltaTime;
    }

    public void StopDeployment()
    {
        _deploymentRate = 0f;
    }
}

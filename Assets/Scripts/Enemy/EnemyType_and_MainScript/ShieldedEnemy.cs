using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldedEnemy : MonoBehaviour
{
    [SerializeField]
    private int _shieldLife = 1;
    [SerializeField]
    private GameObject _shieldVisualizer;

    private void Start()
    {
        _shieldVisualizer.SetActive(true);
    }

    public void Shield()
    {
        _shieldLife--;

        if (_shieldLife <= 0)
        {
            _shieldVisualizer.SetActive(false);
        }
    }
}

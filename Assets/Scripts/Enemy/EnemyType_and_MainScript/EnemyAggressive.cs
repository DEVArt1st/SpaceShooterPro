using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggressive : MonoBehaviour
{
    private RamPlayer _courseDiverter;

    private void Start()
    {
        _courseDiverter = GetComponentInChildren<RamPlayer>();
    }

    public void RamPlayer()
    {
        _courseDiverter.DivertCourse(this.transform);
    }

    public void Disable()
    {
        _courseDiverter.DisableOnDestroyed();
    }
}

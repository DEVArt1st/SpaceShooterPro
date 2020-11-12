using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private float _rotationSpeed = 30.0f;
    private Animator _anim;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rotate object of the Z axis
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    //check for Laser collisin (tigger)
    //instantiate explosion at the position of the astroid (us)
    //destroy the explosion after 3 seconds

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
            Destroy(other.gameObject);
        }
    }
}

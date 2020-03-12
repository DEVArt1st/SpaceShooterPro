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
        //rotate object of the zed axis!
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    //check for Laser collisin (tigger)
    //instantiate explosion at the position of the astroid (us)
    //destroy the explosion after 3 seconds

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _anim.SetTrigger("OnTargetHit");
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 3f);
            Destroy(other.gameObject);
            Destroy(this.gameObject.GetComponent<CircleCollider2D>());
        }
    }
}

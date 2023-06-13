using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _laserfire = -1f;
    [SerializeField]
    private int __lives = 3;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    
        if (_spawnManager == null)
        {
            Debug.LogWarning("The spawn manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        
      
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _laserfire)
        {
            FireLaser();
        }
    
    }

    void CalculateMovement()
 {
    // Horizontal and Vertical Movement is calculated 
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
    
    // Horizontal Bounds 

    if (transform.position.y >= 0)
    {
        transform.position = new Vector3(transform.position.x, 0, 0);
    }
    else if (transform.position.y <= -3.8f)
    {
        transform.position = new Vector3(transform.position.x, -3.8f, 0);
    }

    // Vertical Bounds

    if (transform.position.x > 11.3f)
    {
        transform.position = new Vector3(-11.3f, transform.position.y, 0);
    }
    else if (transform.position.x <= -11.3f)
    {
        transform.position = new Vector3(11.3f, transform.position.y, 0);
    }
  }
 
    void FireLaser()
    {
        _laserfire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.08f, 0), Quaternion.identity);
   
    }

    public void Damage()
    {
        // Subtract a live by one
        __lives--;
        
        if (__lives < 1)
        {
           _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

}

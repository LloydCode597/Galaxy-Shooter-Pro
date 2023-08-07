using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _laserfire = -1f;
    [SerializeField]
    private int __lives = 3;
    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        // take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, -3, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogWarning("The spawn manager is null");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The uiManager is null");
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
 
        if (_isTripleShotActive == true)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            Instantiate(_laserPrefab, transform.position + new Vector3(-0.78f, -0.25f, 0), Quaternion.identity);
            Instantiate(_laserPrefab, transform.position + new Vector3(0.78f, -0.25f, 0), Quaternion.identity);
        }
 
        //else fire 1 laser
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        // _audioSource.Play();
    }

    public void Damage()
    {
        // if shield is active
        // do nothing....
        // deactivate the shield
        //return;
        if (_isShieldActive == true)  
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        // Subtract a live by one
        __lives--;

        _uiManager.UpdateLives(__lives);
        
        if (__lives < 1)
        {
           _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    
     public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
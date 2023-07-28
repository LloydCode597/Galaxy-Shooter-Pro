using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField] // 0 = Triple shot, 1 = Speed boost, 2 = Shield
    private int powerupID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player!= null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        Debug.Log("TripleShot Active");
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        Debug.Log("SpeedBoost Active");
                        break;
                    case 2:
                        Debug.Log("Colledted Shields");
                        break;
                    default:
                        Debug.Log("Default Values");
                        break;
                }
                
            }

            Destroy(this.gameObject);
        }
    }
}

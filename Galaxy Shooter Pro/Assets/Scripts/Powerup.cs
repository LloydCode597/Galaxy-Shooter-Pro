using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField] // 0 = Triple shot, 1 = Speed boost, 2 = Shield
    private int powerupID;
    [SerializeField]
    private AudioClip _clip;

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

            AudioSource.PlayClipAtPoint(_clip, transform.position);
            
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
                        player.ShieldActive();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;

    public GameObject deathParticle;

    public float bouncePower;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            //Destroy(other.gameObject);

            other.gameObject.SetActive(false);
            
            Instantiate(deathParticle, other.transform.position, other.transform.rotation);

            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, bouncePower, 0f);
        }
    }
}

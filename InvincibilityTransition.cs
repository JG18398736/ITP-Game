using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityTransition : MonoBehaviour
{
    public Sprite playerOriginal;
    public Sprite playerInvincible;

    public float invincibilityTime;

    private SpriteRenderer theSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        theSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Damage")
        {
            theSpriteRenderer.sprite = playerInvincible;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float delayToRespawn;
    public PlayerController thePlayer;

    public GameObject deathParticle;

    public int coinCount;
    public AudioSource coinSound;

    public Text coinText;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite heartFull;
    public Sprite heartEmpty;

    public int maxHealth;
    public int healthCount;

    private bool respawning;

    public SpawnReset[] objectsToReset;

    public bool invincible;
    
    public Text livesText;
    public int startingLives;
    public int currentLives;
    
    public GameObject gameOverScreen;

    public AudioSource backgroundMusic;
    public AudioSource gameOverMusic;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();

        healthCount = maxHealth;

        objectsToReset = FindObjectsOfType<SpawnReset>();

        if(PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount = PlayerPrefs.GetInt("CoinCount");
        }
        
        coinText.text = "Coins: " + coinCount;

         if(PlayerPrefs.HasKey("PlayerLives"))
        {
            currentLives = PlayerPrefs.GetInt("PlayerLives");
        }

        currentLives = startingLives;
        livesText.text = "Lives x" + currentLives;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthCount <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
        }
    }

    //Respawn Function
    public void Respawn()
    {
        currentLives -= 1;
        livesText.text = "Lives x" + currentLives;

        if(currentLives > 0)
        {
            StartCoroutine("RespawnCo");
        }
        else
        {
            thePlayer.gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
            backgroundMusic.Stop();
            gameOverMusic.Play();

        }  
    }

    //Co-Routine(Delay)
    public IEnumerator RespawnCo()
    {
        thePlayer.gameObject.SetActive(false);

        //Death Particle
        Instantiate(deathParticle, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(delayToRespawn);

        healthCount = maxHealth;
        respawning = false;
        UpdateHeartMeter();

        coinCount = 0;
        coinText.text = "Coins: " + coinCount;

        thePlayer.transform.position = thePlayer.respawnPosition;
        thePlayer.gameObject.SetActive(true);

        for(int i = 0; i < objectsToReset.Length; i++)
        {
            
            objectsToReset[i].gameObject.SetActive(true);
            objectsToReset[i].ResetObject();
        }

    }

    public void AddCoins(int coinsToAdd)
    {
        //+= adds the 1st value to the 2nd
        coinCount += coinsToAdd;

        coinText.text = "Coins: " + coinCount;

        coinSound.Play();

    }

    public void HurtPlayer(int damageToTake)
    {
        if(!invincible )
        {
            healthCount -= damageToTake;
            UpdateHeartMeter();

            thePlayer.Knockback();
            thePlayer.hurtSound.Play(); 
        }
    }

    public void UpdateHeartMeter()
    {
        switch(healthCount)
        {
            case 3: 
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                return;
            
            case 1: 
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            case 0: 
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

                default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty; 
                return;
        }
    }

    public void AddLives(int livesToAdd)
    {
        currentLives += livesToAdd;
        livesText.text = "Lives x" + currentLives;
        coinSound.Play();
    }
}

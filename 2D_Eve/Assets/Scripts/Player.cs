using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;

        private int _currentHealth;
        public int curHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = Mathf.Clamp(value, 0, maxHealth); }

        }

        public void Init()
        {
            _currentHealth = maxHealth;
        }
    }

    public PlayerStats stats = new PlayerStats();


    public int fallBoundry = -20;

    public string deathSoundName = "DeathExplosion";

    private AudioManager audioManager;

    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        stats.Init();

        if (statusIndicator == null)
        {
            Debug.LogError("No Status indicator refrenced on the Player");

        }
        else
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No Audio Manager Found In the Scene bee!!");
        }
    }

        void Update()
        {
            if (transform.position.y <= fallBoundry)
            {
                DamagePlayer(9999999);
            }
        }
        public void DamagePlayer(int damage)
        {
            stats.curHealth -= damage;
            if (stats.curHealth <= 0)
            {
            //Play Death Sound.
            audioManager.PlaySound(deathSoundName); 
                GameMaster.KillPlayer(this);
            }

            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);

        }

    }

using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{

    public static GameMaster gm;
    
    [SerializeField]
    private int maxLives = 3;
    private static int _remainingLives;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

   


    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public Transform spawnPrefab;

    public string spawnSoundName = "Spawn";

    public string gameOversound = "GameOver";


    public CameraShake cameraShake;
    [SerializeField]
    private GameObject gameOverUI;


    //Cache
    private AudioManager audioManager;
    


    void Start()
    {
        if (cameraShake == null)
        {
            Debug.LogError("No cameraShake Referenced in GameMaster");
        }
        //Resetting Lives.
        _remainingLives = maxLives;
        //Cache.
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No Audio manager Found in the Scene!!");
        }

    }

    public void EndGame()
    {
        //PlaySound.
        audioManager.PlaySound(gameOversound);  

        Debug.Log("GAME OVER");
        gameOverUI.SetActive(true);
    }

    public IEnumerator _RespawnPlayer()
    {
        //Debug.Log("TODO: Add waiting for spawn sound");
        
        yield return new WaitForSeconds(spawnDelay);
        audioManager.PlaySound(spawnSoundName);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);


    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm._RespawnPlayer());
        }
    }

    public static void KillEnemy(Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }
    public void _KillEnemy(Enemy _enemy)
    {
        //Add Sound.
        audioManager.PlaySound(_enemy.deathSoundName);

        //Add Particles
        Instantiate(_enemy.deathParticle, _enemy.transform.position, Quaternion.identity);
        
        //Add CameraShake.
        cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }
}

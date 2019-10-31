using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    
    public static void KillPlayer(GameObject player)
    {
        Destroy(player.gameObject);
    }

}

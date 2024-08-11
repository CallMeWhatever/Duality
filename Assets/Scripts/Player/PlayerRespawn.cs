using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Health playerHealth;
    private PlayerMovement player;

    private void Awake(){
        playerHealth = GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void Respawn(){
        
        player.Respawn();
        playerHealth.Respawn();
    }
}


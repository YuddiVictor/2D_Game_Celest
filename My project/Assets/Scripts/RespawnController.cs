using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public Transform spawnPoint;  // Referência ao ponto de spawn
    private GameObject player;  // Referência ao jogador

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RespawnPlayer();
    }

    void RespawnPlayer()
    {
        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
            Debug.Log("Player respawned at: " + spawnPoint.position);
        }
        else
        {
            Debug.LogWarning("Player or SpawnPoint not assigned.");
        }
    }

    // Exemplo de uso: chame esta função para respawnar o jogador quando necessário
    public void TriggerRespawn()
    {
        RespawnPlayer();
    }
}

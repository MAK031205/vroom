using UnityEngine;
[RequireComponent(typeof(Collider))]
public class checkpoints : MonoBehaviour
{
    public int checkpointIndex;
    public Transform respawnPoint;
    [SerializeField] public bool disableRespawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            raceManager.instance.checkpointReached(checkpointIndex);
        }
    }
}

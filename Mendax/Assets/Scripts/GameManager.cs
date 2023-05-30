using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject knight;
    public GameObject archer;
    void Awake()
    {

        GameObject player = Instantiate((CheckpointManager.player == 1 ? knight : archer));
        if (CheckpointManager.checkpoint[0] != float.MaxValue)
        {
            
            player.transform.position = new Vector3(CheckpointManager.checkpoint[0], CheckpointManager.checkpoint[1], player.transform.position.z);
        }
        else
        {
            switch (CheckpointManager.level)
            {
                case 2:
                    player.transform.position = new Vector3(-66, -7, -1);
                    break;
                case 4:
                    player.transform.position = new Vector3(145, -50, -1);
                    player.transform.eulerAngles = new Vector3(0, 180, 0);
                    break;
                case 6:
                    player.transform.position = new Vector3(6, -142, -1);
                    break;
                case 8:
                    player.transform.position = new Vector3(-150, -164, -1);
                    player.transform.eulerAngles = new Vector3(0, 180, 0);
                    break;
                case 10:
                    player.transform.position = new Vector3(-553, -169, -1);
                    player.transform.eulerAngles = new Vector3(0, 180, 0);
                    break;
                default:
                    player.transform.position = new Vector3(-20, 5, -1);
                    break;
            }
        }
    }

   
}

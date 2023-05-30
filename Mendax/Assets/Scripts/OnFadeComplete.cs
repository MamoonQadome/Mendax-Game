using UnityEngine;
using UnityEngine.SceneManagement;

public class OnFadeComplete : MonoBehaviour
{
    
    public void FadeComplete() 
    {
        CheckpointManager.checkpoint = new float[] { float.MaxValue };

        if (SceneManager.GetActiveScene().name == "Level0")
        {
            CheckpointManager.level = 2;
            SceneManager.LoadScene(CheckpointManager.level);
        }
        else if (SceneManager.GetActiveScene().name == "Level1")
        {
            CheckpointManager.level = GameObject.Find("Knight(Clone)") ? 3 : 11;
            SceneManager.LoadScene(CheckpointManager.level);
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            CheckpointManager.level = GameObject.Find("Knight(Clone)") ? 5 : 12;
            SceneManager.LoadScene(CheckpointManager.level);
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            CheckpointManager.level = GameObject.Find("Knight(Clone)") ? 7 : 13;
            SceneManager.LoadScene(CheckpointManager.level);
        }
        else if (SceneManager.GetActiveScene().name == "Level4")
        {
            CheckpointManager.level = GameObject.Find("Knight(Clone)") ? 9 : 14;
            SceneManager.LoadScene(CheckpointManager.level);
        }
        else if (SceneManager.GetActiveScene().name.EndsWith("End1"))
        {
            CheckpointManager.level = 4;
            SceneManager.LoadScene(CheckpointManager.level);
        }
        else if (SceneManager.GetActiveScene().name.EndsWith("End2"))
        {
            CheckpointManager.level = 6;
            SceneManager.LoadScene(CheckpointManager.level);
        }
        else if (SceneManager.GetActiveScene().name.EndsWith("End3"))
        {
            CheckpointManager.level = 8;
            SceneManager.LoadScene(CheckpointManager.level);
        }
        else if (SceneManager.GetActiveScene().name.EndsWith("Final"))
        {
            CheckpointManager.level = 10;
            SceneManager.LoadScene(CheckpointManager.level);
        }
        else if (SceneManager.GetActiveScene().name.EndsWith("Fight"))
        {
            CheckpointManager.level = GameObject.Find("Knight(Clone)") ? 15 : 16;
            SceneManager.LoadScene(CheckpointManager.level);
        }
        else if (SceneManager.GetActiveScene().name.EndsWith("Epilogue"))
        {
            CheckpointManager.level = 0;
            SceneManager.LoadScene(CheckpointManager.level);
        }
    }
}

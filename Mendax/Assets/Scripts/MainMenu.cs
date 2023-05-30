using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject selectLevel;
    public GameObject selectPlayer;
    int level;

    public void StartGame(int level=1)
    {

        mainScreen.SetActive(false);
        selectLevel.SetActive(false);
        selectPlayer.SetActive(true);
        this.level = level;
        
    }

    public void SelectLevel()
    {
        mainScreen.SetActive(false);
        selectLevel.SetActive(true);
    }

    public void SelectPlayer(int player)
    {
        CheckpointManager.player = player;
        CheckpointManager.level = level;
        SceneManager.LoadScene(level);
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        selectPlayer.SetActive(false);
        selectLevel.SetActive(false);
        mainScreen.SetActive(true);
    }


}

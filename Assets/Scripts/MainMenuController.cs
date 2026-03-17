using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayClicked()
    {
        Debug.Log("Play clicked!");
        // TODO: SceneManager.LoadScene("GameScene");
    }
    public void OnQuitClicked()
    {
        Debug.Log("Quit clicked!");
        // TODO: Application.Quit();
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayClicked()
    {
        Debug.Log("Play clicked!");
        SceneManager.LoadScene("Combat");
    }
public void OnQuitClicked()
{
    Debug.Log("Quit clicked!");
    
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit(); // Apparently this only works on the compiled version
    #endif
}
}
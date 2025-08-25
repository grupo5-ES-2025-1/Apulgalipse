using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadGame()
    {
        Debug.Log("Load Game ainda n�o implementado");
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Fase1");
    }

    public void OpenConfig()
    {
        Debug.Log("Abrir Configura��es");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

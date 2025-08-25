using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadGame()
    {
        Debug.Log("Load Game ainda não implementado");
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Fase1");
    }

    public void OpenConfig()
    {
        Debug.Log("Abrir Configurações");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

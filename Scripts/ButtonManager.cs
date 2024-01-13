using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject panelNasilOynanir;
    public GameObject panelName;

    public void PlayGame()
    {
        SceneManager.LoadScene("Scene");
    }

    public void OpenPanelNasilOynanir()
    {
        panelNasilOynanir.SetActive(true);
    }

    public void ClosePanelNasilOynanir()
    {
        panelNasilOynanir.SetActive(false);
    }

    public void OpenPanelName()
    {
        panelName.SetActive(true);
    }

    public void ClosePanelName()
    {
        panelName.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}

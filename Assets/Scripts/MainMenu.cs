using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }
}

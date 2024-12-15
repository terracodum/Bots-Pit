using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }
    public void OpenScene(int number)
    {
        SceneManager.LoadScene(number);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

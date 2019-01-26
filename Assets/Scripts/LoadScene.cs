using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}

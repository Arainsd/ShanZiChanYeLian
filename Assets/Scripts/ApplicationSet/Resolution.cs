using UnityEngine;
using UnityEngine.SceneManagement;

public class Resolution : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Screen.SetResolution(1080, 1920, true);
    }
    //返回教学关卡，用于重新开始游戏
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}

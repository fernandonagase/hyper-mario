using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _pausePanel = null;
    private bool _isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        _isPaused = !_isPaused;
        _pausePanel.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0.0f : 1.0f;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

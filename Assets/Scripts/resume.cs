using UnityEngine;
using UnityEngine.UI;


public class resume : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject pauseMenu;
    
    public void resumeFunc()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    
    private void Start()
    {
        var btn = resumeButton.GetComponent<Button>();
        btn.onClick.AddListener(resumeFunc);
        resumeFunc();
    }
}

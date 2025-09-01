using UnityEngine;
using UnityEngine.UI;

public class quitGame : MonoBehaviour
{
    [SerializeField] private Button quitBtn;
    
    public void quit()
    {
        Application.Quit();
    }
    
    private void Start()
    {
        var btn = quitBtn.GetComponent<Button>();
        btn.onClick.AddListener(quit);
        quit();
    }
}

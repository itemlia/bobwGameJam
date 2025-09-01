using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class home : MonoBehaviour
{
    [SerializeField] private Button homeBtn;
    
    public void homeFunc()
    {
        SceneManager.LoadScene("Scenes/titleScreen");    
    }
    
    private void Start()
    {
        var btn = homeBtn.GetComponent<Button>();
        btn.onClick.AddListener(homeFunc);
        homeFunc();
    }
    
}

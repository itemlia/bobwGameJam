using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class uiConInstr : MonoBehaviour
{
    private VisualElement rootElement;
    private UIDocument uiDoc;

    private Button bHome;
    
    private gameManager gameManager;
    

    private void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
        rootElement = uiDoc.rootVisualElement;
        bHome = rootElement.Q<Button>("home");
        
        gameManager = GameObject.FindWithTag("gm").GetComponent<gameManager>();
    }

    private void OnEnable()
    {
        bHome.clicked += homeClicked;
    }
    

    private void homeClicked()
    {
        SceneManager.LoadScene("Scenes/titleScreen");
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class uiConLoseScreen : MonoBehaviour
{
    private VisualElement rootElement;
    private UIDocument uiDoc;

    private Button bRetry;
    private Button bQuit;
    private Button bHome;
    
    private gameManager gameManager;
    

    private void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
        rootElement = uiDoc.rootVisualElement;
        bRetry = rootElement.Q<Button>("b_retry");
        bQuit = rootElement.Q<Button>("b_quit");
        bHome = rootElement.Q<Button>("b_home");
        
        gameManager = GameObject.FindWithTag("gm").GetComponent<gameManager>();
    }

    private void OnEnable()
    {
        bRetry.clicked += retryClicked;
        bQuit.clicked += quitClicked;
        bHome.clicked += homeClicked;
    }

    private void retryClicked()
    {
        for (int i = 0; i < gameManager.scenes.Count; i++)
        {
            if (i  == gameManager.scenes.Count - 2)
            {
                SceneManager.LoadScene(gameManager.scenes[i]);
            }
        }
    }

    private void quitClicked()
    {
        Application.Quit();
    }

    private void homeClicked()
    {
        SceneManager.LoadScene("Scenes/titleScreen");
    }
}

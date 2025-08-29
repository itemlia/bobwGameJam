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
    

    private void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
        rootElement = uiDoc.rootVisualElement;
        bRetry = rootElement.Q<Button>("b_retry");
        bQuit = rootElement.Q<Button>("b_quit");
        bHome = rootElement.Q<Button>("b_home");
    }

    private void OnEnable()
    {
        bRetry.clicked += retryClicked;
        bQuit.clicked += quitClicked;
        bHome.clicked += homeClicked;
    }

    private void retryClicked()
    {
        SceneManager.LoadScene("Scenes/levelOne");
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

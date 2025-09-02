using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class uiConWin : MonoBehaviour
{
    private VisualElement rootElement;
    private UIDocument uiDoc;
    private AudioSource audioSource;

    private Button bQuit;
    private Button bHome;
    

    private void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
        audioSource = GetComponent<AudioSource>();
        rootElement = uiDoc.rootVisualElement;
        bQuit = rootElement.Q<Button>("b_quit");
        bHome = rootElement.Q<Button>("b_home");
    }

    private void OnEnable()
    {
        bQuit.clicked += quitClicked;
        bHome.clicked += homeClicked;
    }

    private void quitClicked()
    {
        audioSource.Play();
        Application.Quit();
    }

    private void homeClicked()
    {
        audioSource.Play();
        SceneManager.LoadScene("Scenes/titleScreen");
    }
}

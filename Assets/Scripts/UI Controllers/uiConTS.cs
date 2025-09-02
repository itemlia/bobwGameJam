using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class uiConTS : MonoBehaviour
{
    private VisualElement rootElement;
    private UIDocument uiDoc;
    private AudioSource audioSource;

    private Button bQuit;
    private Button bShip;
    private Button bAsteroid;
    private Button bInst;

    private void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
        audioSource = GetComponent<AudioSource>();
        rootElement = uiDoc.rootVisualElement;
        bQuit = rootElement.Q<Button>("b_quit");
        bShip = rootElement.Q<Button>("b_ship");
        bAsteroid = rootElement.Q<Button>("b_asteroid");
        bInst = rootElement.Q<Button>("b_instructions");
    }

    private void OnEnable()
    {
        bQuit.clicked += quitClicked;
        bShip.clicked += shipClicked;
        bAsteroid.clicked += asteroidClicked;
        bInst.clicked += instClicked;
    }

    private void quitClicked()
    {
        audioSource.Play();
        Application.Quit();
    }

    private void shipClicked()
    {
        audioSource.Play();
        SceneManager.LoadScene("Scenes/levelOne");
    }

    private void asteroidClicked()
    {
        audioSource.Play();
        SceneManager.LoadScene("Scenes/asteroidLevelOne");
    }

    private void instClicked()
    {
        audioSource.Play();
        SceneManager.LoadScene("Scenes/instructions");
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class uiConTS : MonoBehaviour
{
    private VisualElement rootElement;
    private UIDocument uiDoc;

    private Button bQuit;
    private Button bShip;
    private Button bAsteroid;
    private Button bInst;

    private void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
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
        Application.Quit();
    }

    private void shipClicked()
    {
        SceneManager.LoadScene("Scenes/levelOne");
    }

    private void asteroidClicked()
    {
        SceneManager.LoadScene("Scenes/asteroidLevelOne");
    }

    private void instClicked()
    {
        SceneManager.LoadScene("Scenes/instructions");
    }
}

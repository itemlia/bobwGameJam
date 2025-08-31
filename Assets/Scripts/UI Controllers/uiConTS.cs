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
    private Button bLOne;
    

    private void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
        rootElement = uiDoc.rootVisualElement;
        bQuit = rootElement.Q<Button>("b_quit");
        bLOne = rootElement.Q<Button>("b_level1");
    }

    private void OnEnable()
    {
        bQuit.clicked += quitClicked;
        bLOne.clicked += LOneClicked;
    }

    private void quitClicked()
    {
        Application.Quit();
    }

    private void LOneClicked()
    {
        SceneManager.LoadScene("Scenes/levelOne");
    }
}

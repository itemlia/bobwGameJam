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
    private gameManager gameManager;

    private Button bQuit;
    private Button bHome;
    private Label finalScore;
    

    private void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.FindWithTag("gm").GetComponent<gameManager>();
        rootElement = uiDoc.rootVisualElement;
        bQuit = rootElement.Q<Button>("b_quit");
        bHome = rootElement.Q<Button>("b_home");
        finalScore = rootElement.Q<Label>("score");
    }

    private void OnEnable()
    {
        bQuit.clicked += quitClicked;
        bHome.clicked += homeClicked;
    }

    private void Start()
    {
        finalScore.text = "your score: " + gameManager.pointValue.ToString();
        gameManager.pointValue = 0;
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

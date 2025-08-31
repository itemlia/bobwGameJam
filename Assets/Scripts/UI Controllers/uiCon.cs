using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class uiCon : MonoBehaviour
{
    private UIDocument uiDoc;
    private VisualElement rootElement;
    
    private static ProgressBar healthBar;
    private Label pointScore;

    private void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
        rootElement = uiDoc.rootVisualElement;
        healthBar = rootElement.Q<ProgressBar>("healthBar");
        pointScore = rootElement.Q<Label>("pointCount");
    }

    public static void changeBarVal(float value)
    {
        healthBar.value = value;
        healthBar.title = value.ToString();
    }

    public void setPoints(float points)
    {
        pointScore.text = "score: " + points.ToString();
    }
}

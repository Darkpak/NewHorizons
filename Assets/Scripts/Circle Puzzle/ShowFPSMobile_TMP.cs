using System.Collections;
using TMPro; // Make sure you have this using statement for TextMeshPro
using UnityEngine;
using UnityEngine.UI;

public class ShowFPSMobile_TMP : MonoBehaviour
{
    [Header("Configuration")]
    [Tooltip("The target frame rate for the application.")]
    [Range(30, 120)]
    public int targetFrameRate = 60;

    [Tooltip("How often the FPS text updates (in seconds).")]
    [Range(0.1f, 2f)]
    public float updateFrequency = 1f;

    [Header("UI Reference (Optional)")]
    [Tooltip("Assign your own TextMeshPro object here. If left empty, one will be created automatically.")]
    public TextMeshProUGUI fpsTextComponent;

    // --- Script execution ---

    private void Awake()
    {
        // We use Awake to ensure the UI is ready before other scripts might need it in Start.
        Setup();
    }

    private IEnumerator Start()
    {
        // Apply settings
        Application.targetFrameRate = targetFrameRate;
        QualitySettings.vSyncCount = 0;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        var waitForFrequency = new WaitForSecondsRealtime(updateFrequency);

        // Main loop to update the text
        while (true)
        {
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;

            yield return waitForFrequency;

            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            float currentFps = Mathf.Round(frameCount / timeSpan);
            fpsTextComponent.text = $"FPS: {currentFps}";
        }
    }

    private void Setup()
    {
        // If the text component is already assigned in the inspector, we're done.
        if (fpsTextComponent != null)
        {
            return;
        }

        // --- Automatic UI Creation ---
        // If no text component was assigned, we create everything from scratch.

        // Find or create a Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            // Create a new GameObject for the Canvas
            GameObject canvasObject = new GameObject("Auto-Generated Canvas");
            canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
        }

        // Create the TextMeshPro GameObject
        GameObject textObject = new GameObject("Auto-Generated FPS Text");
        textObject.transform.SetParent(canvas.transform); // Make it a child of the canvas
        textObject.layer = LayerMask.NameToLayer("UI"); // Set the layer to UI

        // Add and configure the TextMeshProUGUI component
        fpsTextComponent = textObject.AddComponent<TextMeshProUGUI>();
        fpsTextComponent.text = "FPS: --";
        fpsTextComponent.fontSize = 24;
        fpsTextComponent.color = Color.green;
        fpsTextComponent.alignment = TextAlignmentOptions.TopRight;

        // Configure the RectTransform for positioning
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(1, 1); // Anchor to top-right corner
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(1, 1);     // Pivot at top-right
        rectTransform.anchoredPosition = new Vector2(-10, -10); // 10px padding from the corner
        rectTransform.sizeDelta = new Vector2(300, 100);       // Give it some space
    }
}

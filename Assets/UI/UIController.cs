using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;

    public VisualElement uiRoot;

    public Button playButton;
    public Button settingsButton;
    public Button exitButton;

    private void Awake()
    {
        uiRoot = GetComponent<UIDocument>().rootVisualElement;
        eventManager.DeactivatePlayerInput();
    }
    private void OnEnable()
    {
        playButton = uiRoot.Q<Button>("StartButton");
        playButton.clicked += OnPlayButtonClicked;

        settingsButton = uiRoot.Q<Button>("SettingsButton");
        settingsButton.clicked += OnSettingsButtonClicked;

        exitButton = uiRoot.Q<Button>("ExitButton");
        exitButton.clicked += QuitToDesktopButtonClicked;
    }

    private void OnDisable()
    {
        playButton.clicked -= OnPlayButtonClicked;
        settingsButton.clicked -= OnSettingsButtonClicked;
        exitButton.clicked -= QuitToDesktopButtonClicked;
    }
    #region UI BUTTON FUNCTIONS
    private void OnPlayButtonClicked()
    {
        Debug.Log("Start Game Button Clicked");
        eventManager.StartGame();
        uiRoot.visible = false;
    }

    private void OnSettingsButtonClicked()
    {
        Debug.Log("Settings Button Clicked");
        eventManager.OpenGameSettings();
    }


    private void QuitToDesktopButtonClicked()
    {
        Application.Quit();
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #endif
    }
    #endregion
}

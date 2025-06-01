using InfinityCode.UltimateEditorEnhancer;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;

    public VisualElement uiRoot;

    public VisualElement mainMenuRoot;

    public Button playButton;
    public Button settingsButton;
    public Button exitButton;

    public VisualElement pauseRoot;

    public Button resumeButton;
    public Button quitToMenuButton;


    private void Awake()
    {
        uiRoot = GetComponent<UIDocument>().rootVisualElement;
        
        GameObject.FindFirstObjectByType<ReferenceHandler>().DeactivatePlayerInput();
    }
    private void Start()
    {
        // Main Menu
        mainMenuRoot = uiRoot.Q<VisualElement>("MainMenuRootElement");
        mainMenuRoot.visible = true;

        playButton = uiRoot.Q<Button>("StartButton");
        playButton.clicked += OnPlayButtonClicked;

        settingsButton = uiRoot.Q<Button>("SettingsButton");
        settingsButton.clicked += OnSettingsButtonClicked;

        exitButton = uiRoot.Q<Button>("ExitButton");
        exitButton.clicked += QuitToDesktopButtonClicked;


        // Pause Menu
        pauseRoot = uiRoot.Q<VisualElement>("PauseRootElement");
        eventManager.onPauseEvent += PauseButtonClicked;
        pauseRoot.visible = false;

        resumeButton = uiRoot.Q<Button>("ResumeButton");
        resumeButton.clicked += ResumeButtonClicked;
        quitToMenuButton = uiRoot.Q<Button>("QuitoMenuButton");
        quitToMenuButton.clicked += QuitToMenuButtonClicked;
    }

    

    private void OnDisable()
    {
        playButton.clicked -= OnPlayButtonClicked;
        settingsButton.clicked -= OnSettingsButtonClicked;
        exitButton.clicked -= QuitToDesktopButtonClicked;
        
        eventManager.onPauseEvent -= PauseButtonClicked;
        resumeButton.clicked -= ResumeButtonClicked;
        quitToMenuButton.clicked -= QuitToMenuButtonClicked;

        Time.timeScale = 1;
    }

    #region UI MAIN MENU FUNCTIONS
    private void OnPlayButtonClicked()
    {
        Debug.Log("Start Game Button Clicked");
        eventManager.StartGame();
        mainMenuRoot.visible = false;
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

    #region UI PAUSE MENU FUNCTIONS
    private void PauseButtonClicked()
    {
        Debug.Log("Pause Game Button Pressed");

        Time.timeScale = 0;
        pauseRoot.visible = true;
    }

    private void ResumeButtonClicked()
    {
        Time.timeScale = 1;
        pauseRoot.visible = false;
    }
    private void QuitToMenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
    
    #endregion
}

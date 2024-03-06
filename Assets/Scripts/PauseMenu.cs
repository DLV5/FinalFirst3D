using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private ActionMap _actionMap;
    private InputAction _pause;

    private bool _isMenuShowing = false;

    private void Awake()
    {

        _actionMap = new ActionMap();

    }

    private void OnEnable()
    {
        _pause = _actionMap.Player.Pause;

        _pause.Enable();

        _pause.performed += OnPause;
    }

    private void OnDisable()
    {
        _pause.Disable();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!_isMenuShowing)
        {
            ShowPauseMenu();
        } else
        {
            HidePauseMenu();
        }
    }

    public void ShowPauseMenu()
    {
        _isMenuShowing = true;
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        _isMenuShowing = false;
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
    }
}

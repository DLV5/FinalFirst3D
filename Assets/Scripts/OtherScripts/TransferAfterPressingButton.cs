using TMPro;
using UnityEngine;

/// <summary>
/// Transfers player to another scene after pressing E if he is in zone
/// </summary>
public class TransferAfterPressingButton : MonoBehaviour
{
    [SerializeField] private string _textToDisplay;
    [SerializeField] private string _sceneToTransfer;

    private TMP_Text _textToShow;

    private SceneFader _sceneFader;

    private bool _isInTransferZone = false;
    // Start is called before the first frame update
    void Start()
    {
        _textToShow = GameObject.Find("TransferText").GetComponent<TMP_Text>();
        _sceneFader = GameObject.Find("SceneFader").GetComponent<SceneFader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInTransferZone)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                _sceneFader.FadeIn(_sceneToTransfer);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        _textToShow.text = _textToDisplay;
        _isInTransferZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;

        _textToShow.text = "";
        _isInTransferZone = false;
    }
}

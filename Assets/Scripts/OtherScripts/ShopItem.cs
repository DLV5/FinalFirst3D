using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private int _expirienceToAdd;

    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _priceButton;

    private int _wasAlreadyBought;
    private static bool _isSceneLoadedForTheFirstTime = true;

    private void OnEnable()
    {
        if (_isSceneLoadedForTheFirstTime)
        {
            _wasAlreadyBought = 0;
        } else
        {
            _wasAlreadyBought = PlayerPrefs.GetInt(gameObject.name, 0);
        }
    }
    
    private void OnDisable()
    {
        _isSceneLoadedForTheFirstTime = false;

        PlayerPrefs.SetInt(gameObject.name, _wasAlreadyBought);
        PlayerPrefs.Save();
    }

    private void Start()
    {
        _priceText.text = _price + "$";

        if (_wasAlreadyBought == 1)
        {
            _priceButton.interactable = false;
        }
    }

    public void BuyItem()
    {
        if (_price > PlayerStats.Money)
            return;

        PlayerStats.Money -= _price;
        PlayerStats.ExpiriencePoints += _expirienceToAdd;

        _wasAlreadyBought = 1;

        _priceButton.interactable = false;
    }
}

using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    [SerializeField] private Stat _statToUpdate;

    /// <summary>
    /// This string displayed before stat value
    /// </summary>
    [SerializeField] private string _whatToDisplayBeforeValue;
    [SerializeField] private TMP_Text _textToUpdate;
    [SerializeField] private string _whatToDisplayAfterValue;


    private void OnEnable()
    {
        switch(_statToUpdate){
            case Stat.Money:
                PlayerStats.OnMoneyChanged += UpdateText;
                UpdateText(PlayerStats.Money);
                break;
            case Stat.Level:
                PlayerStats.OnLevelChanged += UpdateText;
                UpdateText(PlayerStats.Level);
                break;
            case Stat.Expirience:
                PlayerStats.OnExpiriencePointsChanged += UpdateText;
                UpdateText(PlayerStats.ExpiriencePoints);
                break;
        }
    }

    private void OnDisable()
    {
        switch (_statToUpdate)
        {
            case Stat.Money:
                PlayerStats.OnMoneyChanged -= UpdateText;
                break;
            case Stat.Level:
                PlayerStats.OnLevelChanged -= UpdateText;
                break;
            case Stat.Expirience:
                PlayerStats.OnExpiriencePointsChanged -= UpdateText;
                break;
        }
    }

    private void UpdateText(int value)
    {
        _textToUpdate.text = _whatToDisplayBeforeValue + value + _whatToDisplayAfterValue;
    }
}

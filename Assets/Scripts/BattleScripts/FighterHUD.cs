using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Shows health and name of a fighter
/// </summary>
public class FighterHUD : MonoBehaviour
{
    public event Action OnDie;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _healthInNumber;
    [SerializeField] private Slider _healthSlider;

    public void SetHUD(string fighterName, int maxHealth, int currentHealth)
    {
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = currentHealth;

        _healthInNumber.text = currentHealth + "/" + _healthSlider.maxValue;

        _nameText.text = fighterName;
    }
    public void SetHP(int hp)
    {
        _healthSlider.value = hp;
        _healthInNumber.text = hp + "/" + _healthSlider.maxValue;

        if (hp > 0)
            return;
        OnDie?.Invoke();
    }
}

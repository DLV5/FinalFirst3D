using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// It is responsible for the battle UI, such as showing buttons for different behaviours.
/// </summary>
public class BattleUI : MonoBehaviour
{
    public event Action OnBattleUISetUP;
    public event Action OnDefenseClick;
    public event Action OnAttackClick;
    public event Action OnStrongAttackClick;

    [SerializeField] private TMP_Text _startText;

    [Header("Player Turn UI")]
    [SerializeField] private GameObject _battleUI;
    [SerializeField] private Button _defenseButton;
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _strongAttackButton;
    [SerializeField] private int _strongAttackDelay;

    [Header("Enemy Turn UI")]
    [SerializeField] private TMP_Text _enemyTurnText;

    [Header("Win Screen")]
    [SerializeField] private GameObject _winScreen;
    [Header("Dead Screen")]
    [SerializeField] private GameObject _deadScreen;

    private int _strongAttackCounter = 0;//Variable responsible for keeping Strong Attack Button deactevated for some time after it been pressed

    private WaitForSecondsRealtime realSecond = new WaitForSecondsRealtime(1f);
    public void StartCountUntilBattle()
    {
        _winScreen.SetActive(false);
        _deadScreen.SetActive(false);
        StartCoroutine(InitialCounter());

        _defenseButton.onClick.AddListener(() => { OnDefenseClick?.Invoke(); _battleUI.SetActive(false); });
        _attackButton.onClick.AddListener(() => { OnAttackClick?.Invoke(); _battleUI.SetActive(false); });
        _strongAttackButton.onClick.AddListener(() => {
            if (!(_strongAttackCounter <= 0))
                return;
            OnStrongAttackClick?.Invoke();
            _battleUI.SetActive(false); 
            _strongAttackButton.interactable = false;
            _strongAttackCounter = _strongAttackDelay;
        });
    }
    public void PlayerTurn()
    {
        _battleUI.SetActive(true);
        if (_strongAttackCounter != 0)
        {
            _strongAttackCounter--;
            if(_strongAttackCounter < 0)
            {
                _strongAttackCounter = 0;
            }
        }
    }
    public void EnemyTurn(string enemyName)
    {
        _battleUI.SetActive(false);
        _enemyTurnText.text = enemyName + " is attacking.";
    }

    public void ShowWinScreen()
    {
        _winScreen.SetActive(true);
    }
    public void ShowDeadScreen()
    {
        _winScreen.SetActive(false);
    }
    private IEnumerator InitialCounter() //Count until the beginning of the battle 
    {
        _startText.gameObject.SetActive(true);
        _startText.text = "Are you Ready?";
        yield return new WaitForSecondsRealtime(2f);
        _startText.text = "3";
        yield return realSecond;
        _startText.text = "2";
        yield return realSecond;
        _startText.text = "1";
        yield return realSecond;
        _startText.text = "GO!";
        yield return realSecond;
        _startText.gameObject.SetActive(false);

        _battleUI.SetActive(true);
        OnBattleUISetUP?.Invoke();
    }
}

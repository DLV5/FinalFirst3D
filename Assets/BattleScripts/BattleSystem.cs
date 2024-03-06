using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private enum BattleState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Win,
        Lost
    }
    [SerializeField] private BattleUI _battleUI;
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;
    private BattleState _gameState;
    private void Start()
    {
        _gameState = BattleState.Start;

        _battleUI.OnBattleUISetUP += StartBattle;
        _battleUI.StartCountUntilBattle();

        _battleUI.OnDefenseClick += () => {
            if (_gameState != BattleState.PlayerTurn)
                return;
            StartCoroutine(PlayerAttack(AttackBehaviours.defense));
        };

        _battleUI.OnAttackClick += () => {
            if (_gameState != BattleState.PlayerTurn)
                return;
            StartCoroutine(PlayerAttack(AttackBehaviours.attack));
        };

        _battleUI.OnStrongAttackClick += () => {
            if (_gameState != BattleState.PlayerTurn)
                return;
            StartCoroutine(PlayerAttack(AttackBehaviours.strongAttack));
        };

        //Initialization of fighters
        _player.Initialize();
        _enemy.Initialize();
    }
    private void StartBattle()
    {
        _gameState = BattleState.PlayerTurn;
        _battleUI.OnBattleUISetUP -= StartBattle;
        PlayerTurn();
    }
    private void PlayerTurn()
    {
        _battleUI.PlayerTurn();
        Debug.Log("PlayerTurn");
    }
    private void EnemyTurn()
    {
        AttackBehaviours choosedBehavior = _enemy.GetRandomAttackBehaviour();
        _battleUI.EnemyTurn(_enemy.enemyName, choosedBehavior.ToString());
        Debug.Log(choosedBehavior);
        StartCoroutine(EnemyTurn(choosedBehavior));
        Debug.Log("EnemyTurn");
    } 
    private void PowersComparison()
    {

    }
    IEnumerator PlayerAttack(AttackBehaviours playerAttackBehaviour)
    {
        bool isEnemyDead = _player.Attack(playerAttackBehaviour, _enemy);

        //Place for animations
        yield return null;

        if (isEnemyDead)
        {
            _gameState = BattleState.Win;
            EndBattle();
        }
        else
        {
            _gameState = BattleState.EnemyTurn;
            EnemyTurn();
        }
    }
    IEnumerator EnemyTurn(AttackBehaviours enemyAttackBehaviour)
    {

        bool isPlayerDead = _enemy.Attack(enemyAttackBehaviour, _player);

        yield return new WaitForSeconds(1f);
        //Place for animations

        if (isPlayerDead)
        {
            _gameState = BattleState.Lost;
            EndBattle();
        }
        else
        {
            _gameState = BattleState.PlayerTurn;
            PlayerTurn();
        }

    }
    void EndBattle()
    {
        if (_gameState == BattleState.Win)
        {
            _battleUI.ShowWinScreen();
        }
        else if (_gameState == BattleState.Lost)
        {
            _battleUI.ShowDeadScreen();
        }
    }

}

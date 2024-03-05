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
            StartCoroutine(PlayerAttack(AttackBehaviours.Defense));
        };

        _battleUI.OnAttackClick += () => {
            if (_gameState != BattleState.PlayerTurn)
                return;
            StartCoroutine(PlayerAttack(AttackBehaviours.Attack));
        };

        _battleUI.OnStrongAttackClick += () => {
            if (_gameState != BattleState.PlayerTurn)
                return;
            StartCoroutine(PlayerAttack(AttackBehaviours.StrongAttack));
        };
    }
    private void StartBattle()
    {
        _gameState = BattleState.PlayerTurn;
    }
    private void PlayerTurn()
    {
        _gameState = BattleState.PlayerTurn;
        _battleUI.PlayerTurn();
    }
    private void EnemyTurn()
    {
        _gameState = BattleState.PlayerTurn;
        _battleUI.EnemyTurn(_enemy.enemyName);
    }
    IEnumerator PlayerAttack(AttackBehaviours enemyAttackBehaviour)
    {
        bool isEnemyDead = _player.Attack(enemyAttackBehaviour, _enemy);

        yield return new WaitForSeconds(2f);

        if (isEnemyDead)
        {
            _gameState = BattleState.Win;
            EndBattle();
        }
        else
        {
            _gameState = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn(_enemy.GetRandomAttackBehaviour()));
        }
    }
    IEnumerator EnemyTurn(AttackBehaviours enemyAttackBehaviour)
    {
        yield return new WaitForSeconds(1f);

        bool isPlayerDead = _player.Attack(enemyAttackBehaviour, _enemy);

        yield return new WaitForSeconds(1f);

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

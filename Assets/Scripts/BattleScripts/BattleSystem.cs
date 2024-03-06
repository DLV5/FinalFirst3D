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

    private bool _isSomeoneDied = false;

    private void Start()
    {
        _gameState = BattleState.Start;

        _battleUI.OnBattleUISetUP += StartBattle;
        _battleUI.StartCountUntilBattle();

        _battleUI.OnDefenseClick += () => {
            if (_gameState != BattleState.PlayerTurn)
                return;
            StartCoroutine(PlayerAttack(AttackBehaviours.Heal));
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

        //Initialization of fighters
        _player.Initialize();
        _enemy.Initialize();

        _player.OnDie += OnSomeoneDied;
        _enemy.OnDie += OnSomeoneDied;
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
    }

    private void EnemyTurn() => StartCoroutine(EnemyTurn(_enemy.GetRandomAttackBehaviour()));

    private IEnumerator PlayerAttack(AttackBehaviours playerAttackBehaviour)
    {
        _player.Attack(playerAttackBehaviour, _enemy);

        //Place for animations
        yield return null;

        if (!_isSomeoneDied)
        {
            _gameState = BattleState.EnemyTurn;
            EnemyTurn();
        }
    }

    private void OnSomeoneDied()
    {
        //Check if player killed enemy
        if (_gameState == BattleState.PlayerTurn)
        {
            _gameState = BattleState.Win;
        } else
        {
            _gameState = BattleState.Lost;
        }

        _isSomeoneDied = true;

        EndBattle();
    }

    private IEnumerator EnemyTurn(AttackBehaviours enemyAttackBehaviour)
    {
        _enemy.Attack(enemyAttackBehaviour, _player);

        _battleUI.EnemyTurn(_enemy.enemyName, enemyAttackBehaviour.ToString().ToLower());

        yield return new WaitForSeconds(1f);
        //Place for animations

        if (!_isSomeoneDied)
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

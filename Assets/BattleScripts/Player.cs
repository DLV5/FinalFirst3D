using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Defines the key behaviour of a fighter
/// </summary>
public class Player : Fighter
{
    [Header("Battle parameters")]
    [SerializeField] private int _defenseBoost;
    [SerializeField] private int _attackPower;
    [SerializeField] private int _strongAttackPower;

    private FighterHUD _fighterHUD;
    private int _maxHealth;
    private int _currentHealth;

    private int _healthInDefense = 0;

    public override void Initialize(FighterHUD fighterHUD, int maxHealth)
    {
        _fighterHUD = fighterHUD;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
    }
    public override bool Attack(AttackBehaviours attackBehaviour, Fighter enemy)
    {
        bool _isEnemyDead = false;
        Action OnPlayerDie = delegate () { _isEnemyDead = true; };
        enemy.OnDie += OnPlayerDie;

        switch (attackBehaviour)
        {
            case AttackBehaviours.Defense:
                _healthInDefense = _currentHealth + _defenseBoost;
                break;
            case AttackBehaviours.Attack:
                enemy.TakeDamage(_attackPower);
                break;
            case AttackBehaviours.StrongAttack:
                enemy.TakeDamage(_strongAttackPower);
                break;
            default:
                break;
        }
        enemy.OnDie -= OnPlayerDie;
        return _isEnemyDead;
    }

    public override void TakeDamage(int damage) { 
        if(_healthInDefense > 0)
        {
            _healthInDefense -= damage;
            _healthInDefense = 0;
        }
        else
        {
            _currentHealth -= damage;
            _fighterHUD.SetHP(_currentHealth);
        }
    }
}
public enum AttackBehaviours
{
    Defense,
    Attack,
    StrongAttack
}

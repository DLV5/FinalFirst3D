using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    [Header("Additional battle parameters")]
    [SerializeField] private int _strongAttackPower;

    public override void Initialize()
    {
        _currentHealth = _maxHealth;
        _fighterHUD.SetHUD("You", _maxHealth, _currentHealth);
    }
    public override bool Attack(AttackBehaviours attackBehaviour, Fighter enemy)
    {
        bool _isEnemyDead = false;
        Action OnPlayerDie = delegate () { _isEnemyDead = true; };
        enemy.OnDie += OnPlayerDie;

        switch (attackBehaviour)
        {
            case AttackBehaviours.defense:
                _healthInDefense = _defenseBoost;
                break;
            case AttackBehaviours.attack:
                enemy.TakeDamage(_attackPower);
                break;
            case AttackBehaviours.strongAttack:
                enemy.TakeDamage(_strongAttackPower);
                break;
            default:
                break;
        }
        enemy.OnDie -= OnPlayerDie;
        return _isEnemyDead;
    }

    public override void TakeDamage(int damage) {
        if (_healthInDefense > 0)
        {
            _healthInDefense -= damage;
            if (_healthInDefense < 0)
            {
                _currentHealth += _healthInDefense;
            }
        }
        else
        {
            _currentHealth -= damage;
        }
        _fighterHUD.SetHP(_currentHealth);
        if (_currentHealth <= 0)
            OnDie?.Invoke();
    }
}
public enum AttackBehaviours
{
    defense,
    attack,
    strongAttack
}

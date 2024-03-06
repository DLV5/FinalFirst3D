using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    public string enemyName;

    public override void Initialize()
    {
        _currentHealth = _maxHealth;
        _fighterHUD.SetHUD(enemyName, _maxHealth, _currentHealth);
    }

    public override bool Attack(AttackBehaviours attackBehaviour, Fighter player)
    {
        bool _isPlayerDead = false;
        Action OnPlayerDie = delegate () { _isPlayerDead = true; };
        player.OnDie += OnPlayerDie;

        switch (attackBehaviour)
        {
            case AttackBehaviours.defense:
                _healthInDefense = _defenseBoost;
                break;
            case AttackBehaviours.attack:
                player.TakeDamage(_attackPower);
                break;
            default:
                break;
        }
        //player.OnDie -= OnPlayerDie;
        return _isPlayerDead;
    }

    public override void TakeDamage(int damage)
    {
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
        _healthInDefense = 0;
         _fighterHUD.SetHP(_currentHealth);
        if (_currentHealth <= 0)
            OnDie?.Invoke();
    }
    public AttackBehaviours GetRandomAttackBehaviour()
    {
        System.Random rnd = new System.Random();
        int num = rnd.Next(0,2);
        if (num == 0)
            return AttackBehaviours.defense;
        else if (num == 1)
            return AttackBehaviours.attack;
        else
            return default;

    }
}

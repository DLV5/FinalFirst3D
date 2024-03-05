using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    public string enemyName;
    [Header("Battle parameters")]
    [SerializeField] private int _defenseBoost;
    [SerializeField] private int _attackPower;

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

    public override bool Attack(AttackBehaviours attackBehaviour, Fighter player)
    {
        bool _isPlayerDead = false;
        Action OnPlayerDie = delegate () { _isPlayerDead = true; };
        player.OnDie += OnPlayerDie;

        switch (attackBehaviour)
        {
            case AttackBehaviours.Defense:
                _healthInDefense = _currentHealth + _defenseBoost;
                break;
            case AttackBehaviours.Attack:
                player.TakeDamage(_attackPower);
                break;
            default:
                break;
        }
        player.OnDie -= OnPlayerDie;
        return _isPlayerDead;
    }

    public override void TakeDamage(int damage)
    {
        if (_healthInDefense > 0)
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
    public AttackBehaviours GetRandomAttackBehaviour()
    {
        System.Random rnd = new System.Random();
        int num = rnd.Next(0,1);
        if (num == 0)
            return AttackBehaviours.Defense;
        else if (num == 1)
            return AttackBehaviours.Attack;
        else
            return default;

    }
}

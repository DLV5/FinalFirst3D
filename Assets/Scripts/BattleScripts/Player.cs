using System;
using UnityEngine;

public class Player : Fighter
{
    public override void Initialize()
    {
        _maxHealth = PlayerStats.MaxHealth;
        _attackPower = PlayerStats.BaseAttackDamage;

        CurrentHealth = _maxHealth;
        _fighterHUD.SetHUD("You", _maxHealth, CurrentHealth);

        OnHealthChanged += _fighterHUD.SetHP;
    }
    public override bool Attack(AttackBehaviours attackBehaviour, Fighter enemy)
    {
        bool _isEnemyDead = false;
        Action OnPlayerDie = delegate () { _isEnemyDead = true; };
        enemy.OnDie += OnPlayerDie;

        switch (attackBehaviour)
        {
            case AttackBehaviours.Heal:
                CurrentHealth += _healthBoost;
                break;
            case AttackBehaviours.Attack:
                enemy.TakeDamage(_attackPower);
                break;
            case AttackBehaviours.StrongAttack:
                enemy.TakeDamage(_attackPower * 2);
                break;
            default:
                break;
        }
        enemy.OnDie -= OnPlayerDie;
        return _isEnemyDead;
    }

    public override void TakeDamage(int damage) => CurrentHealth -= damage;
}
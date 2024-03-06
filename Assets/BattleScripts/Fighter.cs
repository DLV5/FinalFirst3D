using System;
using UnityEngine;
/// <summary>
/// Defines the key behaviour and fileds of a player
/// </summary>
public abstract class Fighter: MonoBehaviour
{
    [SerializeField] protected FighterHUD _fighterHUD;

    [Header("Battle parameters")]
    [SerializeField] protected int _defenseBoost;
    [SerializeField] protected int _attackPower;

    [Header("Health")]
    [SerializeField] protected private int _maxHealth;
    protected int _currentHealth;
    protected int _healthInDefense = 0;

    public Action OnDie;
    public abstract void Initialize();
    public abstract void TakeDamage(int damage);
    public abstract bool Attack(AttackBehaviours attackBehaviour, Fighter fighter);
}

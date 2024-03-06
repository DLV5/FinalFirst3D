using System;
using UnityEngine;
/// <summary>
/// Defines the key behaviour and fileds of a player
/// </summary>
public abstract class Fighter: MonoBehaviour
{
    public Action<int> OnHealthChanged;
    public Action OnDie;

    [SerializeField] protected FighterHUD _fighterHUD;

    [Header("Battle parameters")]
    [SerializeField] protected int _healthBoost;
    [SerializeField] protected int _attackPower;

    [Header("Health")]
    [SerializeField] protected private int _maxHealth;
    protected int _currentHealth;

    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;

            if (_currentHealth <= 0)
            {
                OnDie?.Invoke();

                OnHealthChanged?.Invoke(0);
            } else
            {
                OnHealthChanged?.Invoke(CurrentHealth);
            }
        }
    }

    public abstract void Initialize();
    public abstract void TakeDamage(int damage);
    public abstract bool Attack(AttackBehaviours attackBehaviour, Fighter fighter);
}

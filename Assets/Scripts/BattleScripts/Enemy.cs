using System;

public class Enemy : Fighter
{
    public string enemyName;

    public override void Initialize()
    {
        CurrentHealth = _maxHealth;
        _fighterHUD.SetHUD(enemyName, _maxHealth, _currentHealth);

        OnHealthChanged += _fighterHUD.SetHP;
    }

    public override bool Attack(AttackBehaviours attackBehaviour, Fighter player)
    {
        bool _isPlayerDead = false;
        Action OnPlayerDie = delegate () { _isPlayerDead = true; };
        player.OnDie += OnPlayerDie;

        switch (attackBehaviour)
        {
            case AttackBehaviours.Heal:
                CurrentHealth += _healthBoost;
                break;
            case AttackBehaviours.Attack:
                player.TakeDamage(_attackPower);
                break;
            default:
                break;
        }
        //player.OnDie -= OnPlayerDie;
        return _isPlayerDead;
    }

    public override void TakeDamage(int damage) => CurrentHealth -= damage;

    public AttackBehaviours GetRandomAttackBehaviour()
    {
        System.Random rnd = new System.Random();
        int num = rnd.Next(0,2);
        if (num == 0)
            return AttackBehaviours.Heal;
        else if (num == 1)
            return AttackBehaviours.Attack;
        else
            return default;

    }
}

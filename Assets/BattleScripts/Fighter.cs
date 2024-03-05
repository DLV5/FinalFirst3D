using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fighter: MonoBehaviour
{
    public event Action OnDie;
    public abstract void Initialize(FighterHUD fighterHUD, int maxHealth);
    public abstract void TakeDamage(int damage);
    public abstract bool Attack(AttackBehaviours attackBehaviour, Fighter fighter);
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonsterBase
{
    public void Init(int id)
    {
        InitMonster(id, "Circle", 100, 5.0f, 5.0f, 50);
    }

    public virtual void Activate()
    {
        
    }

    public virtual void Deactivate(bool _isInit = false)
    {
        if (_isInit == false)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float attackPower, float armorPenetration)
    {
        if (!IsAlive)
            return;

        float validArmor = MonsterData.Armor * (1 - armorPenetration / 100);

        attackPower = attackPower - validArmor;

        MonsterData.CurrentHp -= (int)attackPower;
        Debug.Log($"Monster {MonsterId} Hp Remaining : {MonsterData.CurrentHp}");
        if (MonsterData.CurrentHp <= 0)
        {
            Debug.Log("Monster is dead");
            IsAlive = false;
            //Destroy(this.gameObject);
        }
    }
}

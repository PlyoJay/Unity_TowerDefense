using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonsterBase
{
    public void Init(int id)
    {
        InitMonster(id, "Circle", 100, 1.0f, 50);
    }

    public virtual void Activate()
    {
        
    }

    public virtual void Deactivate(bool _isInit = false)
    {
        if (_isInit == false)
        {

        }
    }
}

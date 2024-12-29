using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonsterBase
{
    public void Init()
    {
        InitMonster(0, "Circle", 100, 2.0f, 50);
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

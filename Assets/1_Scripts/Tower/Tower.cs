using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : TowerBase
{
    public void Init()
    {
        base.InitTower(0, "Tower1", 20, 2.0f, 2.0f, 40f, 1.0f, 50);
    }

    public override void SetRange()
    {
        base.SetRange();
    }

    public override void Start()
    {
        base.Start();

        Init();
        SetRange();
    }
}

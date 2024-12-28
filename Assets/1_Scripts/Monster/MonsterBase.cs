using UnityEngine;

public class MonsterData
{
    public string Type;
    public int TotalHp;
    public int CurrentHp;
    public float Speed;
    public float Reward;
}

public class MonsterBase : MonoBehaviour
{
    public bool IsAlive { get; set; }
    public MonsterData MonsterData { get; private set; }
    public int MonsterId { get; private set; }

    public void InitMonster(int monsterId, string type, int hp, float speed, int reward)
    {
        MonsterId = monsterId;
        MonsterData = new MonsterData()
        {
            Type = type,
            TotalHp = hp,
            CurrentHp = hp,
            Speed = speed,
            Reward = reward
        };
    }
}

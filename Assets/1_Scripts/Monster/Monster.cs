using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Vector2 GetPos => this.transform.position;

    public List<Vector3> pathNodes;
    public float speed = 2f;

    private int currentNodeIndex = 0;

    public virtual void SetInit()
    {
        this.Deactivate(true);
    }

    public virtual void Activate()
    {
        //StartCoroutine(C_OnMove());
    }

    private void Start()
    {
        pathNodes = TileManager.Instance.pathNodes;
    }

    void Update()
    {
       

        if (currentNodeIndex < pathNodes.Count)
        {
            Vector3 targetPosition = pathNodes[currentNodeIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // 노드에 도착하면 다음 노드로 이동
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentNodeIndex++;
            }
        }
    }

    //private IEnumerator C_OnMove()
    //{
    //    this.arrivePos = this.transform.position;

    //    while (true)
    //    {
    //        Vector2 thisPos = this.transform.position;

    //        if (Vector2.Distance(this.arrivePos, thisPos) < .1f)
    //        {
    //            this.isRun = false;

    //            this.arrivePos = Background_Manager.Instance.GetRandPos();
    //        }

    //        Vector2 moveDir = (this.arrivePos - thisPos).normalized;
    //        float monsterMovSpeed = !this.isRun ? DataBase_Manager.Instance.monsterMovSpeed : DataBase_Manager.Instance.monsterRunSpeed;
    //        Vector2 moveVelocity = moveDir * monsterMovSpeed * Time.deltaTime;
    //        this.transform.Translate(moveVelocity);

    //        this.srdr.sortingOrder = Background_Manager.Instance.GetOrderLayer(this.transform);

    //        yield return null;
    //    }
    //}

    public virtual void Deactivate(bool _isInit = false)
    {
        if (_isInit == false)
        {

        }
    }
}

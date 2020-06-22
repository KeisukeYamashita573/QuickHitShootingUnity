using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int _dispTime;      // 表示時間
    protected int _banishTime;    // 消滅時間

    protected int _point;       // 得点

    protected const int _deathTime = 30;     // 死亡してから消えるまでの時間

    void Start()
    {

    }

    void Update()
    {
        if (_dispTime <= 0)
        {
            --_banishTime;
        }
        else
        {
            --_dispTime;
        }
    }

    // 弾と敵が当たったかの判定
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        /// ターゲティングの座標とマウスをクリックしたかの情報が必要
    }

    // 得点取得用
    public int GetPoint
    {
        get { return _point; }
    }

    // 座標取得用
    public Vector2 GetPos
    {
        get { return new Vector2(this.transform.position.x, this.transform.position.y); }
    }
}

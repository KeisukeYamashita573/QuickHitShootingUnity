using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    public NormalEnemy(TargetData tData)
    {
        /// ステータスの初期化
        _dispTime               = tData.dispTime;
        _banishTime             = tData.banishTime;
        this.transform.position = new Vector3(tData.pos.x, tData.pos.y, 0);
    }

    void Start()
    {
        /// 得点の初期化
        _point = 30;
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

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        /// ターゲティングの座標とマウスをクリックしたかの情報が必要
    }
}

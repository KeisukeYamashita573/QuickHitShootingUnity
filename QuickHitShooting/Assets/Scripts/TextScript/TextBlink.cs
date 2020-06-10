/////////////////////////////////////////////////////////////
/// テキスト点滅クラス
/// 制作者：山下圭介
/// 作成日時：2020年6月10日
/// 更新日時：2020年6月10日
/////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    [SerializeField]
    int blinkLate = 30;
    int time = 0;

    void LateUpdate()
    {
        time++;
        if ((time / blinkLate) % 2 == 0)
        {
            this.GetComponent<Text>().enabled = true;
        }
        else
        {
            this.GetComponent<Text>().enabled = false;
        }
    }
}

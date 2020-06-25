using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void OnLeftArrowButton()
    {
        // セレクトマネージャーの取得
        SelectManager sManager = GameObject.FindWithTag("SelectManager").GetComponent<SelectManager>();

        sManager.StageCount = sManager.StageCount - 1;
        sManager.ChangeStage();
    }

    public void OnRightArrowButton()
    {
        // セレクトマネージャーの取得
        SelectManager sManager = GameObject.FindWithTag("SelectManager").GetComponent<SelectManager>();

        sManager.StageCount = sManager.StageCount + 1;
        sManager.ChangeStage();
    }
}

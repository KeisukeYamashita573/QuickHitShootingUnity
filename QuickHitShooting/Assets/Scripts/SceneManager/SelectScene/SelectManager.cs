using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RankData
{
    [Header("プレイヤー名")]
    public Text name;

    [Header("得点")]
    public Text score;
}

public class SelectManager : MonoBehaviour
{
    [SerializeField]
    private RankData[] rankings = new RankData[3];

    [SerializeField]
    private Text stageName;

    [SerializeField]
    private StageLoader stageLoader;

    private int stageCount;           // ステージ数の取得用

    void Start()
    {
        stageCount     = 0;
        ChangeRankData();
    }

    void Update()
    {

    }

    // ステージ情報の更新
    public void ChangeStage()
    {

        if (stageCount > stageLoader.GetStageCnt - 1)
        {
            stageCount = 0;
        }
        else if (stageCount < 0)
        {
            stageCount = stageLoader.GetStageCnt - 1;
        }
        else {}

        stageName.text = "STAGE" + (stageCount + 1).ToString();

        ChangeRankData();
    }

    private void ChangeRankData()
    {
        /// ランキングデータの更新を行う
        for (int i = 0; i < stageLoader.GetStageInfo(stageCount).names.Length; ++i)
        {
            rankings[i].name.text  = (i + 1).ToString() + ". " + stageLoader.GetStageInfo(stageCount).names[i];
            rankings[i].score.text = GetScoreText(stageLoader.GetStageInfo(stageCount).scores[i]);
        }
    }

    private string GetScoreText(int score)
    {
        string str = "";

        int divNum = 1;
        int divCnt = 0;
       /// スコアの桁数を求めている
		while ((score / divNum) >= 1)
        {
            ++divCnt;
            divNum *= 10;
        }

        /// スコアが6桁以下の時、0で埋める処理
        if (divCnt < 6)
        {
            for (int i = 0; i < (6 - divCnt); ++i)
            {
                str += "0";
            }
        }

        if (score > 0)
        {
            str += score.ToString();
        }
        return str;
    }

    // ステージ数を変更するためのプロパティ
    public int StageCount
    {
        get { return stageCount; }
        set { stageCount = value; }
    }

}

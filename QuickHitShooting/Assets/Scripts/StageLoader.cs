using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

struct TargetData
{
    public char type;         // ID
    public int dispTime;      // 出現する時間
    public int banishTime;    // 消えるまでの時間
    public Vector2Int pos;	  // 座標
};

struct StageInfo
{
    // ランキングに登録する得点(配列 : 3個)
    public int[] scores;
    // ランキングに登録する名前(配列 : 3個)
    public string[] names;     
    // ステージデータ(1次元目: ステージ数, 2次元目: 的の数)
    public List<List<TargetData>> stageDatas;     
};

public class StageLoader : MonoBehaviour
{
    private StageInfo stageInfo;

    void Start()
    {
        stageInfo = new StageInfo();
        StageInit();   
    }

    void Update()
    {
        
    }

    private void StageInit()
    {
        /// QuickHitShootingまでのパスを取得している
        string currentDir = Directory.GetCurrentDirectory();

        /// 全ステージのバイナリデータのパスを取得している
        string[] names    = Directory.GetFiles(currentDir, "stage*.bin", SearchOption.AllDirectories);
        foreach (string name in names)
        {
            Load(name);
        }
    }

    private void Load(string path)
    {
    }

    // ステージ数の取得
    public int GetStageCnt
    {
        get { return stageInfo.stageDatas.Count; }
    }

    // ステージデータの取得
    List<TargetData> GetStage(int num)
    {
        return stageInfo.stageDatas[num];
    }
}

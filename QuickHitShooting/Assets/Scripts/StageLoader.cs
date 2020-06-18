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
    public Vector2 pos;	      // 座標
};

struct StageInfo
{
    // ランキングに登録する得点(配列 : 3個)
    public int[] scores;
    // ランキングに登録する名前(配列 : 3個)
    public string[] names;     
    // ステージデータ(1次元目: ウェーブ数, 2次元目: 的の数)
    public List<List<TargetData>> stageData;     
};

public class StageLoader : MonoBehaviour
{
    private List<StageInfo> stageInfos;

    void Start()
    {
        stageInfos = new List<StageInfo>();
        StageInit();   
    }

    void Update()
    {
        int i = 0;
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

    // ステージデータの読み込み
    private void Load(string path)
    {
        using (FileStream fs   = new FileStream(path, FileMode.Open))
        using (BinaryReader br = new BinaryReader(fs))
        {
            StageInfo sInfo = new StageInfo();
            sInfo.scores    = new int[3];
            sInfo.names     = new string[3];
            sInfo.stageData = new List<List<TargetData>>();

            for (int i = 0; i < sInfo.scores.Length; ++i)
            {
                /// ランキングのスコアを読み込む
                sInfo.scores[i] = br.ReadInt32();

                // ランキングの名前を読み込む
               for (int c = 0; c < sInfo.names.Length; ++c)
               {
                   sInfo.names[i] += br.ReadChar();
               }
            }

            int waveCnt, targetCnt;
            TargetData targetData;
            List<TargetData> targetDatas = new List<TargetData>();

            /// ウェーブ情報を読み込むループ回数の読み込み
            waveCnt = br.ReadInt32();
            for (int w = 0; w < waveCnt; ++w)
            {
                /// 的情報を読み込むループ回数の読み込み
                targetCnt = br.ReadInt32();
                /// 的情報の読み込み
                for (int t = 0; t < targetCnt; ++t)
                {
                    targetData.type        = br.ReadChar();
                    targetData.dispTime    = br.ReadInt32();
                    targetData.banishTime  = br.ReadInt32();
                    targetData.pos.x       = br.ReadInt32();
                    targetData.pos.y       = br.ReadInt32();
                    
                    /// 的情報の追加
                    targetDatas.Add(targetData);
                }
                /// ウェーブデータの追加
                sInfo.stageData.Add(targetDatas);
                targetDatas = new List<TargetData>();
            }

            /// ステージ情報の追加
            stageInfos.Add(sInfo);
        }
    }

    // ステージ数の取得
    public int GetStageCnt
    {
        get { return stageInfos.Count; }
    }

    // ステージデータの取得
    List<List<TargetData>> GetStage(int num)
    {
        return stageInfos[num].stageData;
    }
}

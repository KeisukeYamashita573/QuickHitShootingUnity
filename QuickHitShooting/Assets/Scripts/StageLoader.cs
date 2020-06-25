using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TargetData
{
    public char type;         // ID
    public int dispTime;      // 出現する時間
    public int banishTime;    // 消えるまでの時間
    public Vector2 pos;	      // 座標
};

public class StageInfo
{
    // ランキングに登録する得点(配列 : 3個)
    public int[] scores;
    // ランキングに登録する名前(配列 : 3個)
    public string[] names;     
    // ステージデータ(1次元目: ウェーブ数, 2次元目: 的の数)
    public List<List<TargetData>> stageData;
    // ステージのパス取得用(ランキング登録の時使用する)
    public string stagePath;
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
                /// スコアデータを読み込む(4バイト×3)
                sInfo.scores[i] = br.ReadInt32();

               for (int c = 0; c < sInfo.names.Length; ++c)
               {
                   // プレイヤー名を読み込む(1バイト×3)
                   sInfo.names[i] += br.ReadChar();
               }
            }

            /// ウェーブ情報の初期化
            TargetData tData        = new TargetData();
            List<TargetData> tDatas = new List<TargetData>();

            // 読み込むループ回数で使用するもの
            int wCnt, tCnt;

            /// ウェーブ情報を読み込むループ回数の読み込み(4バイト)
            wCnt = br.ReadInt32();
            for (int w = 0; w < wCnt; ++w)
            {
                /// 的情報を読み込むループ回数の読み込み(4バイト)
                tCnt = br.ReadInt32();
                /// 的情報の読み込み
                for (int t = 0; t < tCnt; ++t)
                {
                    /// 的の種類を読み込む(1バイト)
                    tData.type        = (char)br.ReadByte();
                    /// 的の表示時間を読み込む(4バイト)
                    tData.dispTime    = (int)br.ReadUInt32();
                    /// 的の消滅時間を読み込む(4バイト)
                    tData.banishTime  = (int)br.ReadUInt32();
                    /// X座標の読み込み(4バイト)
                    tData.pos.x       = br.ReadInt32();
                    /// Y座標の読み込み(4バイト)
                    tData.pos.y       = br.ReadInt32();

                    /// 的情報の追加
                    tDatas.Add(tData);
                    tData = new TargetData();
                }
                /// ウェーブデータの追加
                sInfo.stageData.Add(tDatas);

                /// ウェーブデータの初期化
                tDatas = new List<TargetData>();
            }
            /// 読み込んだステージパスの登録
            sInfo.stagePath = path;

            /// ステージ情報の追加
            stageInfos.Add(sInfo);
        }
        /* ファイル操作を終了する */
    }

    // ステージデータの書き込み
    public void Save(StageInfo sInfo)
    {
        using (FileStream fs   = new FileStream(sInfo.stagePath, FileMode.Open))
        using (BinaryWriter bw = new BinaryWriter(fs))
        {
            for (int i = 0; i < sInfo.scores.Length; ++i)
            {
                /// スコアデータの書き込み(4バイト×3)
                bw.Write(sInfo.scores[i]);

                foreach (char c in sInfo.names[i])
                {
                    /// プレイヤー名の書き込み(1バイト×3)
                    bw.Write(c);
                }
            }
            int wCnt, tCnt;

            /// ウェーブ数の書き込み(4バイト)
            wCnt = sInfo.stageData.Count;
            bw.Write(wCnt);
            for (int w = 0; w < wCnt; ++w)
            {
                /// 的数の書き込み(4バイト)
                tCnt = sInfo.stageData[w].Count;
                bw.Write(tCnt);

                /// 的情報の書き込み
                for (int t = 0; t < tCnt; ++t)
                {
                    /// 的の種類の書き込み(1バイト)
                    bw.Write(sInfo.stageData[w][t].type);
                    /// 的の出現時間の書き込み(4バイト)
                    bw.Write(sInfo.stageData[w][t].dispTime);
                    /// 的の消滅時間の書き込み(4バイト)
                    bw.Write(sInfo.stageData[w][t].banishTime);
                    /// X座標の書き込み(4バイト)
                    bw.Write((int)sInfo.stageData[w][t].pos.x);
                    /// Y座標の書き込み(4バイト)
                    bw.Write((int)sInfo.stageData[w][t].pos.y);
                }
            }
        }
        /* ファイル操作を終了する */
        Debug.Log("書き込みが正常に終了したよ");
    }

    // ステージ数の取得
    public int GetStageCnt
    {
        get { return stageInfos.Count; }
    }

    // ステージデータの取得
    public StageInfo GetStageInfo(int num)
    {
        return stageInfos[num];
    }
}

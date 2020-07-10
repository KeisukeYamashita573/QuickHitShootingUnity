using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 銃のステータス
[System.Serializable]
public struct GunStatus
{
    [Header("銃の名前")]
    public string name;
    [Header("マガジン内の残り弾数")]
    public int bulletsInMagazine;
    [Header("残り弾数")]
    public int remainingBullets;
    [Header("最大弾数")]
    public int maxBullets;
    [Header("マガジン内の最大弾数")]
    public int maxBulletsInMagazine;
}

public class Gun : MonoBehaviour
{
    [SerializeField]
    GunStatus _gun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 銃のステータス取得用
    public GunStatus GetGunStatus
    {
        get { return _gun; }
    }

    //　銃が選択された時に入る処理
    public void OnClick()
    {
        Debug.Log(_gun.name + " の銃が選択されたよ");
    }
}

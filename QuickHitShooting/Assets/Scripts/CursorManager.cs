/////////////////////////////////////////////////////////////
/// カーソル管理クラス
/// 制作者：山下圭介
/// 作成日時：2020年6月10日
/// 更新日時：2020年6月10日
/////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    public Texture2D testmouse = null;
    [SerializeField]
    int ExtLate = 2;

    // Start is called before the first frame update
    void Start()
    {
        SetCursorTexture();
    }

    public void SetCursorTexture()
    {
        var resizedTexture = new Texture2D(testmouse.width / ExtLate, testmouse.height / ExtLate, TextureFormat.RGBA32, false);
        resizedTexture.alphaIsTransparency = true;
        Graphics.ConvertTexture(testmouse, resizedTexture);
        Vector2 hotspot = resizedTexture.texelSize / 2;
        Cursor.SetCursor(resizedTexture, hotspot, CursorMode.ForceSoftware);
    }
}

/////////////////////////////////////////////////////////////
/// セレクトシーン管理クラス
/// 制作者：山下圭介
/// 作成日時：2020年3月19日
/// 更新日時：2020年6月2日
/////////////////////////////////////////////////////////////
using Deg;

public class SelectSceneManager : IScene
{
    // 現在のシーンネーム
    private string sceneName = "SelectScene";

    public IScene NextScene()
    {
        return new GameSceneManager();
    }

    public IScene BackScene()
    {
        return new TitleSceneManager();
    }

    public IScene NestScene()
    {
        return this;
    }

    // 対応するシーン名取得用関数
    public string GetSceneName()
    {
        return sceneName;
    }
}

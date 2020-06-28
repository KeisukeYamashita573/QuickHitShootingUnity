/////////////////////////////////////////////////////////////
/// シーン管理クラス、シーン読み込みクラス
/// 制作者：山下圭介
/// 作成日時：2020年6月10日
/// 更新日時：2020年6月10日
/////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Deg
{
    // シーン遷移用インターフェース
    public interface IScene
    {
        // 次のシーン遷移用インターフェース
        IScene NextScene();
        // 前のシーン遷移用インターフェース
        IScene BackScene();
        // 入れ子のシーン遷移用インターフェース
        IScene NestScene();
        // 対応するシーン名取得用インターフェース
        string GetSceneName();
    }

    // シーン管理クラス
    public class SceneManager : MonoBehaviour
    {
        // 現在の管理クラスを保持するための変数
        IScene scene;

        private void Start()
        {
            // 初期状態をタイトルシーンからスタートする
            scene = new TitleSceneManager();
            // 最初にこのオブジェクトを遷移時に消えないようにする
            // 自身のオブジェクトがないときだけDontDestroyにする
            if (GameObject.FindGameObjectsWithTag("SceneManager").Length <= 1)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }

        // 次のシーンに遷移する関数
        public void NextScene()
        {
            scene = scene.NextScene();
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.GetSceneName());
        }

        // 前のシーンに遷移する関数
        public void BackScene()
        {
            scene = scene.BackScene();
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.GetSceneName());
        }

        // 現在のシーンに入れ子になってるシーンに遷移する関数
        public void NestScene()
        {
            scene = scene.NestScene();
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.GetSceneName());
        }

        public void LoadNextScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                NextScene();
            }
            else if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                NestScene();
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                BackScene();
            }
#endif
        }
    }
}


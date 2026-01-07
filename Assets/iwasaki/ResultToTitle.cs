using UnityEngine;
using UnityEngine.SceneManagement; // シーン切り替えに必要

public class ResultToTitle : MonoBehaviour
{
    [Header("Settings")]
    public string titleSceneName = "Title"; // タイトルシーンの名前

    void Update()
    {
        // エンターキー（またはテンキーのエンター）が押されたか判定
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ReturnToTitle();
        }
    }

    public void ReturnToTitle()
    {
        // 指定した名前のシーンを読み込む
        SceneManager.LoadScene(titleSceneName);
    }
}
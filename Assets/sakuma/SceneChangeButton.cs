using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("遷移先のシーン名（インスペクターで設定）")]
    public string nextSceneName;

    // ボタンの OnClick にこの関数を登録
    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("シーン名が設定されていません");
        }
    }

}

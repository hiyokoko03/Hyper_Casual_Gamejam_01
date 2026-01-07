using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [Header("遷移先のシーン名")]
    public string nextScene;

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーだけに反応させたい場合
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextScene);
        }
    }

}

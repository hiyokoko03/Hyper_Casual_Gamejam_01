using UnityEngine;
using UnityEngine.SceneManagement; // シーン遷移に必須

public class CollisionSceneChanger : MonoBehaviour
{
    [Header("遷移先のシーン名")]
    public string targetSceneName;

    [Header("特定のタグのみ反応させる（空なら全てに反応）")]
    public string targetTag = "Player";

    // 物理的な衝突（跳ね返る設定など）の場合
    private void OnCollisionEnter(Collision collision)
    {
        CheckCollision(collision.gameObject);
    }

    // トリガー（すり抜ける設定）の場合
    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other.gameObject);
    }

    private void CheckCollision(GameObject hitObject)
    {
        // タグが設定されている場合はタグをチェック、そうでなければ即遷移
        if (string.IsNullOrEmpty(targetTag) || hitObject.CompareTag(targetTag))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
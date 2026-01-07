using UnityEngine;

public class TimeRefillItem : MonoBehaviour
{
    [Header("Settings")]
    public float recoverAmount = 2.0f; // 回復する秒数

    // 当たり判定（Is TriggerがONのColliderが必要）
    void OnTriggerEnter(Collider other)
    {
        // ぶつかった相手が "Player" タグを持っているか確認
        if (other.CompareTag("Player"))
        {
            // シーン内にある TimeGauge スクリプトを探す
            TimeGauge gauge = FindObjectOfType<TimeGauge>();

            // 見つかったら時間を回復させる
            if (gauge != null)
            {
                gauge.AddTime(recoverAmount);
            }

            // 自分自身を破壊して消す
            Destroy(gameObject);
        }
    }
}
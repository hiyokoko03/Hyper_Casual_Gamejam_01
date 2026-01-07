using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeGauge : MonoBehaviour
{
    [Header("Settings")]
    public float maxRewindTime = 5.0f;

    public TextMeshProUGUI timeText;

    private float currentRewindTime;
    private Image gaugeImage;

    void Start()
    {
        gaugeImage = GetComponent<Image>();
        currentRewindTime = maxRewindTime;
    }

    void Update()
    {
        // --- 増減処理 ---
        if (TimeManager.isRewinding)
        {
            currentRewindTime -= Time.deltaTime;
        }
        /*else
        {
            currentRewindTime += Time.deltaTime;
        }*/

        // 範囲制限
        currentRewindTime = Mathf.Clamp(currentRewindTime, 0f, maxRewindTime);

        // --- 表示更新 ---

        // 1. 画像（円グラフ）の更新
        if (gaugeImage != null)
        {
            gaugeImage.fillAmount = currentRewindTime / maxRewindTime;
        }

        // 2. テキスト（数字）の更新
        if (timeText != null)
        {
            timeText.text = currentRewindTime.ToString("F1");
        }
    }

    // ★★★ これが足りていませんでした！ ★★★
    // 外部から時間を回復させるための関数
    public void AddTime(float amount)
    {
        currentRewindTime += amount;

        // 最大値を超えないように制限
        if (currentRewindTime > maxRewindTime)
        {
            currentRewindTime = maxRewindTime;
        }
    }
}
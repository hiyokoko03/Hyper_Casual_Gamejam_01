using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // どこからでも TimeManager.isRewinding でアクセス可能にする
    public static bool isRewinding = false;

    [Header("UI Settings")]
    // ここにさっき作った「RewindEffectPanel」をドラッグ＆ドロップする
    public GameObject rewindEffectPanel;

    void Update()
    {
        // --- 1. キー入力でフラグを切り替え ---
        if (Input.GetKey(KeyCode.Space))
        {
            isRewinding = true;
        }
        else
        {
            isRewinding = false;
        }

        // --- 2. フラグに応じて画面効果をON/OFF ---
        // パネルが設定されている場合のみ実行
        if (rewindEffectPanel != null)
        {
            // isRewinding が true なら表示(true)、false なら非表示(false)にする
            // この1行で状態に合わせて切り替わります
            rewindEffectPanel.SetActive(isRewinding);
        }
    }
}
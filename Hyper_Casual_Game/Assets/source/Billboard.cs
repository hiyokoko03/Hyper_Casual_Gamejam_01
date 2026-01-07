using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        // 常にカメラと同じ向きを向く（＝カメラの方を向く）
        transform.forward = Camera.main.transform.forward;
    }
}
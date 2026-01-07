using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float conveyorSpeed = 3.0f;  // 流れる速度
    public float moveDirectionX  = 1.0f; // 流れる方向（1.0f: 右方向、-1.0f: 左方向）

    private float smooth = 6.0f;
    private float currentSpeed = 0.0f;
    private Transform player;

    

     

    //プレイヤーとの接触判定
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           player = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            currentSpeed = 0.0f;
        }
    }

    void Update()
    {
        // スペースキーの状態をチェック!
        if (Input.GetKeyDown(KeyCode.Space)) moveDirectionX = -1.0f;
        if (Input.GetKeyUp(KeyCode.Space)) moveDirectionX = 1.0f;

        if (player != null)
        {
            // プレイヤーを流す
            currentSpeed = Mathf.Lerp(currentSpeed, conveyorSpeed, smooth * Time.deltaTime * 5.0f);
            player.position += new Vector3(currentSpeed * moveDirectionX * Time.deltaTime, 0, 0);
        }
    }

}

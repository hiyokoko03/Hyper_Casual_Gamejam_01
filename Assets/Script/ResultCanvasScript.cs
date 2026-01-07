using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultCanvasScript : MonoBehaviour
{
    int clickTime = 0;//クリックした回数
    Text scoreText;//クリックした回数をテキストに表示
    GameObject score;
    public static float resultScore; //他のスクリプトから参照できるようにしておきます

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = score.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = resultScore.ToString("0000.00m");
    }

    public void ClickButton(string ButtonName)
    {
        //ゲーム終了
        if(ButtonName == "QUIT")
        {
#if UNITY_EDITOR //UNITYエディターなら 
            UnityEditor.EditorApplication.isPlaying = false;//ゲーム終了
#else //それ以外(ビルド後)なら
        Application.Quit();
#endif
        }
    }

    public void SwitchScene(string scenename) //シーンを切り替える
    {
        SceneManager.LoadScene(scenename);
    }
    public void SaveCount(string KeyName, int value)
    {
        PlayerPrefs.SetInt(KeyName, value);
        Debug.Log("セーブしました。:" + value);
    }

    public int LoadCount(string KeyName)
    {//入力した回数を取得
        Debug.Log(clickTime);
        return PlayerPrefs.GetInt(KeyName);
    }
}

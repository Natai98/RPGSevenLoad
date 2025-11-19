using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private Image pannal;
    [SerializeField] private TMP_InputField nickName;
    public void Gamestart()
    {
        if (nickName.text != null)
        {
            SceneManager.LoadScene("02_Tutorial");
            GameManager.Instance.playerName = nickName.text;
        }
    }

    public void OpenLoginPannal()
    {
        pannal.gameObject.SetActive(true);
    }
}

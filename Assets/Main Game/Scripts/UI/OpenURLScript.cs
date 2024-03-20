using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenURLScript : MonoBehaviour
{
    string urlname;
    public void OpenURL(string url)
    {
        urlname = url;
        Invoke("CallOpenURL", 0.5f);
    }
    void CallOpenURL()
    {
        Application.OpenURL(urlname);
    }

}

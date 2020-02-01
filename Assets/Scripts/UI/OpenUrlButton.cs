using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenUrlButton : MonoBehaviour
{
    public string Url;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(Url));
    }
}
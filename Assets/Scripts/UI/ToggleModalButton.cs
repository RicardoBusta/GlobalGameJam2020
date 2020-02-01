using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleModalButton : MonoBehaviour
{
    [Required] public Canvas ModalCanvas;

    private void Start()
    {
        var modal = ModalCanvas.gameObject;
        modal.SetActive(false);
        GetComponent<Button>().onClick.AddListener(() => modal.SetActive(!modal.activeSelf));
    }
}
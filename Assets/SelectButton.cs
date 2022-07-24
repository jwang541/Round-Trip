using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button button;
    public string sceneName;

    Vector2 initialScale;
    [SerializeField] float targetScale = 1.0f;
    [SerializeField] float scale = 1.0f;

    public AudioSource ads;
    public float pitch;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SwitchScene);

        initialScale = button.gameObject.transform.localScale;
    }

    void Update()
    {
        float dt = Time.deltaTime;
        if (scale < targetScale) {
            scale = Mathf.Min(scale + 10.0f * dt, targetScale);
        } 
        if (scale > targetScale) {
            scale = Mathf.Max(scale - 10.0f * dt, targetScale);
        }
        button.gameObject.transform.localScale = initialScale * scale;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (ads == null) return;
        targetScale = 1.25f;
        ads.pitch = pitch;
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (ads == null) return;
        targetScale = 1.0f;
        ads.pitch = 1.0f;
    }

    void SwitchScene() {
        SceneManager.LoadScene(sceneName);
    }
}

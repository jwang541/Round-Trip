using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScene : MonoBehaviour
{
    Button b;
    public string sceneName;

    void Start()
    {
        b = GetComponent<Button>();
        b.onClick.AddListener(SwitchScene);
    }

    void Update()
    {
        
    }

    void SwitchScene() {
        SceneManager.LoadScene(sceneName);
    }
}

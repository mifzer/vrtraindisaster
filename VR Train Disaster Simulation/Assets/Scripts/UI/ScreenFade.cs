using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenFade : MonoBehaviour
{
    
    [SerializeField] private CanvasGroup _MyCanvasGroup;
    
    public static ScreenFade Instance;

    void Awake(){
        Instance = this;

        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1){
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start(){
        FadeIn();
    }

    // masuk screen
    public void FadeIn(){
        _MyCanvasGroup.DOFade(0, 0.5f);
    }

    // keluar screen
    public void FadeOut(){
        _MyCanvasGroup.DOFade(1, 0.5f);
    }

   
}

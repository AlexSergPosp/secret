using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PopupWindow : MonoBehaviour
{

    [HideInInspector]
    public VisualController controller;

    public bool dimmed = false;
    public bool modal = true;
    public bool fragile = true;

    public bool forbidClosing = false;

    [HideInInspector]
    public bool shown;

    Animator animator;

    public Subject<Unit> shownEvent = new Subject<Unit>();
    public Subject<Unit> closeEvent = new Subject<Unit>();

    void Awake()
    {
        //animator = GetComponent<Animator>();
        var stroke = (Instantiate(UnityEngine.Resources.Load<GameObject>("Prefabs/stroke")) as GameObject)
            .GetComponent<RectTransform>();
        stroke.SetParent(this.GetComponent<RectTransform>());
        stroke.SetSiblingIndex(0);
        stroke.FillTransform();
        float border = 60;
        stroke.offsetMax = new Vector2(border, border);
        stroke.offsetMin = new Vector2(-border, -border);
    }

    public void RunShowAnimation()
    {
        CheckHideCoroutine();
        gameObject.SetActive(true);
        //animator.SetBool("shown", true);
    }

    Coroutine hideCoroutine;

    public void RunHideAnimation()
    {
        CheckHideCoroutine();
        gameObject.SetActive(true);
        hideCoroutine = StartCoroutine(Hide());
    }

    void CheckHideCoroutine()
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }
    }

    IEnumerator Hide()
    {
        //animator.SetBool("shown", false);
        yield return new WaitForSeconds(0.8f);
        gameObject.SetActive(false);
    }

    public void Close()
    {
        if (forbidClosing) return;
        if (controller != null) controller.CloseWindow(this);
    }
}

using UnityEngine;
using Interactors;
using System.Collections;
using System.Collections.Generic;

public class ViewFinisher : MonoBehaviour, IRenderService
{
    [SerializeField]
    private float timeForFinish = 10;

    private UnityImageView imageView;

    private bool isFinishByLose = false;
    private bool isFinishByWin = false;
    private System.Action afterFinish;
    private Coroutine coroutine;


    private List<int> cleanList = new List<int>();

    private void Awake()
    {
        imageView = GetComponent<UnityImageView>();
    }

    public void FinishByWin(System.Action afterFinish)
    {
        if (isFinishByLose || isFinishByWin) return;



        foreach (var view in imageView.GetViews())
        {
            CleanBehaviour cleanBeh = view.Value.GetComponent<CleanBehaviour>();
            if (cleanBeh)
            {
                cleanBeh.SubscribeAfterClean(view.Key,  HideAtNextUpdate);
                cleanBeh.ClearByWin();
            }
        }

        coroutine = StartCoroutine("CloseFinishCutscene");

        isFinishByWin = true;
        this.afterFinish = afterFinish;
    }

    public void FinishByLose(System.Action afterFinish)
    {
        if (isFinishByLose || isFinishByWin) return;

        foreach (var view in imageView.GetViews())
        {
            CleanBehaviour cleanBeh = view.Value.GetComponent<CleanBehaviour>();
            if (cleanBeh)
            {
                cleanBeh.SubscribeAfterClean(view.Key, HideAtNextUpdate);
                cleanBeh.ClearByLose();
            }
        }

        coroutine = StartCoroutine("CloseFinishCutscene");

        isFinishByLose = true;
        this.afterFinish = afterFinish;
    }

    IEnumerator CloseFinishCutscene()
    {
        yield return new WaitForSeconds(timeForFinish);

        ForceFinish();
    }

    public void ForceFinish()
    {
        if (isFinishByLose || isFinishByWin)
        {
            afterFinish();
            StopCoroutine(coroutine);
            isFinishByLose = false;
            isFinishByWin = false;
        }

        imageView.RemoveAll();
    }

    public void HideAtNextUpdate(int id)
    {
        cleanList.Add(id);
    }

    void FixedUpdate()
    {
        if (cleanList.Count > 0)
        {
            int[] ids = cleanList.ToArray();
            cleanList.Clear();
            imageView.Hide(ids);
        }
    }
}

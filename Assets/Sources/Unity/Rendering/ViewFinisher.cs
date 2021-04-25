using UnityEngine;
using UnityEngine.Events;
using Interactors;
using System.Collections;
using System.Collections.Generic;

public class ViewFinisher : MonoBehaviour
{
    [SerializeField]
    private float timeForFinish = 10;

    [SerializeField]
    private UnityEvent afterFinishByWin;
    [SerializeField]
    private UnityEvent afterFinishByLose;

    private UnityImageView imageView;

    private bool isFinishByLose = false;
    private bool isFinishByWin = false;
    private Coroutine coroutine;


    private List<int> cleanList = new List<int>();

    [Zenject.Inject]
    public void Init(UnityImageView unityImageView)
    {
        imageView = unityImageView;
    }

    public void FinishByWin()
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
    }

    public void FinishByLose()
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
            if (isFinishByLose)
                afterFinishByLose.Invoke();
            if (isFinishByWin)
                afterFinishByWin.Invoke();
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

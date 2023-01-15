using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    // static Dictionary<string, Coroutine> debounceList = new Dictionary<string, Coroutine>();
    // public static void Debounce(string key, Action func, float time = 0.15f)
    // {
    //     GameManager gm = GameManager.Instance;
    //     if (!gm) return;

    //     if (debounceList.ContainsKey(key))
    //     {
    //         gm.StopCoroutine(debounceList[key]);
    //         debounceList.Remove(key);
    //     }

    //     Coroutine coroutine = gm.StartCoroutine(StartDebounce(func, time));
    //     debounceList.Add(key, coroutine);
    // }
    // static IEnumerator StartDebounce(Action func, float time)
    // {
    //     yield return new WaitForSeconds(time);
    //     func();
    // }
}

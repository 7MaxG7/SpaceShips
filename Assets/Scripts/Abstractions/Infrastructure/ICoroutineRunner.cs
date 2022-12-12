using System.Collections;
using UnityEngine;


namespace Infrastructure
{
    internal interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(IEnumerator coroutine);
    }
}
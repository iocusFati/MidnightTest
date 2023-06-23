using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public  interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
        public void DoAfter(Func<bool> condition, Action action);
    }
}
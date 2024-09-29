using System;
using System.Collections;
using System.Collections.Generic;
using CoreLib.Utilities;
using UnityEngine;

namespace CoreLib.Complex_Types
{
public class ObjectPoolBacking<T> : IDisposable where T : class
{
    internal readonly List<T> m_List;
    private readonly Func<T> m_CreateFunc;
    private readonly Action<T> m_ActionOnGet;
    private readonly Action<T> m_ActionOnRelease;
    private readonly Action<T> m_ActionOnDestroy;
    private readonly int m_MaxSize;
    internal bool m_CollectionCheck;

    public int CountAll { get; private set; }

    public int CountActive => this.CountAll - this.CountInactive;

    public int CountInactive => this.m_List.Count;

    public ObjectPoolBacking(
      Func<T> createFunc,
      Action<T> actionOnGet = null,
      Action<T> actionOnRelease = null,
      Action<T> actionOnDestroy = null,
      bool collectionCheck = true,
      int defaultCapacity = 10,
      int maxSize = 10000)
    {
      if (createFunc == null)
        throw new ArgumentNullException(nameof (createFunc));
      if (maxSize <= 0)
        throw new ArgumentException("Max Size must be greater than 0", nameof (maxSize));
      this.m_List = new List<T>();
      this.m_CreateFunc = createFunc;
      this.m_MaxSize = maxSize;
      this.m_ActionOnGet = actionOnGet;
      this.m_ActionOnRelease = actionOnRelease;
      this.m_ActionOnDestroy = actionOnDestroy;
      this.m_CollectionCheck = collectionCheck;
      IEnumerator creationCoroutine = RampUp(defaultCapacity);
      CoroutineService.RunCoroutine(creationCoroutine);
    }

    private int initialRampUp = 0100; //per second

    IEnumerator RampUp(int target)
    {
      int counter = 0;
      int amountPerFrame = initialRampUp / 60;
      int framesNeeded = Mathf.CeilToInt(target / (float)amountPerFrame);
      while(counter < framesNeeded)
      {
        CreateInterval(amountPerFrame);
        counter ++;
        yield return null;
      }
    }
    
    private void CreateInterval(int interval)
    {
      for (int i = 0; i < interval; i++)
      {
        var item = m_CreateFunc();
        CountAll++;
        Release(item);
      }
    }
    

    public T Get()
    {
      T obj;
      if (this.m_List.Count == 0)
      {
        obj = this.m_CreateFunc();
        ++this.CountAll;
      }
      else
      {
        int index = this.m_List.Count - 1;
        obj = this.m_List[index];
        this.m_List.RemoveAt(index);
      }
      Action<T> actionOnGet = this.m_ActionOnGet;
      if (actionOnGet != null)
        actionOnGet(obj);
      return obj;
    }

    public PooledObjectBacking<T> Get(out T v) => new PooledObjectBacking<T>(v = this.Get(),  this);

    public void Release(T element)
    {
      
      if (this.m_CollectionCheck && this.m_List.Count > 0)
      {
        for (int index = 0; index < this.m_List.Count; ++index)
        {
          if ((object) element == (object) this.m_List[index])
            throw new InvalidOperationException("Trying to release an object that has already been released to the pool.");
        }
      }
      
      Action<T> actionOnRelease = this.m_ActionOnRelease;
      if (actionOnRelease != null)
        actionOnRelease(element);
      if (this.CountInactive < this.m_MaxSize)
      {
        this.m_List.Add(element);
      }
      else
      {
        Action<T> actionOnDestroy = this.m_ActionOnDestroy;
        if (actionOnDestroy != null)
          actionOnDestroy(element);
      }
    }

    public void Clear()
    {
      if (this.m_ActionOnDestroy != null)
      {
        foreach (T obj in this.m_List)
          this.m_ActionOnDestroy(obj);
      }
      this.m_List.Clear();
      this.CountAll = 0;
    }

    public void Dispose() => this.Clear();
  }
}
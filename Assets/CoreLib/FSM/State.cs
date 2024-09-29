//FSM State class

namespace CoreLib.Complex_Types
{
  public class State<T>
  {
    // The name for the state.
    public string Name { get; set; }

    // The ID of the state.
    public T ID { get; private set; }

    public State(T id)
    {
      ID = id;
    }
    public State(T id, string name) : this(id)
    {
      Name = name;
    }

    public delegate void DelegateNoArg();

    public DelegateNoArg OnEnter;
    public DelegateNoArg OnExit;
    public DelegateNoArg OnUpdate;
    public DelegateNoArg OnFixedUpdate;

    public State(T id,
        DelegateNoArg onEnter,
        DelegateNoArg onExit = null,
        DelegateNoArg onUpdate = null,
        DelegateNoArg onFixedUpdate = null) : this(id)
    {
      OnEnter = onEnter;
      OnExit = onExit;
      OnUpdate = onUpdate;
      OnFixedUpdate = onFixedUpdate;
    }
    public State(T id,
        string name,
        DelegateNoArg onEnter,
        DelegateNoArg onExit = null,
        DelegateNoArg onUpdate = null,
        DelegateNoArg onFixedUpdate = null) : this(id, name)
    {
      OnEnter = onEnter;
      OnExit = onExit;
      OnUpdate = onUpdate;
      OnFixedUpdate = onFixedUpdate;
    }

    virtual public void Enter()
    {
      OnEnter?.Invoke();
    }

    virtual public void Exit()
    {
      OnExit?.Invoke();
    }
    virtual public void Update()
    {
      OnUpdate?.Invoke();
    }

    virtual public void FixedUpdate()
    {
      OnFixedUpdate?.Invoke();
    }
  }
}


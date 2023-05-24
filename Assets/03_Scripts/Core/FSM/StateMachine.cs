public class StateMachine<T> where T : class
{
    private T ownerEntity;
    private State<T> currentState;
    private State<T> previousState;
    private State<T> globalState;

    public void Setup(T owner, State<T> entryState)
    {
        ownerEntity = owner;
        currentState = null;
        previousState = null;
        globalState = null;


        ChangeState(entryState);
    }

    public void Execute()
    {
        if (globalState != null)
        {
            globalState.Execute(ownerEntity);
        }

        if (currentState != null)
        {
            currentState.Execute(ownerEntity);
        }
    }

    public void ChangeState(State<T> newState)
    {

        if (newState == null) return;

        if (currentState != null)
        {

            previousState = currentState;

            currentState.Exit(ownerEntity);
        }


        currentState = newState;
        currentState.Enter(ownerEntity);
    }

    public void SetGlobalState(State<T> newState)
    {
        globalState = newState;
    }

    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    }
}


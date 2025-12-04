using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RaiseEventOnPlayerCount : MonoBehaviour
{
    [Tooltip("How many players need to be active for the event to be raised.")]
    [SerializeField]
    private int TargetCount = 2;

    [Tooltip("Destroys this component the first time the event is raised. This ensures the event isn't raised every frame after the target is hit.")]
    [SerializeField]
    private bool IsDestroySelfOnFirstHit = true;

    [Tooltip("what to do when the event is raised.")]
    [SerializeField]
    private UnityEvent OnHitTarget;

    void Update()
    {
        if (PlayerInput.all.Count >= TargetCount)
        {
            OnHitTarget.Invoke();
            if (IsDestroySelfOnFirstHit)
            {
                Destroy(this);
            }
        }
    }
}

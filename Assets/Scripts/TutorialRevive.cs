using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialRevive : MonoBehaviour
{
    [Tooltip("Hazards or other objects to disable during tutorial.")]
    [SerializeField] private List<GameObject> DisabledDuringTutorial;

    [Tooltip("Flock NPCs already positioned to be revived.")]
    [SerializeField] private List<PlayerHealth> FlockToRevive;

    /// <returns> true only if all members of the flock are revived </returns>
    public bool IsFlockFullyRevived()
    {
        return FlockToRevive.All(f => f.IsAlive);
    }
}

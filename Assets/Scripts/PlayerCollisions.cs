using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] PlayerHealth health;

    private new Rigidbody2D rigidbody
    {
        get
        {
            _rigidbody ??= this.GetComponent<Rigidbody2D>();
            return _rigidbody;
        }
    }
    private Rigidbody2D _rigidbody;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // ressurrect
        {
            if (health.IsDead)
            {
                //Debug.Log(collision.gameObject.name + " helped " + this.name);
                health.Resurrect(0.5f);
            }
        }
        else if (health.IsAlive && collision.gameObject.tag == "Hazard")
        {
            //Debug.Log(collision.gameObject.name + " killed " + this.name);
            health.Kill();
        }
    }
}

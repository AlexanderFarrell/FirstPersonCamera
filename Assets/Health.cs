using UnityEngine;

public class health: MonoBehaviour
{
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            if (value > MaxHealth)
            {
                _health = MaxHealth;
            } else if (value <= 0)
            {
                _health = 0.0f;
                Death.Invoke();
            }
            else
            {
                _health = value;
            }
        }
    }

    private float _health;
    
    public float MaxHealth;

    public delegate void HealthListener();

    public event HealthListener Death;
}
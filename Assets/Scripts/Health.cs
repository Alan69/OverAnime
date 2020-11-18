using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Health : NetworkBehaviour
{
    public int health;
    //public Animator anim;
    //public ThirdPersonController player;

    void Update()
    {
        Die();
    }

    private void Die()
    {
        if (isLocalPlayer)
        {
            if (health <= 0)
            {
                //Создать меню смерти и добавить ragdoll
                //anim.Play("Death");
                Destroy(this.gameObject, 1f);
            }

            if (transform.position.y <= -30)
            {
                health = 0;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (isLocalPlayer) 
            health -= amount;
    }
    
}

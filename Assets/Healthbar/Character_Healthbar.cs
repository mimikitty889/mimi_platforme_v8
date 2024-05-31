using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Healthbar : MonoBehaviour
{
   public int MaxHealth;
   public Healthbar healthbar;

   private int CurHealth;
   
   // Start is called before the first frame update
    void Start()
    {
        CurHealth = MaxHealth;
    }

    public void TakeDamage(int Damage)
    {
        CurHealth -= Damage;
        healthbar.UpdateHealth((float) CurHealth / (float)MaxHealth);
    }
    }

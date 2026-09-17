using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   public int currentHealth = 100;
   public int maxHealth = 100;

   public void ChangeHealth(int amount)
   {
       currentHealth += amount;
       if (currentHealth <= 0)
       {
        //Tijdelijk uitgeschakeld, word later Die
            gameObject.SetActive(false);
       }
   }
}

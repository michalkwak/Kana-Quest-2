using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
    // Reference to the AudioManager instance
    private AudioManager audioManager;

    public Slider slider;
    public EnemyHealthBar healthBar;
    public Animator enemyAnimator;
    public TMP_Text streak;
    
    private int streakCount = 0;

    public GameObject streakFire;

    public int maxHealth;
    public int currentHealth;
    public int minHealth;

    public Button restartButton;
    public Image blocker;

    private void Start()
    {
        // Get the AudioManager instance
        audioManager = AudioManager.Instance;

        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance not found.");
        }
    }

    public void ChangeMaxhealthValue(int health)
    {
        maxHealth = health;
        currentHealth = health;
        SetMaxHealth(health);
        Debug.Log("The health is now: " +  health);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SetHealth(currentHealth);

        audioManager.Play("Sword Slash");

        if (currentHealth <= minHealth)
        {
            EnemyDeath();
            streakCount++;
        }
    }

    public void SetMaxHealth(int health)
    {      
        currentHealth = health;
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        Debug.Log("Enemy health is " + health);

        if (health <= minHealth)
        {
            RestartGame();
        }
    }

    public void EnemyDeath()
    {
        enemyAnimator.SetTrigger("Death");
    }
    public void ResetHealth()
    {
        healthBar.currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
    }

    public void RestartGame()
    {
        restartButton.gameObject.SetActive(true);
        blocker.gameObject.SetActive(true);
        Debug.Log("The button is active");
    }
    public void AddToStreak()
    {
        streak.text = "WIN STREAK: " + streakCount;
    }

    public void ResetStreak()
    {
        streakCount = 0;
        streak.text = "WIN STREAK: " + streakCount;
    }

    public void StreakFire()
    {
        if (streakCount == 3)
        {
            streakFire.SetActive(true);
        }
    }
}


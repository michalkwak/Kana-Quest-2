using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Reference to the AudioManager instance
    private AudioManager audioManager;

    public Slider slider;
    public HealthBar healthBar;
    public Animator animator;
    public TMP_Text streak;

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
        Debug.Log("The health is now: " + health);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        audioManager.Play("Heavy Sword Slash");
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
        Debug.Log("Current health is " + health);

        if (health <= minHealth)
        {
            RestartGame();
            Death();
        }
    }

    public void Death()
    {
        animator.SetTrigger("Death");
    }

    public void ResetHealth()
    {
        SetMaxHealth(maxHealth);
        healthBar.currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
        Debug.Log("Health restored");
    }

    public void RestartGame()
    {
        restartButton.gameObject.SetActive(true);
        blocker.gameObject.SetActive(true);
        Debug.Log("The button is active");
    }

    public void ResetStreak()
    {
        streak.text = "WIN STREAK: " + 0;
    }
}

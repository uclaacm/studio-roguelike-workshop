using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Transform heartContainer;
    [SerializeField] GameObject heartPrefab;

    List<HeartUI> hearts = new();

    float lastHealth = 0;
    float lastMaxHealth = 0;

    void Start(){
        UpdateStats();
    }

    void Update(){
        UpdateStats();
    }

    void UpdateStats(){
        float maxHealth, health;
        if(Player.IsDead) {
            health = 0;
            maxHealth = lastMaxHealth;
        }
        // if player is alive
        else {
            maxHealth = Player.Instance.stats.MaxHealth;
            health = Player.Instance.CurrentHealth;
        }
        if(Mathf.Approximately(maxHealth, lastMaxHealth) && Mathf.Approximately(health, lastHealth)){
            return;
        }

        int deltaHearts = Mathf.CeilToInt(maxHealth / 2) - Mathf.CeilToInt(lastMaxHealth / 2);
        if(deltaHearts > 0){
            for(int i = 0; i < deltaHearts; ++i){
                var heartGO = Instantiate(heartPrefab);
                heartGO.transform.SetParent(heartContainer);
                hearts.Add(heartGO.GetComponent<HeartUI>());
            }
        }
        else if(deltaHearts < 0){
            int toRemove = -deltaHearts;
            for(int i = hearts.Count - toRemove; i < hearts.Count; ++i){
                Destroy(hearts[i].gameObject);
            }
            hearts.RemoveRange(hearts.Count - toRemove, toRemove);
        }

        float healthRemaining = health;
        foreach(var heart in hearts) {
            int heartHealth = Mathf.RoundToInt(Mathf.Clamp(healthRemaining, 0, 2));
            heart.SetHealth(heartHealth);
            healthRemaining -= 2;
        }

        lastMaxHealth = maxHealth;
        lastHealth = health;
    }
}

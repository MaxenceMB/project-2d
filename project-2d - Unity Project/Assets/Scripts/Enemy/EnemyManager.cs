using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    public Enemy enemy;
    public string enemyName;
    public float health;

    public bool hasBeenAlarmed = false;

    private void Start() {
        health = enemy.health;
        spriteRenderer.sprite = enemy.enemySprite;
    }

    public void TakeDamage(float damage){
        health -= damage;
        if (health <= 0){
            Destroy(gameObject);
        }
        StartCoroutine(TakeDamageGFX());
    }

    IEnumerator TakeDamageGFX(){
        spriteRenderer.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(255, 255, 255);
    }

    public IEnumerator NoLongerAlerted(){
        hasBeenAlarmed = true;
        yield return new WaitForSeconds(5f);
        hasBeenAlarmed = false;
    }

}

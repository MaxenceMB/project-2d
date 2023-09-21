using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlertEnemy : MonoBehaviour {

    public SpriteRenderer alertSprite;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && !transform.parent.GetComponent<EnemyManager>().hasBeenAlarmed){
            StartCoroutine(SpawnAlertSign());
            StartCoroutine(transform.parent.GetComponent<EnemyManager>().NoLongerAlerted());
        }
    }

    IEnumerator SpawnAlertSign(){
        alertSprite.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        alertSprite.gameObject.SetActive(false);
    }

}

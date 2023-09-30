using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(menuName = "Weapon/Arrow")]
public class Arrow : ScriptableObject {

    [SerializeField] public string arrowName;

    [SerializeField] public float velocity;
    [SerializeField] public float damage;

}

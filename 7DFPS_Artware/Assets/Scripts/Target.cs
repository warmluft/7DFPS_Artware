using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    bool isAlive;
    [SerializeField] public float health;
    [SerializeField] bool isPlayer;
    [SerializeField] bool isHuman;
    [SerializeField] bool isFakeSoldier;
    [SerializeField] GameObject soldierModel;
    [SerializeField] GameObject dyingDoctor;
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent navMesh;
    [SerializeField] EnemyCombat enemyShootScript;
    [SerializeField] GameObject gunToDrop;
    [SerializeField] GameObject enemyPistolModel;
    [SerializeField] GameObject enemySMGModel;
    [SerializeField] GameObject FakeSoldier;
    Vector3 soldierPosition;


    private void Start()
    {
        isAlive = true;
    }

    private void Update()
    {
        soldierPosition = FakeSoldier.transform.position;
    }

    public void TakeDamage(float amountOfDmg)
    {
        health -= amountOfDmg;

        if (health <= 0 && isPlayer)
        {
            SceneManager.LoadScene(4);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (health <= 0 && !isHuman)
        {
            Destroy(gameObject);
        }

        if (health <= 0 && isHuman && !isFakeSoldier && isAlive)
        {
            animator.SetBool("dead", true);
            navMesh.enabled = false;
            Destroy(enemyShootScript);
            Instantiate(gunToDrop, soldierPosition, Quaternion.identity);
            Destroy(enemyPistolModel);
            Destroy(enemySMGModel);
            isAlive = false;
        }

        if (health <= 0 && isHuman && isFakeSoldier)
        {
            Destroy(soldierModel);
            Instantiate(dyingDoctor, soldierPosition, Quaternion.identity);
            Instantiate(gunToDrop, soldierPosition, Quaternion.identity);
        }
    }
}

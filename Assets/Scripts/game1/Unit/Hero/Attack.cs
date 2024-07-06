using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private float attack = 2f;
    private Coroutine attackCoroutine;
    private GameObject targetGoalObject;
    private Animator heroAnimator;
    private Hero heroComponent;
    public List<GameObject> enemiesInAttack = new List<GameObject>();

    void Start()
    {
        heroComponent = GameObject.Find("hero").GetComponent<Hero>();
        heroAnimator = GameObject.Find("hero").GetComponent<Animator>();
    }

    public void RotateTo(Quaternion targetRotation, float rotationSpeed)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void  PutTargetGoalObject(GameObject curEnemy)
    {
        targetGoalObject = curEnemy;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            enemiesInAttack.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            enemiesInAttack.Remove(collision.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (enemiesInAttack.Contains(targetGoalObject))
            {
                ToAttack();
            }
        }
    }

    void ToAttack()
    {
        heroComponent.busyAnimator = true;
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        var startClip = "";
        var endClip = "";

        if(heroComponent.orientation == "side_right")
        {
            startClip = "attack_side";
            endClip = "idle_side"; 
        }
        if(heroComponent.orientation == "side_left")
        {
            startClip = "attack_side";
            endClip = "idle_side"; 
        }
        if(heroComponent.orientation == "back")
        {
            startClip = "attack_back";
            endClip = "idle_back"; 
        }
        if(heroComponent.orientation == "front")
        {
            startClip = "attack_front";
            endClip = "idle_front"; 
        }
        attackCoroutine = StartCoroutine(WaitAndPlayIdle(startClip, endClip));
        var enemy = targetGoalObject.GetComponent<Enemy>();
        enemy.TakeDamage(attack);
    }

    IEnumerator WaitAndPlayIdle(string startClip, string endClip)
    {
        heroAnimator.Play(startClip);
        float clipLength = TimeClip(startClip);
        if (clipLength > 0)
        {
            yield return new WaitForSeconds(clipLength);
            heroAnimator.Play(endClip);
            heroComponent.busyAnimator = false;
        }
    }

    float TimeClip(string clipName)
    {
        AnimationClip[] clips = heroAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == clipName)
            {
                float playbackSpeed = heroAnimator.GetCurrentAnimatorStateInfo(0).speed;
                return clip.length / playbackSpeed;
            }
        }
        return 0f;
    }
}

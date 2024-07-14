using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : MonoBehaviour
{
    public int id;
    public string orientation;
    public string type;
    public float health;
    public float stamina;
    public float equilibrium;
    public float mana;
    public float damage;
    public float protection;
    public float animationSpeed; //сомнительно, но окей
    public float attackSpeed;
    public float runningSpeed;
    public float level;

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;


    protected float TimeClip(string clipName)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == clipName)
            {
                float playSpeed = animator.GetCurrentAnimatorStateInfo(0).speed;
                return clip.length / playSpeed;
            }
        }
        return 0f;
    }

    protected IEnumerator UpdatePositionZ()
    {
        while (true)
        {
            Vector3 position = transform.position;
            position.z = position.y * 0.01f;
            transform.position = position;
            yield return null; // Ждем следующий кадр перед продолжением
        }
    }
}

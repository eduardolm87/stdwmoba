using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public Collider Collider;
    public Rigidbody Rigidbody;
    public MeshRenderer MeshRenderer;


    [HideInInspector]
    public Creature Owner = null;



    public static Hitbox CreateHitbox(Vector3 Position)
    {
        GameObject go = GameObject.Instantiate(GameManager.Instance.HitboxPrefab.gameObject, Position, Quaternion.identity) as GameObject;

        Hitbox hbx = go.GetComponent<Hitbox>();

        hbx.MeshRenderer.enabled = GameManager.Instance.DebugHitboxes;


        return hbx;
    }


    public void DestroyAfterSeconds(float time)
    {
        Invoke("DestroyAfterTimeInvoked", time);
    }

    void DestroyAfterTimeInvoked()
    {
        Destroy(gameObject);
    }


}

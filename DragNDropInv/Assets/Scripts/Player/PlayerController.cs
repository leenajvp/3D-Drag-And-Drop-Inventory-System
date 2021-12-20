using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 2f;
    Animator animState;
    public bool isGrounded = true;
    public Inventory inventory;
    private static readonly int AnimState = Animator.StringToHash("AnimState"); 

    public bool helmetCollected;

    void Start()
    {
        animState = GetComponent<Animator>();
        animState.SetInteger(AnimState, 0);

        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();

            if (!inventory)
            {
                Debug.LogError("Inventory does not exist");
            }
        }
    }

    void Update()
    {
        Forward();
        Backward();
        Animations();
        CollectItem();
    }

    public void Forward()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    public void Backward()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
    }

    private void Animations()
    {
        if (!Input.anyKey)
        {
            animState.SetInteger(AnimState, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            animState.SetInteger(AnimState, 1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            animState.SetInteger(AnimState, 2);
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            animState.SetInteger(AnimState, 1);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.A))
            {
                animState.SetInteger(AnimState, 2);
            }

            if (Input.GetKey(KeyCode.D))
            {
                animState.SetInteger(AnimState, 1);
            }
            
            if (!Input.anyKey)
            {
                animState.SetInteger(AnimState, 0);
            }
        }
    }

    public void CollectItem()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);

        foreach (var col in colliders)
        {
            ICollectable collectableItem = col.GetComponent<ICollectable>();

            if (collectableItem != null && collectableItem.isAvailable == true)
            {
                inventory.AddItem(collectableItem);
            }
        }
    }
}


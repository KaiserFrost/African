using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class PController : MonoBehaviour {


    public Texture mira;
    Collider other;
    private CharacterController _characterController;
    public Animator animator;
    public AnimatorClipInfo[] clip;
    bool noChao = true;

    public bool ataque1;
    public bool ataque2;
    public bool defesa;
    public bool rola;

    public float velocidade = 100.0f;

    public float velocidadeRotacao = 100.0F;

    //Cena pa mandar merdas
    public float tempMaxReload;
    public float delay;
    public GameObject spear;
    public Transform posLancamento;
    float tmpCorrentReload;
    float tmpCorrentDelay;
    bool disparar;

    public float pickUpDistance = 10f;

    private InventoryItemBase mCurrentItem = null;

    private SistemaStatus healthBar;
    private SistemaStatus hungryBar;
    private SistemaStatus thirstBar;

    private int startHealth;
    private int startHungry;
    private int startFood;

    public Inventory Inventory;
    public GameObject Hand;
    public HUD Hud;

    void Start()
    {
        animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        Inventory.ItemUsed += Inventory_ItemUsed;
        Inventory.ItemRemoved += Inventory_ItemRemoved;
    }

    #region Inventory

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        InventoryItemBase item = e.Item;

        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);
        goItem.transform.parent = null;

    }

    private void SetItemActive(InventoryItemBase item, bool active)
    {
        GameObject currentItem = (item as MonoBehaviour).gameObject;
        currentItem.SetActive(active);
        currentItem.transform.parent = active ? Hand.transform : null;
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        if (e.Item.ItemType != EItemType.Consumable)
        {
            // If the player carries an item, un-use it (remove from player's hand)
            if (mCurrentItem != null)
            {
                SetItemActive(mCurrentItem, false);
            }

            InventoryItemBase item = e.Item;

            // Use item (put it to hand of the player)
            SetItemActive(item, true);

            mCurrentItem = e.Item;
        }

    }

    //private void SetItemActive(InventoryItemBase item, bool active)
    //{
    //    GameObject currentItem = (item as MonoBehaviour).gameObject;
    //    currentItem.SetActive(active);
    //    currentItem.transform.parent = active ? Hand.transform : null;
    //}

    //private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    //{
    //    if (e.Item.ItemType != EItemType.Consumable)
    //    {
    //        // If the player carries an item, un-use it (remove from player's hand)
    //        if (mCurrentItem != null)
    //        {
    //            SetItemActive(mCurrentItem, false);
    //        }

    //        InventoryItemBase item = e.Item;

    //        // Use item (put it to hand of the player)
    //        SetItemActive(item, true);

    //        mCurrentItem = e.Item;
    //    }

    //}


    private void DropCurrentItem()
    {
       

        GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;

        Inventory.RemoveItem(mCurrentItem);

        // Throw animation
        Rigidbody rbItem = goItem.AddComponent<Rigidbody>();
        if (rbItem != null)
        {
            rbItem.AddForce(transform.forward * 2.0f, ForceMode.Impulse);

            Invoke("DoDropItem", 0.25f);
        }

    }

    public void DoDropItem()
    {

        // Remove Rigidbody
        Destroy((mCurrentItem as MonoBehaviour).GetComponent<Rigidbody>());

        mCurrentItem = null;
    }

    #endregion


    void FixedUpdate()
    {
        
            // Drop item
            if (mCurrentItem != null && Input.GetKeyDown(KeyCode.R))
            {
                DropCurrentItem();
            }
        
    }


    // Update is called once per frame
    void Update()
    {
      
        Ataque1();
        Ataque2();
        Defesa();
        Esquivar();
        Lancar();

        if (Input.GetAxis("Vertical") > 0)
        {
            animator.SetBool("isWalking", true);
           transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        animator.SetBool("isRunning", Input.GetButton("Run"));
        animator.SetFloat("direction", Input.GetAxis("Horizontal"));

        if (animator.GetBool("isWalking") || animator.GetBool("isRunning"))
        {
  

        }

        if (Input.GetKeyDown(KeyCode.Mouse3))
        {
            Lancar();
        }

        UpdateParametrosAnimator();

        // Interact with the item

        Ray ray = Camera.main.ScreenPointToRay(Camera.main.transform.position);
        RaycastHit hit;
        //if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickUpDistance))
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickUpDistance))

        {
            InteractableItemBase item = hit.transform.gameObject.GetComponent<InteractableItemBase>();

            Debug.Log("ray hit (name): " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag == "Item")
            {
                if (item != null)
                {
                    mInteractItem = item;

                    Hud.OpenMessagePanel(mInteractItem);
                }

             //   Debug.Log("ray hit1 (name): " + hit.collider.gameObject.name);
            }
            else
            {
                Hud.CloseMessagePanel();
                mInteractItem = null;
            }
          
        }

        if (mInteractItem != null && Input.GetKeyDown(KeyCode.F))
            {
                // Common interact method
                mInteractItem.OnInteract();
           

            // TODO: Check to move this logic to a better location
            if (mInteractItem is InventoryItemBase)
            {
                Inventory.AddItem(mInteractItem as InventoryItemBase);
                (mInteractItem as InventoryItemBase).OnPickup();
                
            }

            Hud.CloseMessagePanel();

            mInteractItem = null;
        }

        // Execute action with item
        if (mCurrentItem != null && Input.GetMouseButtonDown(0))
        {
            // Dont execute click if mouse pointer is over uGUI element
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // TODO: Logic which action to execute has to come from the particular item
                //animator.SetTrigger("attack_1");
            }
        }

    }

    private InteractableItemBase mInteractItem = null;
//DESCOMENTADO
/*
     private void OnTriggerEnter(Collider other)
      {
            InteractableItemBase item = other.gameObject.GetComponent<InteractableItemBase>();
              if (item != null)
              {
                  mInteractItem = item;

                  Hud.OpenMessagePanel(mInteractItem);
              }

      }

    private void OnTriggerExit(Collider other)
    {
        InteractableItemBase item = other.GetComponent<InteractableItemBase>();
        if (item != null)
        {
            Hud.CloseMessagePanel();
            mInteractItem = null;
        }
    }
    */

    void UpdateParametrosAnimator(){
            animator.SetBool("z_noChao", noChao);


            animator.SetBool("Hit1", ataque1);
            animator.SetBool("Hit2", ataque2);
            animator.SetBool("isBlock", defesa);
            animator.SetBool("isCambalhota", rola);

            animator.SetFloat("z_velocidade", velocidade);
        }

    #region Movimentos/Combos
    void Ataque2()
    {
        ataque2 = false;
        clip = animator.GetCurrentAnimatorClipInfo(0);
        if (ataque2)
            ataque1 = false;


        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (noChao == true)
            {
                ataque1 = false;
                ataque2 = true;
                defesa = false;
            }

        }

    }

    void Ataque1() {
        ataque1 = false;
        clip = animator.GetCurrentAnimatorClipInfo(0);
        if (ataque1)
           ataque2 = false;

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    if (noChao)
        //    {
        //        ataque1 = false;
        //        ataque2 = true;
        //        defesa = false;
        //    }
           
        //}

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (noChao)
            {
                
                Debug.Log(noChao);
                ataque1 = true;
                ataque2 = false;
                defesa = false;

                clip = animator.GetCurrentAnimatorClipInfo(0);
                if (clip[0].clip.name == "hit1")
                {

                    ataque1 = false;
                    ataque2 = true;
                    defesa = false;
                }
            }
        }
                    
        }
    
    void Defesa() {
            defesa = false;
            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (noChao)
                {
                    defesa = true;
                }
            }
        }

    void Esquivar() {
            rola = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (noChao)
                {
                    rola = true;
                }
            }
        }

    void Lancar(){
           
                    //Dispara
                    tmpCorrentReload += Time.deltaTime;
                    if (tmpCorrentDelay >= delay)
                    {
                        Instantiate(spear, posLancamento.position, posLancamento.rotation);
                        tmpCorrentDelay = 0;
                        disparar = false;
                    }

                }
    #endregion

  /*  private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        if(item != null)
        {
            inventory.AddItem(item);
        }
    }*/




}

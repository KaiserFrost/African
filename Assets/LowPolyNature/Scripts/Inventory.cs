using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 9;

    public IList<InventorySlot> mSlots = new List<InventorySlot>();
    private InventorySlot invslot;
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public event EventHandler<InventoryEventArgs> ItemUsed;

    public Inventory()
    {
        for (int i = 0; i < SLOTS; i++)
        {
            mSlots.Add(new InventorySlot(i));
        }
    }

    [Header("Construccion")]
    public SpawnObj[] spawn;
    [System.Serializable]
    public class SpawnObj
    {
        public string Nombre;
        public int iD;
        public GameObject Spawn;
        public GameObject Prefab;
        [HideInInspector]
        public bool Activado = false;
        [HideInInspector]
        public bool PuedeConstruirse = false;
    }

    public void Update()
    {
        Construir();
    }
    void Construir()
    {
        //		int SpawnActivado = 0;		
        foreach (InventorySlot slot in mSlots)
        {
            for (int i = 0; i < spawn.Length; i++)
            {
                if (spawn[i].Activado == false && slot.Id == spawn[i].iD && slot.Count > 0)
                {
                    spawn[i].Spawn.gameObject.SetActive(true);
                    spawn[i].Activado = true;
                }

                if (spawn[i].Activado == true && slot.Id != spawn[i].iD)
                {
                    spawn[i].Activado = false;
                    spawn[i].Spawn.gameObject.SetActive(false);
                }


                if (slot.Count < 1 && spawn[i].Activado == true && slot.Id == spawn[i].iD)
                {
                 
                    spawn[i].Spawn.gameObject.SetActive(false);
                    spawn[i].Activado = false;
                }

                if (spawn[i].Activado == true && spawn[i].PuedeConstruirse == true)
                {
                    if (Input.GetKeyDown("e"))
                    {
                        if (slot.Id == spawn[i].iD && slot.Count > 0)
                        {
                            GameObject ClonIns;
                            ClonIns = Instantiate(spawn[i].Prefab, spawn[i].Spawn.transform.position, spawn[i].Spawn.transform.rotation) as GameObject;
                         
                            ClonIns.gameObject.SetActive(true);
                            ClonIns.name = spawn[i].Prefab.name;
                            Debug.Log("" + spawn[i].Prefab.name);
                        }
                    }
                }
            }
        }
    }

    private InventorySlot FindStackableSlot(InventoryItemBase item)
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.IsStackable(item))
                return slot;
        }
        return null;
    }

    private InventorySlot FindNextEmptySlot()
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.IsEmpty)
                return slot;
        }
        return null;
    }

    public void AddItem(InventoryItemBase item)
    {
        InventorySlot freeSlot = FindStackableSlot(item);
        if (freeSlot == null)
        {
            freeSlot = FindNextEmptySlot();
        }
        if (freeSlot != null)
        {
            freeSlot.AddItem(item);

            if (ItemAdded != null)
            {
                ItemAdded(this, new InventoryEventArgs(item));
            }

        }
    }

    internal void UseItem(InventoryItemBase item)
    {
        if (ItemUsed != null)
        {
            ItemUsed(this, new InventoryEventArgs(item));
        }
    }

    public void RemoveItem(InventoryItemBase item)
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.Remove(item))
            {
                if (ItemRemoved != null)
                {
                    ItemRemoved(this, new InventoryEventArgs(item));
                }
                break;
            }

        }
    }
}

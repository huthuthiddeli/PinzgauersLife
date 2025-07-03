using UnityEngine;

interface IItem
{

}

interface IEquipable
{
    public void Equip(GameObject obj);
    public void UnEquip(GameObject obj);
}

interface IQuestItem
{
    public bool IsLoosable { get; }
    public bool IsQuestItem { get; }


    public void GiveQuestItem(GameObject obj);
}
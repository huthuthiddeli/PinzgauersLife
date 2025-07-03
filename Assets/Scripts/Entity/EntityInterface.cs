using UnityEngine;


interface IEntity
{

}

interface IInteractable
{
    public void Interact(GameObject obj);
}

interface IPunshable
{
    public void Punsh(GameObject obj);
}
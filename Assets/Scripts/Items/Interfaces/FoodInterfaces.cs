using UnityEngine;

interface IBaseItem
{
    string Name { get; }
    string Description { get; }
    string IconPath { get; }
    uint ItemSlots { get; }
    bool IsStackable { get; }
}

interface IFood
{
    float HungerValue { get; }
    float ThirstValue { get; }
    float AlcoholValue { get; }
}
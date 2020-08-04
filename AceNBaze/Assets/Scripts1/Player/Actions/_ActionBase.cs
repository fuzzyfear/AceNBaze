using System;
using UnityEngine;

interface _ActionBase<T>
{
    void ActionFunction(CharacterBaseAbilitys characterBase, T input);
}


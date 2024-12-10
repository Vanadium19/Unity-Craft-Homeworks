using UnityEngine;

namespace Modules
{
    //Don't modify
    public interface ICoin
    {
        ///Quantity of snake bones to expand 
        int Bones { get; }

        ///Quantity of player score to add 
        int Score { get; }
        
        ///Position on map 
        Vector2Int Position { get; set; }
    }
}
using System;
using UnityEngine;

namespace SampleGame.Common
{
    //Don't modify
    [Serializable]
    public struct SerializedVector3
    {
        public float x, y, z;

        public SerializedVector3(Vector3 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;
        }
        
        public static implicit operator SerializedVector3(Vector3 vector) =>
            new() {x = vector.x, y = vector.y, z = vector.z};
        
        public static implicit operator Vector3(SerializedVector3 vector) =>
            new() {x = vector.x, y = vector.y, z = vector.z};

        public static implicit operator SerializedVector3(Quaternion quaternion) =>
            quaternion.eulerAngles;
        public static implicit operator Quaternion(SerializedVector3 vector) => 
            Quaternion.Euler(vector);
    }
}
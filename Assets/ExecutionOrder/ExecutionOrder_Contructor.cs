using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace ExecutionOrder
{
    public class ExecutionOrder_Contructor : MonoBehaviour, ISerializationCallbackReceiver
    {
        public static ObjectIDGenerator generator = new ObjectIDGenerator();

        public ExecutionOrder_Contructor()
        {
            bool firsTime;
            var hash = generator.GetId(this, out firsTime);
            ThreadHelper.LogCurThread($"ExecutionOrder_Contructor Contructor => hash = {hash}\n{Environment.StackTrace}");
        }
        
        ~ExecutionOrder_Contructor()
        {
            bool firsTime;
            var hash = generator.GetId(this, out firsTime);
            ThreadHelper.LogCurThread($"~ExecutionOrder_UnityEvents => hash = {hash}\n{Environment.StackTrace}");
        }
        
        // if gameobject with this component is selected, called on editor.Update
        public void OnBeforeSerialize()
        {
            bool firsTime;
            var hash = generator.GetId(this, out firsTime);
            ThreadHelper.LogCurThread($"ExecutionOrder_Contructor OnBeforeSerialize => hash = {hash}");
        }

        public void OnAfterDeserialize()
        {
            bool firsTime;
            var hash = generator.GetId(this, out firsTime);
            ThreadHelper.LogCurThread($"ExecutionOrder_Contructor OnAfterDeserialize => hash = {hash}");
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectTest", menuName = "MissingReferences/ScriptableObjectTest")]
public class ScriptableObjectTest : ScriptableObject
{
    public ScriptableObjectTest Reference;
    public List<ScriptableObjectTest> ReferencesCollection = new List<ScriptableObjectTest>();
    public TestSerializedClass SerializedClass;
}

[Serializable]
public class TestSerializedClass
{
    public List<ScriptableObjectTest> ReferencesCollection = new List<ScriptableObjectTest>();
}
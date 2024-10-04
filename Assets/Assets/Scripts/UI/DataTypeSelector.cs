using System.Collections.Generic;
using UnityEngine;

public enum DataType
{
    CameraCityMovement,
    AnotherScriptType
}

public class DataTypeSelector : MonoBehaviour
{
    public List<GameObject> gameObjectWithType;
    public List<DataType> selectedDataType;
}

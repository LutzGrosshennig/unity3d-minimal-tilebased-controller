using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TileArray))]
public class BoolArray2DDrawer : PropertyDrawer
{
    const int checkboxSize = 16;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Find properties
        var pWidth = property.FindPropertyRelative("_width");
        var pHeight = property.FindPropertyRelative("_height");
        var pArray = property.FindPropertyRelative("_array");
        var pTileSize = property.FindPropertyRelative("_tileSize");

        // Clamp height & width
        if (pWidth.intValue <= 0) { pWidth.intValue = 1; }
        if (pHeight.intValue <= 0) { pHeight.intValue = 1; }

        // Begin property
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Fix intent
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var halfWidth = position.width / 2;
        var widthRect = new Rect(position.x, position.y, halfWidth, checkboxSize);
        var heightRect = new Rect(position.x + halfWidth, position.y, halfWidth, checkboxSize);

        // Width & Height
        EditorGUIUtility.labelWidth = 40;
        EditorGUI.PropertyField(widthRect, pWidth, new GUIContent("Width"));
        EditorGUI.PropertyField(heightRect, pHeight, new GUIContent("Height"));
        position.y += 20;

        // Tile size property
        var UnitsRect = new Rect(position.x, position.y, halfWidth, checkboxSize);
        EditorGUI.PropertyField(UnitsRect, pTileSize, new GUIContent("Units"));

        position.y += 5;

        // Draw grid
        var width = pWidth.intValue;
        var height = pHeight.intValue;
        pArray.arraySize = width * height;
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var index = x + width * y;
                var rect = new Rect(0 + (x * checkboxSize), position.y + checkboxSize + (y * checkboxSize), checkboxSize, checkboxSize);
                EditorGUI.PropertyField(rect, pArray.GetArrayElementAtIndex(index), GUIContent.none);
            }
        }

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        if (GUILayout.Button("Clear Level"))
        {
            ClearLevel();
        }

        if (GUILayout.Button("Generate Level"))
        {
            GenerateLevel(property);
        }

        // End property
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUI.GetPropertyHeight(property, label);
        int sizeY = property.FindPropertyRelative("_height").intValue + 1;
        height += sizeY * checkboxSize + 10;
        return height;
    }

    private void GenerateLevel(SerializedProperty property)
    {
        var pWidth = property.FindPropertyRelative("_width");
        var pHeight = property.FindPropertyRelative("_height");
        var pArray = property.FindPropertyRelative("_array");
        var pTileSize = property.FindPropertyRelative("_tileSize");

        var width = pWidth.intValue;
        var height = pHeight.intValue;
        var units = pTileSize.intValue;
        pArray.arraySize = width * height;

        var root = new GameObject("Level");

        var floor = GameObject.CreatePrimitive(PrimitiveType.Quad);
        
        float floorx = ((float)width-1) * (float)units / 2f;
        float floorz = ((float)height-1) * (float)units / 2f;

        floor.transform.position = new Vector3(floorx, 0f, floorz);
        floor.transform.rotation = Quaternion.Euler(90f,0f, 0f);
        floor.isStatic  = true;
        floor.transform.localScale = (Vector3.one * units * width);
        floor.transform.SetParent(root.transform);

        for (int x = 0; x < pWidth.intValue; x++)
        {
            for (int y = 0; y < pHeight.intValue; y++)
            {
                var index = x + width * y;
                var tile = pArray.GetArrayElementAtIndex(index);
                if (!tile.boolValue)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(x * units, units/2f, y * units);
                    cube.transform.SetParent(root.transform);
                    cube.transform.localScale = new Vector3(units, units, units);
                    cube.isStatic = true;
                }
            }
        }
    }

    private void ClearLevel()
    {
        GameObject root = GameObject.Find("Level");
        GameObject.DestroyImmediate(root);
    }
}

//
// Copyright (c) 2017 Geri Borbás http://www.twitter.com/_eppz
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace EPPZ.Cloud.Model.Simulation
{

    
    [CustomPropertyDrawer(typeof(KeyValuePair))]
    public class KeyValuePair_PropertyDrawer : PropertyDrawer
    {


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        { return -2.0f; }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {   
            EditorGUILayout.BeginVertical(GUI.skin.GetStyle("HelpBox"));
            EditorGUI.BeginProperty(position, label, property);
            
            SerializedProperty keyProperty = property.FindPropertyRelative("key");
            SerializedProperty foldedOutProperty = property.FindPropertyRelative("foldedOut");
            foldedOutProperty.boolValue = EditorGUILayout.Foldout(foldedOutProperty.boolValue, keyProperty.stringValue);

            if (foldedOutProperty.boolValue)
            {
                EditorGUILayout.PropertyField(property.FindPropertyRelative("key"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("type"));

                Model.KeyValuePair.Type type = (Model.KeyValuePair.Type)property.FindPropertyRelative("type").intValue;
                switch (type)
                {
                    case Model.KeyValuePair.Type.String:
                        EditorGUILayout.PropertyField(
                            property.FindPropertyRelative("stringValue"),
                            new GUIContent("Value")
                        );
                        break;
                    case Model.KeyValuePair.Type.Float:
                        EditorGUILayout.PropertyField(
                            property.FindPropertyRelative("floatValue"),
                            new GUIContent("Value")
                        );
                        break;
                    case Model.KeyValuePair.Type.Int:
                        EditorGUILayout.PropertyField(
                            property.FindPropertyRelative("intValue"),
                            new GUIContent("Value")
                        );
                        break;
                    case Model.KeyValuePair.Type.Bool:
                        EditorGUILayout.PropertyField(
                            property.FindPropertyRelative("boolValue"),
                            new GUIContent("Value")
                        );
                        break;
                }

                EditorGUILayout.PropertyField(property.FindPropertyRelative("isChanged"));
            }

            EditorGUI.EndProperty();
            EditorGUILayout.EndVertical();
        }
    }
}

		
		

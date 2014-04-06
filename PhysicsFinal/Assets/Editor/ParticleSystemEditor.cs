using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CustomParticleSystem))]
public class ParticleSystemEditor : Editor {
	private CustomParticleSystem.EmitterType myFlag;
	private GUIStyle m_guiStyle = new GUIStyle(); //For smaller text
	
	public override void OnInspectorGUI () {
		serializedObject.Update();  
		m_guiStyle.fontSize = 9;
		//Reference to the CustomParticleSystem      
		CustomParticleSystem myTarget = (CustomParticleSystem)target;
            
		//General options
		myTarget.p_EmitterType = (CustomParticleSystem.EmitterType)EditorGUILayout.EnumPopup("Particle System" , myTarget.p_EmitterType); 	
		Show(serializedObject.FindProperty("p_ParticleReferences"));
		myFlag = myTarget.p_EmitterType;  
		
		if(myFlag == CustomParticleSystem.EmitterType.Radial)
			myTarget.p_NumberOfParticles = EditorGUILayout.IntField("Particles Per Burst", myTarget.p_NumberOfParticles);
		else
			myTarget.p_NumberOfParticles = EditorGUILayout.IntField("Number Of Particles", myTarget.p_NumberOfParticles);
			
			
		myTarget.p_ParticleSpeed = EditorGUILayout.FloatField("Particle Speed", myTarget.p_ParticleSpeed);
		
		//Repeat Toggle + Value
		myTarget.p_isRepeating = EditorGUILayout.Toggle("Repeating", myTarget.p_isRepeating);
		if(myTarget.p_isRepeating)
		{
			EditorGUI.indentLevel += 1;	
			myTarget.p_repeatSpeed = EditorGUILayout.FloatField("Repeat Speed", myTarget.p_repeatSpeed);
			EditorGUI.indentLevel -= 1;
			GUILayout.Space(10);
		}
		
		//Fade Toggle + Value
		myTarget.p_isFading = EditorGUILayout.Toggle("Fading", myTarget.p_isFading);
		if(myTarget.p_isFading)
		{
			EditorGUI.indentLevel += 1;	
			myTarget.p_fadeSpeed = EditorGUILayout.FloatField("Fade Speed", myTarget.p_fadeSpeed);
			myTarget.p_destroyOnFade = EditorGUILayout.Toggle("Destroy On Fade?", myTarget.p_destroyOnFade);
			EditorGUI.indentLevel -= 1;
			GUILayout.Space(10);
		}
		
		if(myFlag == CustomParticleSystem.EmitterType.Radial)
		{
			//Radial options
		}
		if(myFlag == CustomParticleSystem.EmitterType.Emitter)
		{
			//Emitter options	
			myTarget.p_arcWidth = EditorGUILayout.FloatField("Arc Of Emission", myTarget.p_arcWidth);
			GUILayout.BeginHorizontal();
			GUILayout.Space(140);
			GUILayout.Label("Default direction is straight up on the Y Axis", m_guiStyle);	
			GUILayout.EndHorizontal();
			myTarget.p_rotationInDegrees = EditorGUILayout.FloatField("Rotation Of Emission", myTarget.p_rotationInDegrees);	
		}
		if(myFlag == CustomParticleSystem.EmitterType.Trail)
		{
			//Trail options
		}
		
		serializedObject.ApplyModifiedProperties();
		
    }
	
	void Show(SerializedProperty array){
		EditorGUILayout.PropertyField(array, new GUIContent("Particle Prefabs"));
		
		EditorGUI.indentLevel += 1;	
		if(array.isExpanded)
		{
			EditorGUILayout.PropertyField(array.FindPropertyRelative("Array.size"));
			for(int i = 0; i < array.arraySize; i++){
				EditorGUILayout.PropertyField(array.GetArrayElementAtIndex(i));
			}
		}
		EditorGUI.indentLevel -= 1;
    }
}

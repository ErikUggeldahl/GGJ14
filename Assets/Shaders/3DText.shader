//Author: Eric Haines (Eric5h5)
//http://wiki.unity3d.com/index.php?title=3DText
Shader "GUI/3D Text Shader" 
{ 
	Properties 
	{ 
	   _MainTex ("Font Texture", 2D) = "white" {} 
	   _Alpha ("Transparency", float) = 1.0
	   _Color ("Text Color", Color) = (1,1,1,1) 
	} 
	
	SubShader 
	{ 
	   Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" } 
	   Lighting Off Cull Off ZWrite Off Fog { Mode Off }  
	   Blend SrcAlpha OneMinusSrcAlpha 
	   Pass
	   { 
	      Color [_Color] 
	      SetTexture [_MainTex] 
	      { 
	         combine primary, texture * primary 
	      }  
	   } 
	} 
}
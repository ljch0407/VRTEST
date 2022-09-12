// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "KUPAFX/Additve_Helix_Move"
{
	Properties
	{
		_FXT_Helix_Arrow("FXT_Helix_Arrow", 2D) = "white" {}
		_Helix_Move("Helix_Move", Float) = 0
		[HDR]_Tint_Color("Tint_Color", Color) = (1,1,1,0)
		_Arrow_Pow("Arrow_Pow", Range( 1 , 10)) = 1
		_Arrow_Ins("Arrow_Ins", Range( 1 , 10)) = 1
		[Toggle(_USE_CUSTOM_ON)] _Use_Custom("Use_Custom", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma shader_feature_local _USE_CUSTOM_ON
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float4 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform float4 _Tint_Color;
		uniform sampler2D _FXT_Helix_Arrow;
		uniform float4 _FXT_Helix_Arrow_ST;
		uniform float _Helix_Move;
		uniform float _Arrow_Pow;
		uniform float _Arrow_Ins;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 uvs_FXT_Helix_Arrow = i.uv_texcoord;
			uvs_FXT_Helix_Arrow.xy = i.uv_texcoord.xy * _FXT_Helix_Arrow_ST.xy + _FXT_Helix_Arrow_ST.zw;
			#ifdef _USE_CUSTOM_ON
				float staticSwitch16 = i.uv_texcoord.z;
			#else
				float staticSwitch16 = _Helix_Move;
			#endif
			float2 appendResult6 = (float2(staticSwitch16 , 0.0));
			o.Emission = ( ( _Tint_Color * ( pow( tex2D( _FXT_Helix_Arrow, (uvs_FXT_Helix_Arrow.xy*1.0 + appendResult6) ).r , _Arrow_Pow ) * _Arrow_Ins ) ) * i.vertexColor ).rgb;
			o.Alpha = ( i.vertexColor.a * saturate( pow( ( ( ( 1.0 - i.uv_texcoord.xy.x ) * i.uv_texcoord.xy.x ) * 4.0 ) , 5.0 ) ) );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
1;1;1912;967;2713.958;1120.976;2.682419;True;True
Node;AmplifyShaderEditor.RangedFloatNode;4;-1623.954,98.94113;Float;False;Property;_Helix_Move;Helix_Move;1;0;Create;True;0;0;0;False;0;False;0;0.71;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;15;-1669.837,-85.63861;Inherit;False;0;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;5;-1585.954,265.9411;Float;False;Constant;_Float1;Float 1;2;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;16;-1458.837,1.361389;Float;False;Property;_Use_Custom;Use_Custom;5;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;17;-939.9526,368.453;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;6;-1238.954,82.94113;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1448.954,-215.0589;Inherit;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;19;-625.9526,315.453;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;2;-1146.954,-112.0589;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-963.9545,-123.0589;Inherit;True;Property;_FXT_Helix_Arrow;FXT_Helix_Arrow;0;0;Create;True;0;0;0;False;0;False;-1;a72b30dc4d397c64bb98da8bcead82ba;a72b30dc4d397c64bb98da8bcead82ba;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-419.9526,388.453;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-932.9545,132.9411;Float;False;Property;_Arrow_Pow;Arrow_Pow;3;0;Create;True;0;0;0;False;0;False;1;1;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;7;-678.9545,-121.0589;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-645.9545,139.9411;Float;False;Property;_Arrow_Ins;Arrow_Ins;4;0;Create;True;0;0;0;False;0;False;1;1;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-165.9526,605.453;Float;False;Constant;_Float0;Float 0;7;0;Create;True;0;0;0;False;0;False;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-222.9526,390.453;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;4;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;21;-17.95264,390.453;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;10;-436.9545,-314.0589;Float;False;Property;_Tint_Color;Tint_Color;2;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;0.8773585,0.6461357,0.3434941,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-439.9545,-120.0589;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-214.9545,-180.0589;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;13;-210.4445,-74.44356;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;23;209.0474,388.453;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;461.0474,317.453;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;71.85962,-220.9221;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;35;674,-216;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;KUPAFX/Additve_Helix_Move;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;1;4;0
WireConnection;16;0;15;3
WireConnection;6;0;16;0
WireConnection;6;1;5;0
WireConnection;19;0;17;1
WireConnection;2;0;3;0
WireConnection;2;2;6;0
WireConnection;1;1;2;0
WireConnection;18;0;19;0
WireConnection;18;1;17;1
WireConnection;7;0;1;1
WireConnection;7;1;11;0
WireConnection;20;0;18;0
WireConnection;21;0;20;0
WireConnection;21;1;22;0
WireConnection;8;0;7;0
WireConnection;8;1;12;0
WireConnection;9;0;10;0
WireConnection;9;1;8;0
WireConnection;23;0;21;0
WireConnection;24;0;13;4
WireConnection;24;1;23;0
WireConnection;14;0;9;0
WireConnection;14;1;13;0
WireConnection;35;2;14;0
WireConnection;35;9;24;0
ASEEND*/
//CHKSM=A2FB7355D3401C9B5BDB548AF50C5E202AF85848
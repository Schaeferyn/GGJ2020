// Upgrade NOTE: upgraded instancing buffer 'GGJAltar' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "GGJ/Altar"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_EdgeDist("EdgeDist", Range( 0 , 0.5)) = 0.4
		_FadeDist("FadeDist", Range( 0 , 0.3)) = 0.4
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_DistortSpeed("DistortSpeed", Vector) = (0,0,0,0)
		[HDR]_Emission("Emission", Color) = (0,0,0,0)
		_Percent("Percent", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Emission;
		uniform sampler2D _TextureSample0;
		uniform sampler2D _Sampler6015;
		uniform float2 _DistortSpeed;
		uniform float _EdgeDist;
		uniform float _FadeDist;
		uniform float _Cutoff = 0.5;

		UNITY_INSTANCING_BUFFER_START(GGJAltar)
			UNITY_DEFINE_INSTANCED_PROP(float, _Percent)
#define _Percent_arr GGJAltar
		UNITY_INSTANCING_BUFFER_END(GGJAltar)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color37 = IsGammaSpace() ? float4(0.4245283,0.1784083,0.07008717,1) : float4(0.1507121,0.02677153,0.005991074,1);
			o.Albedo = color37.rgb;
			float2 temp_output_1_0_g1 = float2( 1,1 );
			float2 appendResult10_g1 = (float2(( (temp_output_1_0_g1).x * i.uv_texcoord.x ) , ( i.uv_texcoord.y * (temp_output_1_0_g1).y )));
			float2 temp_output_11_0_g1 = float2( 0,0 );
			float2 panner18_g1 = ( ( (temp_output_11_0_g1).x * _Time.y ) * float2( 1,0 ) + i.uv_texcoord);
			float2 panner19_g1 = ( ( _Time.y * (temp_output_11_0_g1).y ) * float2( 0,1 ) + i.uv_texcoord);
			float2 appendResult24_g1 = (float2((panner18_g1).x , (panner19_g1).y));
			float2 temp_output_47_0_g1 = _DistortSpeed;
			float2 uv_TexCoord78_g1 = i.uv_texcoord * float2( 2,2 );
			float2 temp_output_31_0_g1 = ( uv_TexCoord78_g1 - float2( 1,1 ) );
			float2 appendResult39_g1 = (float2(frac( ( atan2( (temp_output_31_0_g1).x , (temp_output_31_0_g1).y ) / 6.28318548202515 ) ) , length( temp_output_31_0_g1 )));
			float2 panner54_g1 = ( ( (temp_output_47_0_g1).x * _Time.y ) * float2( 1,0 ) + appendResult39_g1);
			float2 panner55_g1 = ( ( _Time.y * (temp_output_47_0_g1).y ) * float2( 0,1 ) + appendResult39_g1);
			float2 appendResult58_g1 = (float2((panner54_g1).x , (panner55_g1).y));
			float4 tex2DNode13 = tex2D( _TextureSample0, ( ( (tex2D( _Sampler6015, ( appendResult10_g1 + appendResult24_g1 ) )).rg * 0.1 ) + ( float2( 1,1 ) * appendResult58_g1 ) ) );
			float temp_output_2_0 = distance( i.uv_texcoord , float2( 0.5,0.5 ) );
			float temp_output_4_0 = (( temp_output_2_0 < 0.5 ) ? (( temp_output_2_0 > _EdgeDist ) ? (( temp_output_2_0 > ( _EdgeDist + _FadeDist ) ) ? 1.0 :  ( ( temp_output_2_0 - _EdgeDist ) / _FadeDist ) ) :  0.0 ) :  0.0 );
			float _Percent_Instance = UNITY_ACCESS_INSTANCED_PROP(_Percent_arr, _Percent);
			o.Emission = ( _Emission * tex2DNode13 * temp_output_4_0 * _Percent_Instance ).rgb;
			o.Smoothness = 0.5;
			o.Alpha = ( tex2DNode13.r * temp_output_4_0 * _Percent_Instance );
			clip( temp_output_4_0 - _Cutoff );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17600
19;290;1680;769;196.763;479.6621;1.3;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-671,-3.5;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;3;-644,204.5;Inherit;False;Constant;_Vector0;Vector 0;0;0;Create;True;0;0;False;0;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.DistanceOpNode;2;-414.4,100.7;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1030.2,265.5;Inherit;False;Property;_EdgeDist;EdgeDist;2;0;Create;True;0;0;False;0;0.4;0.26;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;27;-967.5245,737.7037;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-1172.925,423.1037;Inherit;False;Property;_FadeDist;FadeDist;3;0;Create;True;0;0;False;0;0.4;0.2;0;0.3;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;-769.9247,421.8037;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-831.1002,562.5999;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;28;-723.1246,757.2037;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;18;-222.3724,-702.907;Inherit;False;Property;_DistortSpeed;DistortSpeed;5;0;Create;True;0;0;False;0;0,0;0.3,0.3;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TFHCCompareGreater;23;-585.3245,507.6036;Inherit;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-183,420.5;Inherit;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;15;174.2745,-624.0948;Inherit;False;RadialUVDistortion;-1;;1;051d65e7699b41a4c800363fd0e822b2;0;7;60;SAMPLER2D;_Sampler6015;False;1;FLOAT2;1,1;False;11;FLOAT2;0,0;False;65;FLOAT;0.1;False;68;FLOAT2;1,1;False;47;FLOAT2;0,0;False;29;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCCompareGreater;10;-380.2,322.1;Inherit;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-279,-21.5;Inherit;False;Constant;_Float2;Float 2;1;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareLower;4;-129,118.5;Inherit;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;21;433.6276,-319.907;Inherit;False;Property;_Emission;Emission;7;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0.315329,1.720795,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;36;388.2469,267.7778;Inherit;False;InstancedProperty;_Percent;Percent;8;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;88.30977,-74.46617;Inherit;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;0;False;0;-1;None;61c0b9c0523734e0e91bc6043c72a490;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-164.3724,-514.907;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-86.2,-203.9;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;758.2745,-70.0948;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector2Node;19;-417.3724,-597.907;Inherit;False;Property;_Tiling;Tiling;6;0;Create;True;0;0;False;0;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;782.2758,100.7038;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;8;-422,-370.5;Inherit;False;Property;_Color0;Color 0;1;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;37;788.637,-423.7621;Inherit;False;Constant;_Color1;Color 1;9;0;Create;True;0;0;False;0;0.4245283,0.1784083,0.07008717,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;38;1334.637,-14.26208;Inherit;False;Constant;_Float3;Float 3;9;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;978.0001,-110.4;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;GGJ/Altar;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;0
WireConnection;2;1;3;0
WireConnection;27;0;2;0
WireConnection;27;1;11;0
WireConnection;26;0;11;0
WireConnection;26;1;25;0
WireConnection;28;0;27;0
WireConnection;28;1;25;0
WireConnection;23;0;2;0
WireConnection;23;1;26;0
WireConnection;23;2;5;0
WireConnection;23;3;28;0
WireConnection;15;47;18;0
WireConnection;10;0;2;0
WireConnection;10;1;11;0
WireConnection;10;2;23;0
WireConnection;10;3;6;0
WireConnection;4;0;2;0
WireConnection;4;1;7;0
WireConnection;4;2;10;0
WireConnection;4;3;6;0
WireConnection;13;1;15;0
WireConnection;20;0;19;0
WireConnection;9;0;8;0
WireConnection;9;1;4;0
WireConnection;14;0;21;0
WireConnection;14;1;13;0
WireConnection;14;2;4;0
WireConnection;14;3;36;0
WireConnection;22;0;13;1
WireConnection;22;1;4;0
WireConnection;22;2;36;0
WireConnection;0;0;37;0
WireConnection;0;2;14;0
WireConnection;0;4;38;0
WireConnection;0;9;22;0
WireConnection;0;10;4;0
ASEEND*/
//CHKSM=713ACAF159D6C901623E22A567BBC095B6C5A141
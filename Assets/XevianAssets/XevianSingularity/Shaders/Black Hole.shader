// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Black Hole"
{
	Properties
	{
		_Position("Position", Vector) = (0.5,0.5,0,0)
		_Radius("Radius", Float) = 1
		_Ratio("Ratio", Vector) = (1,1,0,0)
		_Distance("Distance", Float) = 1
		_OpacityRadius("Opacity Radius", Float) = 5
		_Float3("Float 3", Float) = 0
		_EventHorizon("Event Horizon", Range( 0 , 1)) = 0
		_Strength("Strength", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Overlay+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		GrabPass{ "_GrabScreenBlackHole" }
		CGPROGRAM
		#pragma target 3.0
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			float4 screenPos;
			float2 uv_texcoord;
		};

		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabScreenBlackHole )
		uniform float _OpacityRadius;
		uniform float _Strength;
		uniform float2 _Position;
		uniform float2 _Ratio;
		uniform float _Distance;
		uniform float _Radius;
		uniform float _EventHorizon;
		uniform float _Float3;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 temp_cast_0 = (_OpacityRadius).xx;
			float2 uv_TexCoord107 = i.uv_texcoord * temp_cast_0;
			float temp_output_105_0 = ( _OpacityRadius / 2.0 );
			float2 temp_cast_1 = (temp_output_105_0).xx;
			float temp_output_111_0 = length( ( uv_TexCoord107 - temp_cast_1 ) );
			float temp_output_112_0 = ( 1.0 - temp_output_111_0 );
			float clampResult140 = clamp( temp_output_112_0 , 0.0 , _Strength );
			float2 temp_output_96_0 = ( i.uv_texcoord - _Position );
			float clampResult131 = clamp( temp_output_112_0 , 0.0 , _Strength );
			float4 screenColor117 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabScreenBlackHole,( ( ase_screenPosNorm * ( 1.0 - clampResult140 ) ) + float4( ( ( _Position + ( temp_output_96_0 * ( 1.0 - ( 1.0 / ( pow( ( length( ( temp_output_96_0 / _Ratio ) ) * pow( _Distance , 0.5 ) ) , 2.0 ) * _Radius ) ) ) ) ) * clampResult131 ), 0.0 , 0.0 ) ).xy);
			float clampResult116 = clamp( (( 1.0 - _Strength ) + (temp_output_111_0 - _EventHorizon) * (1.0 - ( 1.0 - _Strength )) / (1.0 - _EventHorizon)) , 0.0 , 1.0 );
			o.Emission = ( ( screenColor117 * clampResult116 ) * _Float3 ).rgb;
			float2 appendResult11_g1 = (float2(temp_output_105_0 , temp_output_105_0));
			float temp_output_17_0_g1 = length( ( (i.uv_texcoord*2.0 + -1.0) / appendResult11_g1 ) );
			o.Alpha = saturate( ( ( 1.0 - temp_output_17_0_g1 ) / fwidth( temp_output_17_0_g1 ) ) );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
470;73;1427;769;550.813;38.91711;1;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;94;-2822.552,-93.40515;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;95;-2781.155,257.554;Inherit;True;Property;_Position;Position;1;0;Create;True;0;0;0;False;0;False;0.5,0.5;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleSubtractOpNode;96;-2478.811,63.2951;Inherit;True;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;97;-2450.137,401.8056;Inherit;True;Property;_Ratio;Ratio;3;0;Create;True;0;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;98;-2083.631,640.1888;Inherit;False;Property;_Distance;Distance;4;0;Create;True;0;0;0;False;0;False;1;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;99;-2083.723,189.0686;Inherit;True;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;100;-1921.595,654.488;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.LengthOpNode;101;-1776.24,363.7758;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;102;-1486.925,520.3132;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;103;-130.5476,994.6727;Inherit;False;Property;_OpacityRadius;Opacity Radius;5;0;Create;True;0;0;0;False;0;False;5;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;104;-1141.703,845.9675;Inherit;True;Property;_Radius;Radius;2;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;106;-1190.62,538.4833;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;107;182.9486,788.6033;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;10,10;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;105;98.45246,1108.673;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;108;-814.6513,545.4714;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;109;478.9487,791.6033;Inherit;True;2;0;FLOAT2;0,0;False;1;FLOAT;0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LengthOpNode;111;726.7761,670.8036;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;110;-525.336,500.7465;Inherit;True;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;112;1017.301,676.5341;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;125;-92.62048,491.3629;Inherit;False;Property;_Strength;Strength;8;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;129;-255.4424,429.9471;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;140;336.3922,194.3818;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;128;-60.10732,222.2399;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;139;534.5422,-20.30877;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;124;226.6988,-301.6903;Float;True;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;131;395.009,401.4166;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;130;150.3167,-30.09513;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;113;768.699,32.13401;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;672.3524,-205.4492;Inherit;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.OneMinusNode;132;547.5533,640.1933;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;133;701.0607,310.8371;Inherit;False;Property;_EventHorizon;Event Horizon;7;0;Create;True;0;0;0;False;0;False;0;0.4;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;115;1173.731,220.788;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0.8;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;114;1066.347,-94.18394;Inherit;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ScreenColorNode;117;1392.321,-138.6934;Inherit;False;Global;_GrabScreenBlackHole;Grab Screen Black Hole;2;0;Create;True;0;0;0;False;0;False;Object;-1;True;False;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;116;1475.625,161.6274;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;119;1755.655,181.973;Inherit;False;Property;_Float3;Float 3;6;0;Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;118;1693.4,-52.80561;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;120;2039.731,-41.59674;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;134;215.3667,597.9181;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;136;686.9261,1135.645;Inherit;True;Ellipse;-1;;1;3ba94b7b3cfd5f447befde8107c04d52;0;3;2;FLOAT2;0,0;False;7;FLOAT;0.5;False;9;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2606.099,-46.14131;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Black Hole;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Overlay;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;96;0;94;0
WireConnection;96;1;95;0
WireConnection;99;0;96;0
WireConnection;99;1;97;0
WireConnection;100;0;98;0
WireConnection;101;0;99;0
WireConnection;102;0;101;0
WireConnection;102;1;100;0
WireConnection;106;0;102;0
WireConnection;107;0;103;0
WireConnection;105;0;103;0
WireConnection;108;0;106;0
WireConnection;108;1;104;0
WireConnection;109;0;107;0
WireConnection;109;1;105;0
WireConnection;111;0;109;0
WireConnection;110;1;108;0
WireConnection;112;0;111;0
WireConnection;129;0;110;0
WireConnection;140;0;112;0
WireConnection;140;2;125;0
WireConnection;128;0;96;0
WireConnection;128;1;129;0
WireConnection;139;0;140;0
WireConnection;131;0;112;0
WireConnection;131;2;125;0
WireConnection;130;0;95;0
WireConnection;130;1;128;0
WireConnection;113;0;130;0
WireConnection;113;1;131;0
WireConnection;126;0;124;0
WireConnection;126;1;139;0
WireConnection;132;0;125;0
WireConnection;115;0;111;0
WireConnection;115;1;133;0
WireConnection;115;3;132;0
WireConnection;114;0;126;0
WireConnection;114;1;113;0
WireConnection;117;0;114;0
WireConnection;116;0;115;0
WireConnection;118;0;117;0
WireConnection;118;1;116;0
WireConnection;120;0;118;0
WireConnection;120;1;119;0
WireConnection;134;0;125;0
WireConnection;136;7;105;0
WireConnection;136;9;105;0
WireConnection;0;2;120;0
WireConnection;0;9;136;0
ASEEND*/
//CHKSM=CCF1B29B6559E85894AE2604993818970F9F967D
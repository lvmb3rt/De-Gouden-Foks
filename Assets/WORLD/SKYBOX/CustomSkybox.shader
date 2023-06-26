Shader "Unlit/CustomSkybox"
{
    Properties
    {
        _MainTex ("Cubemap", Cube) = "white" {}
        _RotationSpeedX ("Rotation Speed X", Range(0, 10)) = 1
        _RotationSpeedY ("Rotation Speed Y", Range(0, 10)) = 1
    }

    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Background" }
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest Less
        Blend Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            samplerCUBE _MainTex;
            float _RotationSpeedX;
            float _RotationSpeedY;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                float rotationX = _Time.y * _RotationSpeedX;
                float rotationY = _Time.y * _RotationSpeedY;

                float3x3 rotationMatrixX = float3x3(
                    1, 0, 0,
                    0, cos(rotationX), -sin(rotationX),
                    0, sin(rotationX), cos(rotationX)
                );

                float3x3 rotationMatrixY = float3x3(
                    cos(rotationY), 0, sin(rotationY),
                    0, 1, 0,
                    -sin(rotationY), 0, cos(rotationY)
                );

                float3 rotatedWorldPos = mul(rotationMatrixX, mul(rotationMatrixY, IN.worldPos));

                fixed4 color = texCUBE(_MainTex, rotatedWorldPos);
                return color;
            }

            ENDCG
        }
    }
}

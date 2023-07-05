
// Copyright © 2022 Doppelgänger
// Discord: Doppelgänger#8376
// Patreon: https://www.patreon.com/dopestuff

// v 1.0.1
// Added more color related options
// Added more options to control accretionary disk
// Added stencil
// Changed way how black hole rendering
// Updated UI: queue changer

// v 1.0.2
// Added ZWrite option for distortion pass

Shader "Doppels shaders/FX/Black Hole 1.0.2"
{
    Properties
    {
        _Color1("Disk Color 1 (RGB)", color) = (0.5,0.5,0.5,1)
        _Color2("Disk Color 2 (RGB)", color) = (0.5,0.5,0.5,1)
        _Color3("Black Hole Glow Color (RGBA)", color) = (0.8,0.8,0.8,1)
        _bhs("Black Hole Scale", range(0,1)) = 0.5
        _gravitation("Gravitation Power", range(0,1)) = 1.0
        _gravitationscale("Gravitation Scale", range(0,1)) = 0.5
        _absorptionspeed("Disk Absorption Speed", range(0,1)) = 0.5
        _rotationspeed("Disk Rotation Speed", range(0,1)) = 0.5
        _radialdistortion("Disk Radial Distortion", range(0,1)) = 0.5

        [Enum(Off, 0, On, 1)]HUE("Use Hue", int) = 0
        _hue("Hue", range(0, 1)) = 0
        _hueo("Hue Offset", float) = 0
        _huespeed("Hue Speed", float) = 0
        _hueoc("Offset Original Colors", range(0, 1)) = 0
        _huesaturation("Saturation", range(0, 1)) = 1

        [IntRange]_Stencil("Stencil ID [0;255]", Range(0,255)) = 0
        _ReadMask("ReadMask [0;255]", Int) = 255
        _WriteMask("WriteMask [0;255]", Int) = 255
        [Enum(UnityEngine.Rendering.CompareFunction)]_StencilComp("Stencil Comparison", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)]_StencilOp("Stencil Operation", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)]_StencilFail("Stencil Fail", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)]_StencilZFail("Stencil ZFail", Int) = 0
		[Enum(Off, 0, On, 1)]_UseCustomQueue("Use Custom Render Queue", int) = 0
        _Queue("Material Render Queue", int) = 4000
        [Enum(Off, 0, On, 1)]_ZWrite("Distortion Pass ZWrite", int) = 1
    }
    CustomEditor "BlackHole102GUI"
    SubShader
    {
        Tags { "RenderType"="Overlay" "Queue"="Overlay" }
        Cull Front
        Blend SrcAlpha OneMinusSrcAlpha
        CGINCLUDE
        uniform int HUE;
        uniform float _hue, _hueo, _huespeed, _hueoc, _huesaturation;
        float3 HSVToRGB(float3 c)
        {
            float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
            float3 p = saturate(abs(frac(c.xxx + K.xyz) * 6.0 - K.www) - K.xxx);
            return c.z * lerp(K.xxx, p, c.y);
        }

        float3 RGBToHSV(float3 c)
        {
            float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
            float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
            float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
            float d = q.x - min( q.w, q.y );
            float e = 1.0e-10;
            return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
        }
        ENDCG
		Stencil
		{
			Ref [_Stencil]
			ReadMask [_ReadMask]
			WriteMask [_WriteMask]
			Comp [_StencilComp]
			Pass [_StencilOp]
			Fail [_StencilFail]
			ZFail [_StencilZFail]
		}
        Pass
        {
            CGPROGRAM
            #pragma target 5.0
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex   : POSITION;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                float3 vdir     : TEXCOORD0;
                float3 cpos     : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.vdir = ObjSpaceViewDir(v.vertex);
                o.cpos = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos, 1.0)).xyz;
                return o;
            }

			float GetDepth(float3 p)
			{
				float4 d = UnityObjectToClipPos(float4(p, 1.0));
                #if UNITY_UV_STARTS_AT_TOP
                    return d.z / d.w;
                #else
                    return (d.z / d.w) * 0.5 + 0.5;
                #endif
			}

            uniform float4 _Color1, _Color2;
            uniform float _absorptionspeed, _rotationspeed, _radialdistortion;

            float hash21(float2 p)
            {
                return frac(sin(dot(p - 4.5 * floor(p / 4.5), float2(12.9898, 78.233))) * 47758.5453); 
            }

            float valuenoise2D(float2 p)
            {
                float2 id = floor(p);
                p = smoothstep(0.0, 1.0, frac(p));
                float n00 = hash21(id);
                float n01 = hash21(id + float2(0.0, 1.0));
                float n10 = hash21(id + float2(1.0, 0.0));
                float n11 = hash21(id + float2(1.0, 1.0));
                return lerp(lerp(n00, n10, p.x), lerp(n01, n11, p.x), p.y);
            }

            float2 rotate(float2 p, float a)
            {
                float s, c;
                sincos(a, s, c);
                return mul(p, float2x2(c,s,-s,c));
            }

            float fbm(float2 p, float t)
            {
                p = rotate(p, t * 0.5 * _rotationspeed);
                p = float2(atan2(p.x, p.y), length(p)) / UNITY_TWO_PI * 2.0;
                p.x += p.y * (0.5 + _radialdistortion);
                p.y += t * 0.1 * _absorptionspeed;
                p *= 4.5;
                float r = 0.0, z = 0.5;
                for(int i = 0; i < 5; i++)
                {
                    r += valuenoise2D(p) * z;
                    z *= 0.5;
                    p *= 2.0;
                }
                return r;
            }

            float DiskIntersect(float3 ro, float3 rd, float3 dr, float ra)
            {
                float i = -dot(ro, dr) / dot(rd, dr);
                float3 p = ro + rd * i; if (-(dot(p, p) - ra * ra) < 0.0 || i < 0.0) discard;
                return i;
            }

            fixed4 frag (v2f i, out float DP : SV_DEPTH) : SV_Target
            {
                fixed4 col = 1.0;
                float3 ro = i.cpos, rd = -normalize(i.vdir), p = ro + rd * DiskIntersect(ro, rd, float3(0.0, 0.0, 1.0), 0.5);

                float2 uv = p.xy;
                float n3 = fbm(uv * 24.0, _Time.y * 1.15);
                float n2 = fbm(uv * 9.0, _Time.y * 1.52);
                float n1 = fbm(uv * 7.0, _Time.y * 1.43);

                uv *= 1.0 + n1 * 0.23;
                uv *= 1.0 - n2 * 0.34;

                col.a = saturate(smoothstep(0.4, 0.25, length(uv)) * lerp(pow(n1 * n2, 7.0), 1.0, smoothstep(0.4, 0.2, length(uv))));
                col.rgb = clamp(pow(pow(n1, 7) + pow(n2, 5), 3.0 - 2.0 * lerp(_Color1.rgb, _Color2.rgb, saturate(n3*n3*n3*3.0))) * 30.0, 0, 3);

                DP = GetDepth(p);

                UNITY_BRANCH if (HUE)
                {
                    col.rgb = RGBToHSV(col.rgb);
                    col.rgb = HSVToRGB(float3(_hue + _hueo + lerp(0.0, col.r, _hueoc) + _Time.y * _huespeed, col.g * _huesaturation, col.b));
                }

                return col;
            }
            ENDCG
        }
        GrabPass {"_BlackHoleGP"}
        Pass
        {
            ZWrite [_ZWrite]
            ZTest Always
            CGPROGRAM
            #pragma target 5.0
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex   : POSITION;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                float3 grabUV   : TEXCOORD0;
                float4 cenUV    : TEXCOORD1;
                float3 vdir     : TEXCOORD2;
                float3 cpos     : TEXCOORD3;
            };

            uniform sampler2D _BlackHoleGP;
            uniform float4 _Color3;
            uniform float _gravitation, _gravitationscale, _bhs;

            static float3 ObjectScale = float3(
                length(float3(unity_ObjectToWorld[0].x, unity_ObjectToWorld[1].x, unity_ObjectToWorld[2].x)), 
                length(float3(unity_ObjectToWorld[0].y, unity_ObjectToWorld[1].y, unity_ObjectToWorld[2].y)), 
                length(float3(unity_ObjectToWorld[0].z, unity_ObjectToWorld[1].z, unity_ObjectToWorld[2].z)));

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.xyz *= 0.5 * _ProjectionParams.z / max(ObjectScale.x, max(ObjectScale.y, ObjectScale.z));
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.grabUV = ComputeGrabScreenPos(o.vertex).xyw;
                o.vdir = ObjSpaceViewDir(v.vertex);
                o.cpos = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos, 1.0)).xyz;
                o.cenUV = ComputeGrabScreenPos(UnityObjectToClipPos(float4(0,0,0,1)));
                o.cenUV.z = distance(_WorldSpaceCameraPos, mul(unity_ObjectToWorld, float4(0,0,0,1)).xyz);
                return o;
            }

            float st0(float x)
            {
                return saturate(x / fwidth(x) + 0.5);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 ro = i.cpos, rd = i.vdir;
                float2 uv = i.grabUV.xy / i.grabUV.z, cen = i.cenUV.xy / i.cenUV.w;
                float d = distance(normalize(rd), normalize(ro)) * i.cenUV.z / max(ObjectScale.x, max(ObjectScale.y, ObjectScale.z)) - _bhs * 0.12 - 0.03;
                float z = exp(-abs(d) * (20.0 - 15.0 * _gravitationscale));
                fixed3 col = tex2D(_BlackHoleGP, lerp(uv, cen, _gravitation * saturate(z * z)));
                UNITY_BRANCH if (HUE)
                {
                    _Color3.rgb = RGBToHSV(_Color3.rgb);
                    _Color3.rgb = HSVToRGB(float3(_hue + _hueo + lerp(0.0, _Color3.r, _hueoc) + _Time.y * _huespeed, _Color3.g * _huesaturation, _Color3.b));
                }
                col = lerp(col, _Color3.xyz * 3.0, max(_Color3.x, max(_Color3.y, _Color3.z)) * _Color3.w * saturate(pow(0.003/abs(d), 2.2))) * st0(d);
                col = lerp(col, 0.0, smoothstep(0.2*_bhs+0.03, 0.0, length(ro) - _bhs * 0.12 - 0.03));
                return fixed4(col, 1.0);
            }
            ENDCG
        }
    }
}

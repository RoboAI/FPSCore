Shader "Custom/StarsShader2"
{
    Properties
    {
        _StarDensity("Star Density", Range(0, 2000)) = 1000
        _StarSize("Star Size", Range(0.0001, 0.5)) = 0.002
        _StarColor("Star Color", Color) = (1,1,1,1)
        _BackgroundColor("Background Color", Color) = (0,0,0,1)
        _Seed("Noise Seed", Float) = 42
    }
    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Opaque" }
        //Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float3 dir : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _StarDensity;
            float _StarSize;
            float4 _StarColor;
            float4 _BackgroundColor;
            float _Seed;

            // Hash function for pseudo-random generation
            float hash(float3 p)
            {
                p = frac(p * 0.3183099 + float3(_Seed, _Seed, _Seed));
                p *= 100.0;
                return frac(p.x * p.y * p.z * (p.x + p.y + p.z));
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.dir = normalize(mul(unity_ObjectToWorld, v.vertex).xyz);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 dir = normalize(i.dir);

                // Map direction to spherical coordinates for hashing
                float3 p = dir * _StarDensity;

                float h = hash(floor(p));
                float d = frac(p.x + p.y + p.z);

                float brightness = smoothstep(_StarSize, 0.0, d) * step(0.999, h);

                return lerp(_BackgroundColor, _StarColor, brightness);
            }
            ENDCG
        }
    }
    FallBack Off
}
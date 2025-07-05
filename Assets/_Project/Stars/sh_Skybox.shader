Shader "Skybox/CustomGradient"
{
    Properties
    {
        _TopColor ("Top Color", Color) = (0.0, 0.5, 1.0, 1.0)
        _BottomColor ("Bottom Color", Color) = (1.0, 1.0, 1.0, 1.0)
    }
    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Background" }
        Cull Off ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _TopColor;
            fixed4 _BottomColor;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 direction : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.direction = normalize(mul(unity_ObjectToWorld, v.vertex).xyz);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float t = saturate(i.direction.y * 0.5 + 0.5); // from -1..1 to 0..1
                return lerp(_BottomColor, _TopColor, t);
            }
            ENDCG
        }
    }
}

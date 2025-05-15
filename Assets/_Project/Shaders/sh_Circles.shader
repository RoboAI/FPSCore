Shader "Unlit/hs_Circles"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _radius ("Radius", float) = 0.5
        _thickness ("Thickness", float) = 0.05
        _colour ("Lines Colour", Color) = (27,64,220,255)
        //_bkColour("Background Colour", Colour) = (0,0,0,0)
        _numOfRings ("Number of Rings", int) = 4
        _hasDot ("Has Dot at Center", float) = 1.0
        _dotSize ("Dot Size", Range(0,1)) = 0.1
        _ringsDist("Distance between rings", float) = 0.25
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _radius;
            float _thickness;
            float _hasDot;
            fixed4 _colour;
            float _dotSize;
            float _numOfRings;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                float2 center = float2(0.5,0.5);
                float dist = distance(i.uv, center);
                float edgeInner;
                float edgeOuter;
                float ringAlpha;
                
                if(_hasDot == 1.0 && dist <= _dotSize){
                    edgeInner = smoothstep(_dotSize - 1, _dotSize - 1 * 0.5, dist);
                    edgeOuter = smoothstep(_dotSize + 1 * 0.5, _dotSize + 1, dist);
                    ringAlpha = edgeInner * (1.0 - edgeOuter);
                }
                else
                {
                }
                edgeInner = smoothstep(_radius - _thickness, _radius - _thickness * 0.5, dist);
                edgeOuter = smoothstep(_radius + _thickness * 0.5, _radius + _thickness, dist);
                ringAlpha = edgeInner * (1.0 - edgeOuter);

                // Blend ring color with transparency
                fixed4 ringColor = _colour * ringAlpha;

                col = ringColor;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }

            int checkAndDrawOnlyDot(float dist)
            {
                float edgeInner;
                float edgeOuter;
                float ringAlpha;
                
                if(_hasDot == 1.0 && dist <= _dotSize){
                    _radius = _dotSize;
                    _thickness = 1;
                }

                edgeInner = smoothstep(_radius - _thickness, _radius - _thickness * 0.5, dist);
                edgeOuter = smoothstep(_radius + _thickness * 0.5, _radius + _thickness, dist);
                ringAlpha = edgeInner * (1.0 - edgeOuter);

                // Blend ring color with transparency
                return _colour * ringAlpha;
            }
            ENDCG
        }
    }
}

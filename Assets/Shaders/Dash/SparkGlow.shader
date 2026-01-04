Shader "Custom/SparkGlow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _GlowColor ("Glow Color", Color) = (0.3,0.7,1,1)
        _GlowIntensity ("Glow Intensity", Range(0,10)) = 2
        _DistortStrength ("Distortion Strength", Range(0,1)) = 0.1
        _ColorShiftSpeed ("Color Shift Speed", Range(0,100)) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend One One   // Additive = aparência de energia

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _BaseColor;
            float4 _GlowColor;
            float _GlowIntensity;
            float _DistortStrength;
            float _ColorShiftSpeed;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // Distortion
                float2 uv = i.uv;
                float noise = sin(uv.y * 30 + _Time * 20);
                uv.x += noise * _DistortStrength;

                float4 tex = tex2D(_MainTex, uv);

                // Color shift over time
                float shift = sin(_Time * _ColorShiftSpeed) * 0.5 + 0.5;
                float4 animatedGlow = lerp(_BaseColor, _GlowColor, shift);

                // Bloom fake
                float4 glow = tex * animatedGlow * _GlowIntensity;

                return glow;
            }
            ENDHLSL
        }
    }
}

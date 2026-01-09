Shader "Custom/SlashDistortion"
{
    Properties
    {
        _Mask ("Mask (white = visible)", 2D) = "white" {}
        _Strength ("Distortion Strength", Range(0,0.1)) = 0.03
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _CameraOpaqueTexture;
            sampler2D _Mask;

            float4 _Mask_ST;
            float _Strength;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _Mask);
                return o;
            }

            float4 frag (Varyings i) : SV_Target
            {
                float mask = tex2D(_Mask, i.uv).r;
                if (mask <= 0.01) discard;

                float2 screenUV = i.positionHCS.xy / i.positionHCS.w;
                screenUV = screenUV * 0.5 + 0.5;

                float2 dir = normalize(i.uv - float2(0.5, 0.5));
                float dist = length(i.uv - float2(0.5, 0.5));

                float distortion = (1 - dist) * _Strength;
                float2 distortedUV = screenUV + dir * distortion * mask;

                float4 col = tex2D(_CameraOpaqueTexture, distortedUV);

                col.a = mask;

                return col;
            }
            ENDHLSL
        }
    }
}

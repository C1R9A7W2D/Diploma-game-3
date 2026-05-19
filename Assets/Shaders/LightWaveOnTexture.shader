Shader "Custom/LightWaveOnTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _WaveSpeed ("Wave Speed", Float) = 5.0
        _WaveAmplitude ("Wave Amplitude", Float) = 1.5
        _WaveWidth ("Wave Width", Float) = 0.3
    }
    
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float _WaveSpeed, _WaveAmplitude, _WaveWidth;

            float4 _ContactPoint;
            bool _IsActive;
            float _LocalTime;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                float4 resCol = _Color;

                float luminanceThreshold = 0.5;
                float3 perceptedLuminance = float3(0.299, 0.587, 0.114);
                float intencityModifier = 1;

                float2 centerPos = _ContactPoint.xy;
                float dist = distance(i.worldPos.xy, centerPos);

                float waveIntencity = 0;
                if (_IsActive)
                    waveIntencity = max(0, intencityModifier - abs(dist * _WaveWidth - _LocalTime * _WaveSpeed));

                float luminance = dot(tex.rgb, perceptedLuminance);
                if (luminance > luminanceThreshold)
                    resCol *= waveIntencity;

                return tex * resCol;
            }
            ENDCG
        }
    }
}

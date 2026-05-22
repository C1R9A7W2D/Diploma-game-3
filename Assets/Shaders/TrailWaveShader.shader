Shader "Custom/TrailWave"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _WaveFrequency ("Wave Frequency", Float) = 5.0
        _WaveAmplitude ("Wave Amplitude", Float) = 1.0
        _WaveSpeed ("Wave Speed", Float) = 2.0
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
            float _WaveFrequency, _WaveAmplitude, _WaveSpeed;
            float _WavePhase;
            bool _IsHorizontal;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                
                float trailPos;
                if (_IsHorizontal)
                    trailPos = i.uv.x;
                else
                    trailPos = i.uv.y;

                _WaveAmplitude = sin(_WaveFrequency * trailPos - _WaveSpeed * _WavePhase);       
                return tex * _WaveAmplitude * _Color;
            }
            ENDCG
        }
    }
}

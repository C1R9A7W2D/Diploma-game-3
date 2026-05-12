Shader "Custom/Vibration"
{   
    Properties
    {
        _MainTex("Sprite", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _VibrationIntensity("Vibration Intensity", Range(0, 1)) = 0
        _VibrationFrequency("Vibration Frequency", Range(1, 50)) = 10
        _VibrationAmplitude("Vibration Amplitude", Range(0, 0.2)) = 0.05
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            
            #pragma vertex Vert
            #pragma fragment Frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _Color;
            float _VibrationIntensity;
            float _VibrationFrequency;
            float _VibrationAmplitude;

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

            v2f Vert(appdata v)
            {
                v2f o;

                float vibration = sin(_Time.y * _VibrationFrequency) 
                * _VibrationAmplitude 
                * _VibrationIntensity;

                v.vertex.x += vibration;
                v.vertex.y += cos(_Time.y * _VibrationFrequency) 
                * _VibrationAmplitude 
                * _VibrationIntensity;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                
                return o;
            }

            float4 Frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                return col;
            }
            
            ENDCG
        }
    }
}

Shader "Custom/FlickingTexture"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Mask ("Flash Mask", 2D) = "black" {}
        _Color ("Flash Color", Color) = (1, 1, 0, 1)
        _Intensity ("Flash Intensity", Float) = 1.0
        _IsActive ("Is Active", Float) = 0.0
    }
    
    SubShader
    {
        Tags 
        { 
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }
        
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };
            
            sampler2D _MainTex;
            sampler2D _Mask;
            float4 _MainTex_ST;
            float4 _Mask_ST;
            float4 _Color;
            float _Intensity;
            float _IsActive;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // Основная текстура
                fixed4 mainColor = tex2D(_MainTex, i.uv);
                
                // Маска мигания
                fixed4 maskColor = tex2D(_Mask, i.uv);
                
                // Применяем маску
                fixed4 flashEffect = maskColor * _IsActive * _Intensity * _Color;
                
                // Комбинируем
                fixed4 finalColor = mainColor + flashEffect;
                finalColor.a = mainColor.a;
                
                return finalColor;
            }
            ENDCG
        }
    }
    
    FallBack "Sprites/Default"
}

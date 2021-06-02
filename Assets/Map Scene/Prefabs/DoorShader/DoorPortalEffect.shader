Shader "Custom/DoorPortalEffect"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Noise("Noise", 2D) = "noise" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _SpeedX("SpeedX", float)=3.0
        _SpeedY("SpeedY", float)=3.0
        _Scale("Scale", range(0.005, 0.2))=0.03
        _TileX("TileX", float)=5
        _TileY("TileY", float)=5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        float _SpeedX;
        float _SpeedY;
        float _Scale;
        float _TileX;
        float _TileY;

        float lerp(float a, float b, float w) {
            return a + w*(b-a);
        }
        
        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
            
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = IN.uv_MainTex;
            uv.x += cos ((uv.x+uv.y)*_TileX+_Time.g *_SpeedX)*_Scale;
            uv.x += sin (uv.y*_TileY+_Time.g *_SpeedY)*_Scale;
            
            half4 c = tex2D (_MainTex, uv);
            o.Albedo = c.rgb;
            o.Alpha = c.a; 
        }
        ENDCG
    }
    FallBack "Diffuse"
}

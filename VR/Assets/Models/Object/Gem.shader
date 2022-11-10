Shader "Custom/Gem"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Dist ("Refraction Intensity", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
 
        GrabPass { "_RefractionTex" }
 
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert
 
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
 
        sampler2D _RefractionTex;
        float4 _RefractionTex_TexelSize;
 
        struct Input
        {
            float4 grabPos;
        };
 
        half _Glossiness;
        half _Metallic;
        half _Dist;
        fixed4 _Color;
 
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
 
        void vert (inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT (Input, o);
 
            float4 pos = UnityObjectToClipPos (v.vertex);
            o.grabPos = ComputeGrabScreenPos (pos);
        }
 
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = IN.grabPos.xy / IN.grabPos.w;
 
            float2 norm = normalize (mul ((float3x3)unity_WorldToCamera, o.Normal)).xy;
 
            o.Albedo = tex2D (_RefractionTex, uv - norm * 0.1 * _Dist) * _Color;
 
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
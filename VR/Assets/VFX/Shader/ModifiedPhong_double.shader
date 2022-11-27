Shader "Custom/ModifiedPhong_double"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _SpecColor("Specular MAterial Color", Color) = (1,1,1,1)
        _Shininess("Shininess (n)", Range(1,1000)) = 100
        _NormalMap("Normal MAp", 2D) = "bump" {}
        _Ambient("Ambient",Range (0,1)) = 0.25
    }
    SubShader
    {
        Tags { "RenderType"="Cutout" }
        LOD 200
        
        Zwrite On
        Cull off
        
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf PhongModified fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NormalMap;
        struct Input
        {
            float2 uv_MainTex;
        };

        half _Shininess;
        fixed4 _Color;
        float _Ambient;

        inline void LightingPhongModified_GI(
            SurfaceOutput s,
            UnityGIInput data,
            inout UnityGI gi)
        {
            gi = UnityGlobalIllumination(data, 1,0, s.Normal);
        }

        inline fixed4 LightingPhongModified (SurfaceOutput s, half3 viewDir, UnityGI gi)
        {
            const float PI = 3.14159265358979323846;
            UnityLight light = gi.light;

            float nl = max(_Ambient, dot(s.Normal, light.dir));
            float3 diffuseTerm = nl * s.Albedo.rgb * light.color;

            float norm = (_Shininess + 2) / (2 * PI);
            float3 reflectionDirection = reflect(-light.dir, s.Normal);
            float3 specularDot = max(0.0, dot(viewDir, reflectionDirection));
            float3 specular = norm * pow(specularDot, _Shininess);
            float3 specularTerm = specular * _SpecColor.rgb * light.color.rgb;

            float3 finalColor = diffuseTerm.rgb + specularTerm;

            fixed4 c;
            c.rgb = finalColor;
            c.a = s.Alpha;

            #ifdef UNITY_LIGHT_FUNCTION_APPLY_INDIRECT
            c.rgb += s.Albedo * gi.indirect.diffuse;
            #endif

            return c;
        }
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
            // Metallic and smoothness come from slider variables
            o.Specular = _Shininess;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

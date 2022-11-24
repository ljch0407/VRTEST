Shader "Custom/Post Outline"
{
    Properties
    {
        _MainTex("Main Texture",2D) = "black"{}
        _SceneTex("Scene Texture",2D) = "black"{}
    }
        SubShader
    {
        Pass
        {
            CGPROGRAM
            sampler2D _MainTex;
            sampler2D _SceneTex;
            float2 _MainTex_TexelSize;
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uvs : TEXCOORD0;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uvs = o.pos.xy / 2 + 0.5;
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                //if something already exists underneath the fragment, discard the fragment.
                    if (tex2D(_MainTex,i.uvs.xy).r > 0)
                    {
                        return tex2D(_SceneTex,i.uvs.xy);
                    }
                    int NumberOfIterations = 19;

                    float TX_x = _MainTex_TexelSize.x;
                    float TX_y = _MainTex_TexelSize.y;

                    float ColorIntensityInRadius = 0;

                    for (int k = 0; k < NumberOfIterations; k += 1)
                    {
                        for (int j = 0; j < NumberOfIterations; j += 1)
                        {
                            ColorIntensityInRadius += tex2D(
                                                          _MainTex,
                                                          i.uvs.xy + float2
                                                                       (
                                                                            (k - NumberOfIterations / 2) * TX_x,
                                                                            (j - NumberOfIterations / 2) * TX_y
                                                                       )
                                                         ).r;
                        }
                    }

                    //this value will be pretty high, so we won't see a blur. let's lower it for now.
                    ColorIntensityInRadius *= 0.005;

                    //output some intensity of teal
                    half4 color = tex2D(_SceneTex,i.uvs.xy) + ColorIntensityInRadius * half4(1,0,0,1);
                
                    return color;
                }

                ENDCG

            }
        //end pass        
    }
        //end subshader
}
//end shader
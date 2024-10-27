Shader "Custom/OutlineGlowShader_LargeGlow" {
    Properties {
        _MainTex ("Main Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,0,1)  // Glow color
        _OutlineWidth ("Outline Width", Range(0.0, 0.2)) = 0.05  // Thickness of the glow
        _GlowSize ("Glow Size", Range(1, 10)) = 3  // Glow size (larger means bigger glow)
    }
    SubShader {
        Tags { "Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 200

        // For UI elements
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        ZTest Always

        Pass {
            Name "OUTLINE"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineWidth;
            float _GlowSize;  // New property to control glow size

            v2f vert (appdata_t v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Get the current pixel color
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // Find the outline by checking the alpha of nearby pixels
                float alpha = texColor.a;
                float outline = 0.0;

                // Create a larger sampling area using GlowSize
                float2 offsets[16];  // More samples for a larger glow

                int index = 0;
                for (int x = -1; x <= 1; x++) {
                    for (int y = -1; y <= 1; y++) {
                        if (x != 0 || y != 0) {
                            offsets[index++] = float2(x * _GlowSize * _OutlineWidth, y * _GlowSize * _OutlineWidth);
                        }
                    }
                }

                // Check neighboring pixels for transparency to detect edges
                for (int k = 0; k < 16; k++) {
                    float4 neighborColor = tex2D(_MainTex, i.uv + offsets[k]);
                    outline = max(outline, neighborColor.a);  // Look at alpha of nearby pixels
                }

                // If this pixel is transparent but neighbors are not, apply glow
                if (texColor.a == 0.0 && outline > 0.0) {
                    return _OutlineColor;  // Glow color
                } else {
                    return texColor;  // Keep original image
                }
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}

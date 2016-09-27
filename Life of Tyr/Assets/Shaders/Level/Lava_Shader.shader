Shader "Custom/Lava_Shader"
{
	Properties
	{
		_MainTexture ("MainTexture", 2D) = "white" {}
		_NormalTexture ("NormalTexture", 2D) = "white" {}
		_GlowColor ("Glow Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

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
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 position : SV_POSITION;
			};

			sampler2D _MainTexture;
			float4 _MainTexture_ST;
			sampler2D _NormalTexture;
			float4 _GlowColor;
				
			v2f vert (appdata IN)
			{
				v2f OUT;
				
				float phase = _Time * 20.0;
				float offset = (IN.vertex.x * 2.2 + (IN.vertex.z * 2.2)) * 0.5;
				IN.vertex.y = sin(phase + offset) * 0.2;

				OUT.position = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.uv = TRANSFORM_TEX(IN.uv, _MainTexture);

				return OUT;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 mainColor = tex2D(_MainTexture, i.uv);
				float normalTextureAlpha = tex2D(_NormalTexture, i.uv).r;
				half4 normalColor = tex2D(_NormalTexture, float2(i.uv.x, i.uv.y));

				float luminance =  dot(normalColor, fixed4(0.2126, 0.7152, 0.0722, 0)) * 2;
				if(normalTextureAlpha < normalColor.a)
				{
					//mainColor = lerp(mainColor, mainColor * _GlowColor, 1);
				}

				if(luminance > 1.2f)
				{
					mainColor = lerp(mainColor, mainColor + _GlowColor, 1);
				}
				// sample the texture
				return mainColor;
			}
			ENDCG
		}
	}
}

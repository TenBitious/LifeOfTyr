Shader "Custom/RobotShader"
{
	Properties
	{
		_MainTexture("Main Color (RGB)", 2D) = "White"{}
		_SeccondTexture("Seccond Texture", 2D) = "White"{}
		_DissolveTexture("Dissolve Texture", 2D) = "White"{}
		_DissolveAmount("Dissolve Amount", Range(.5,1)) = 1
		_Color("Color",Color) = (1,1,1,1)
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM

			#pragma vertex vertexFunction
			#pragma fragment fragmentFunction
			#include "UnityCG.cginc"
			//Vertices
			//Normal
			//Color
			//UV
			struct appdata 
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};
			
			float4 _Color;
			sampler2D _MainTexture;
			sampler2D _SeccondTexture;
			sampler2D _DissolveTexture;
			float _DissolveAmount;

			bool startDissolve, isDissolving;

			//Build object
			v2f vertexFunction(appdata IN) 
			{
				v2f OUT;

				OUT.position = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.uv = IN.uv;

				return OUT;
			}

			//Color it in
			fixed4 fragmentFunction(v2f IN) : SV_TARGET
			{
				fixed4 textureColor = tex2D(_MainTexture, IN.uv);
				float4 seccondTexture = tex2D(_SeccondTexture, IN.uv) * _Color;
				float dissolve = tex2D(_DissolveTexture, IN.uv).r;

				//Dissolve sin wave
				//_DissolveAmount = sin(_DissolveAmount * _Time.z);
				if(dissolve <  _DissolveAmount)
					textureColor =lerp(textureColor, seccondTexture, _DissolveAmount );
				
				return textureColor;
			}
			
			
			ENDCG
		}
	}

}

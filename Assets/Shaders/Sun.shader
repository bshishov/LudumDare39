Shader "Custom/Sun"
{
	Properties
	{
		_NoiseTex("Texture", 2D) = "white" {}						
		_Temperature("Temperature", Range(0.0, 1.0)) = 0.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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
				float2 uv1 : TEXCOORD0;
				float2 uv2 : TEXCOORD1;		
				float4 vertex : SV_POSITION;
			};

			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;
			float _Temperature;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv1 = TRANSFORM_TEX(v.uv + float2(sin(v.vertex.y * 1), -sin(v.vertex.x * 0.01) * 10), _NoiseTex);
				o.uv2 = TRANSFORM_TEX((v.uv * 2), _NoiseTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{	
				fixed high = log(_Temperature * 2 + 1) * 2;
				//fixed high = pow(_Temperature + 1, 4);
				
				fixed3 color = sin(lerp(fixed3(0,0,0), fixed3(1,1,0), _Temperature));
				fixed n1 = tex2D(_NoiseTex, i.uv2 * 5 + _Time.xy * 0.1).r;
				fixed n2 = tex2D(_NoiseTex, i.uv1).r;
				fixed n3 = tex2D(_NoiseTex, i.uv2 - _Time.xy * 0.1).r;

				//fixed4 col1 = tex2D(_NoiseTex, i.uv1 + _Time.x) * _Color1;
				//fixed4 col2 = tex2D(_NoiseTex, i.uv2 - _Time.x) * _Color2;				
				
				color = color * n2 + n3 * color + pow(n1 * high, 2);
				return fixed4(color, 1);
			}
			ENDCG
		}
	}
}

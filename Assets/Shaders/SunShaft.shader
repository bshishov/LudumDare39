Shader "Custom/SunShaft"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Temperature("Temperature", Range(0.0, 1.0)) = 0.0
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		Cull Off
		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha
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
				float2 uv : TEXCOORD0;				
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _Temperature;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv + float2(_Time.x, 0), _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{				
				fixed high = log(_Temperature * 2 + 1) * 2;
				fixed3 color = lerp(fixed3(0,0,0), fixed3(1,1,0), high);
				fixed val = tex2D(_MainTex, i.uv).r;
				return fixed4(color.xyz * val + high * (1 - val), val * val);
			}
			ENDCG
		}
	}
}

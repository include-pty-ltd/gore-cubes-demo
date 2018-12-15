Shader "Include/ObjectIDShader"
{
	Properties
	{
		_ObjectID("Object ID", Int) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			ZWrite On
			Cull Back 

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			int _ObjectID;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
				float4 c = 0;
				c.r = (_ObjectID / 256) / (float)0xff;
				c.g = (_ObjectID % 256) / (float)0xff;
				return c;
			}
			ENDCG
		}
	}
}

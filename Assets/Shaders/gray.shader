Shader "Unlit/gray"
{
	Properties{
		_MainTex("Texture", 2D) = "white" {}
	_Brightness("Brightness Value", Range(-0.5, 0.5)) = 0
		_Contrast("Contrast Value", Range(-0.5, 0.5)) = 0
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		Cull Off Lighting Off
		CGPROGRAM
#pragma surface surf Lambert
	struct Input
	{
		float2 uv_MainTex;
	};
	uniform float _testVar;
	uniform float _Brightness;
	uniform float _Contrast;
	sampler2D _MainTex;
	void surf(Input IN, inout SurfaceOutput o)
	{
		// Applying Contrast
		float factor = (1.0156862 * (_Contrast + 1)) / (1 * (1.0156862 - _Contrast));
		float red = tex2D(_MainTex, IN.uv_MainTex).r;
		float green = tex2D(_MainTex, IN.uv_MainTex).g;
		float blue = tex2D(_MainTex, IN.uv_MainTex).b;
		red = (factor * (red - 0.5) + 0.5);
		green = (factor * (green - 0.5) + 0.5);
		blue = (factor * (blue - 0.5) + 0.5);
		// end Contrast

		// Applying Brightness
		red = red + _Brightness;
		green = green + _Brightness;
		blue = blue + _Brightness;
		// end Brightness


		// Applying B & W
		//                   float average = (red+green+blue) / 3 ;
		float average = 0.30 * red + 0.59 * green + 0.11 * blue;
		red = green = blue = average;
		// end B & W o.Albedo = float3 (red, green, blue) ;    
	}



	ENDCG
	}
		Fallback "Diffuse"

}    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}

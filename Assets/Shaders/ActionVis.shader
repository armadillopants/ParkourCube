Shader "Custom/ActionVis" {
	Properties {
		_MainTex ("Main", 2D) = "white" {}
		_OverlayTex ("Overlay", 2D) = "white" {}
		//_FalloffTex ("Fade", 2D) = "white" {}
		_Blend ("Blend", Vector) = (1,1,1,1)
		_VisColor ("Color", Color) = (1,1,1,1)
		_Darken ("Darken", Float) = 0.8
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		sampler2D _OverlayTex;
		//sampler2D _FalloffTex;
		half4 _Blend;
		half4 _VisColor;
		float _Darken;

		struct Input {
			float2 uv_MainTex;
			float2 uv_OverlayTex;
			//float2 uv_FalloffTex;
		};

		struct GradientData
		{
			float start;
			float end;
			float current;
		};

		half grad(GradientData g)
		{
			half result;
			if(g.start < g.end) { result = clamp(g.current, g.start, g.end); }
			else                { result = clamp(g.current, g.end, g.start); }
			result = abs(result - g.start);
			result = result / abs(g.end - g.start);
			return 1 - result;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			half4 mTex = tex2D (_MainTex, IN.uv_MainTex);
			half4 oTex = tex2D (_OverlayTex, IN.uv_OverlayTex);
			half oAlpha = oTex.a;
			o.Alpha = mTex.a;
			//float bHor = 1 - (1 - IN.uv_OverlayTex.x * _Blend.x);

			// bias to mTex
			half bVal = 0;

			if(_Blend.x != _Blend.y)
			{
				GradientData gH;
				gH.start = _Blend.x;
				gH.end = _Blend.y;
				gH.current = IN.uv_OverlayTex.x;
				bVal = grad(gH);
				o.Emission = lerp(mTex, _VisColor*oAlpha, bVal);
			}
			else if(_Blend.z != _Blend.w)
			{
				GradientData gV;
				gV.start = _Blend.z;
				gV.end = _Blend.w;
				gV.current = IN.uv_OverlayTex.y;
				bVal = grad(gV);
				o.Emission = lerp(mTex, _VisColor*oAlpha, bVal);
			}
			else
			{
				o.Emission = mTex;
			}
			

			//o.Emission = lerp(mTex, _VisColor, bHor);
			//o.Emission = lerp(mTex, _VisColor, bHor);// + lerp(mTex, _VisColor, bVert);
			//o.Emission = lerp(mTex, lerp(_VisColor, _VisColor*_Darken, oTex.a), oTex.a);
		}

		ENDCG
	} 
	FallBack "Transparent/Diffuse"
}

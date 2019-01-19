// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//Vertex and Fragment Shader  type

Shader "Custom/TemperatureField" {
	Properties { 
		_MainTex("Texture (RGB)", 2D)="white" {}
		_Point1("Temperature1",Range(0,100)) = 50 
		_Point2("Temperature2",Range(0,100)) = 50 
		_Point3("Temperature3",Range(0,100)) = 50 
		_Point4("Temperature4",Range(0,200)) = 50
	} 
	
	SubShader {
		AlphaTest Greater 0.1 
		Pass{
		 	CGPROGRAM
		 	
		 		// Upgrade NOTE: excluded shader from DX11, Xbox360, OpenGL ES 2.0 because it uses unsized arrays
//		 		#pragma exclude_renderers d3d11 xbox360 gles
		 		
		 		#pragma target 3.0
		 	    #pragma vertex vert
                #pragma fragment frag
		 	 	#include "UnityCG.cginc"
		 	 	
		 	 	sampler2D _MainTex; 
				float4 _MainTex_ST; 
				float _Point1; 
				float _Point2; 
				float _Point3; 
				float _Point4; 
				bool computer = false;
		
				uniform  float4 _PointsArray[100];
				uniform float _Points_Num;
           
		
		 	 	struct v2f
                {
                    float4 pos:SV_POSITION;
					float2 uv:TEXCOORD0;
					fixed4 color1 : COLOR;
                };

				 v2f vert(appdata_full v)
				 {
				 	v2f o;
				 	
				 	o.pos=UnityObjectToClipPos(v.vertex); 
					o.uv = TRANSFORM_TEX(v.texcoord,_MainTex); 
                    o.color1 = v.color;
				 	return o;
				 }

		 	 	
		 
		 	 	float computerTemperature(float2 uv)
		 	 	{
		 	 		float d1 = sqrt(uv.x*uv.x + uv.y*uv.y);
		 	 		float d2 = sqrt((1-uv.x)*(1-uv.x) + (1-uv.y)*(1-uv.y));
		 	 		float d3 = sqrt(uv.x*uv.x + (1-uv.y)*(1-uv.y));
		 	 		float d4 = sqrt((1-uv.x)*(1-uv.x) + uv.y*uv.y);
		 	 		
		 	 		float m = 1/d1 + 1/d2 + 1/d3 + 1/d4;
		 	 		float n = 1/d1 * _Point1 + 1/d2 * _Point2 + 1/d3 * _Point3 + 1/d4 * _Point4;

		 	 		for(int i = 0; i < _Points_Num; i++){
		 	 			float4 tempPos = _PointsArray[i];
		 	 			float dp = sqrt((uv.x - tempPos.x)*(uv.x - tempPos.x) + (uv.y - tempPos.y)* (uv.y - tempPos.y));
		 	 			
		 	 			m = m + 1/dp;
		 	 			n = n + 1/dp * tempPos.z;
		 	 		}
		 	 		
		 	 		return n/m;
		 	 	}
		 	 	
		 	 	float4 frag(v2f i) : COLOR
		 	 	{
		 	 	 	float4 outp;
		 	 	 	
		 	 	 	float4 texCol = tex2D(_MainTex, i.uv);
		 	 	 	float temp = computerTemperature(i.uv);
		 	 	 	 
		 	 	 	if(texCol.w > 0.5)
		 	 	 	{
		 	 	 		if(temp >= 60){
		 	 	 			outp = float4(1,0,0,1)*(temp-60)/40 + float4(1,1,0,1)*(1-(temp-60)/40);
		 	 	 		}else if(temp >= 30){
		 	 	 			outp = float4(1,1,0,1)*(temp-30)/30 + float4(0,1,0,1)*(1-(temp-30)/30);
		 	 	 		}else{
		 	 	 			outp = float4(0,1,0,1)*temp/30 + float4(0,0,1,1)*(1-temp/30);
		 	 	 		}
		 	 	 	}else{
		 		 	 	outp = float4(0,0,0,0);
		 	 	 	}
		 	 	 	return outp;
		 	 	 }
		 	 	
		 	ENDCG
		 }
	}
	
	FallBack "Diffuse" 
}

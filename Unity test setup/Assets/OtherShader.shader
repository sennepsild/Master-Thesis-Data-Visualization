Shader "Unlit/OtherShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
	    _Color("Diffuse Material Color", float) = 1
		
		
		
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

			uniform float blobCount;
			uniform float blobSize[300];
			uniform float blobNeg[300];
			uniform float blobPos[300];
			uniform float blobNeu[300];
			uniform float bokivalues[300];


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


			float lerp(float v0, float v1, float t) {
				return (1. - t) * v0 + t * v1;
			}
			
			float hash21(float2 p) {
				p = frac(p*float2(123.123, 433.231));
				p += dot(p, p + 12.32);

				return frac(p.x*p.y);

			}
			float hash11(float p) {
				p = frac(p*12123.133);
				return frac(p);

			}

			float ShapeMask(float2 uv, int edges, float size, float seed) {

				float PI = 3.14159265359;
				float TWO_PI = 6.28318530718;

				float d = 0.0;

				int N = edges;

				float a = atan2(uv.x, uv.y) + PI + seed;
				float r = TWO_PI / float(N);
				d = cos(floor(.5 + a / r)*r - a)*length(uv*3. / size);



				return 1.0 - smoothstep(.4, .41, d);

			}


			float BoubaKikiMask(float2 uv, float boKi, float seed, float size) {

				boKi = lerp(0.1, 0.7, boKi);

				float r = length(uv);
				float a = atan2(uv.y, uv.x);
				//a = a/3.14;
				//a = a/2.;

				float newa = a * 3.14;

				newa = newa * 5.*(boKi);

				float radius = 0.1*(1.2 - boKi)*size;

				float dist = length(uv);

				//color = float3(1.-smoothstep(f,f+0.01,r));
				//radius+=(0.5+cos((a/2.)+iTime)*0.5)*0.1;

				radius += ((1. + cos(newa))*0.5)*(abs(cos(seed + a)) + sin(a*10.)*0.1*6.)*(0.1*boKi);

				float valueToreturn = smoothstep(radius, radius - 0.00005, dist);
				valueToreturn += ShapeMask(uv, 3, .4*size, seed);
				valueToreturn += ShapeMask(uv, 3, .4*size, seed + 5.);

				valueToreturn = clamp(valueToreturn, 0., 1.);

				return valueToreturn;


			}


			fixed4 frag(v2f i) : SV_Target
			{
				




				//float2 uv = fragCoord/iResolution.xy;
				float2 uv = i.uv -0.5f;




				float3 color = 0;




				//color += BoubaKikiMask(uv,cos(iTime)*.5+.5);


				//color += BoubaKikiMask(uv + float2(.1, -.2), 1., 12321., 0.5);
				//color += BoubaKikiMask(uv + float2(-.1, .2), 0., 12321., 0.5);

				//color += BoubaKikiMask(float2(.1,-.2)+uv,0.3,666.);

				//color += BoubaKikiMask(float2(-.1,.3)+uv,0.8,12321.);

				//color += ShapeMask(uv,3,.4,5.);
				//color += ShapeMask(uv,3,.4,100.);


				//if( a>1.) a = 0.;

				//color += a;

				float gridSize = 4.;

				float2 gv = frac(uv*gridSize) - .5;
				float2 id = floor(uv*gridSize);
				//color.rg =gv;



				//random =1.- random;
				//color += float3(hash21(uv),hash21(uv+1.),hash21(uv+2.));
				//color += random;

				// The set up
				
				for(int i;i< blobCount;i++){
					float random = hash21(float2(i,.2));
					float random2 = hash21(float2(i,.1));
					float mask =BoubaKikiMask(uv+float2((1.-random*2.)*.45,(1.-random2*2.)*.45), bokivalues[i], (random+random2*8.), blobSize[i]);
					color.r += mask * blobNeg[i];
					color.g += mask * blobPos[i];
					color.b += mask * blobNeu[i];
				}

				if(color.r+color.g+color.b ==0.){
					color += float3(hash21(uv),hash21(uv+1.),hash21(uv+2.));
					color.r = clamp(color.r,0.4,1.);
					color.g = clamp(color.g,0.4,1.);
					color.b = clamp(color.b,0.4,1.);

				}
			

				return float4(color.r, color.g, color.b, 1.0);
			}

            
            ENDCG
        }
    }
}

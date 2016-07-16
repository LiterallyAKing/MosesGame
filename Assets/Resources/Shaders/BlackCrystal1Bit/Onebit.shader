Shader "Hidden/Onebit" {
  Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _White ("White", Color) = (0,0,0,0)
    _Black ("Black", Color) = (0,0,0,0)
    _Dithering ("Dithering", Float) = 1
  }
  SubShader {
    Tags { "Queue"="Overlay" }

    Pass {
       
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"

      fixed4 _White;
      fixed4 _Black;
      fixed _Dithering;
      uniform sampler2D _MainTex;
      float4 _MainTex_TexelSize;
      
      struct v2f {
        float4 pos : POSITION;
        half2 uv : TEXCOORD0;
      };

      float eq(fixed4 a, fixed4 b) { // is equal
        if(abs(a.r - b.r) < 0.01 && abs(a.b - b.b) < 0.01) return 1;
        else return 0;
      }
      float neq(fixed4 a, fixed4 b) { if(eq(a, b)) return 0; else return 1; } // is not equal
      float bg(fixed4 a) { // is a background pixel
        if(a.r == 0 && a.g == 0 && a.b == 0) return 1;
        else return 0;
      }
      float edg(fixed4 c) { if(c.a > 0.25) return 1; else return 0; } // is marked as edge
      float nedg(fixed4 c) { if(edg(c)) return 0; else return 1; } // is not marked as edge

      v2f vert (appdata_img v){
        v2f o;
        o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
        o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
        return o; 
      }
      
      fixed4 frag(v2f i) : COLOR {
        fixed4 c = tex2D(_MainTex, i.uv);

        if(bg(c)) {
          c.a = 0.1;
        } else {
          fixed4 u = tex2D(_MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y));
          if(bg(u)) { c.a = 1; return c; }

          fixed4 d = tex2D(_MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y));
          if(bg(d)) { c.a = 1; return c; }

          fixed4 l = tex2D(_MainTex, i.uv - fixed2(_MainTex_TexelSize.x, 0));
          if(bg(l)) { c.a = 1; return c; }

          fixed4 r = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x, 0));
          if(bg(r)) { c.a = 1; return c; }

          if(neq(c, u)) { c.a = 0.5; }
        }

        return c;
      }

      ENDCG
    }

    Pass {
       
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"

      fixed4 _White;
      fixed4 _Black;
      fixed _Dithering;
      uniform sampler2D _MainTex;
      float4 _MainTex_TexelSize;
      
      struct v2f {
        float4 pos : POSITION;
        half2 uv : TEXCOORD0;
      };

      float eq(fixed4 a, fixed4 b) { // is equal
        if(abs(a.r - b.r) < 0.01 && abs(a.b - b.b) < 0.01) return 1;
        else return 0;
      }
      float neq(fixed4 a, fixed4 b) { if(eq(a, b)) return 0; else return 1; } // is not equal
      float bg(fixed4 a) { // is a background pixel
        if(a.r == 0 && a.g == 0 && a.b == 0) return 1;
        else return 0;
      }
      float edg(fixed4 c) { if(c.a > 0.25) return 1; else return 0; } // is marked as edge
      float nedg(fixed4 c) { if(edg(c)) return 0; else return 1; } // is not marked as edge

      v2f vert (appdata_img v){
        v2f o;
        o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
        o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
        return o; 
      }
      
      fixed4 frag(v2f i) : COLOR {
        fixed4 c = tex2D(_MainTex, i.uv);

        float isBg = 0;
        float isOuterEdge = 0;
        float isInnerUpperEdge = 0;
        float isInnerRightEdge = 0;
        float isFace = 0;

        fixed4 u = tex2D(_MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y));
        fixed4 r = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x, 0));
        fixed4 d = tex2D(_MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y));
        fixed4 l = tex2D(_MainTex, i.uv - fixed2(_MainTex_TexelSize.x, 0));

        if(bg(c))           isBg = 1;
        else if(c.a > 0.75) isOuterEdge = 1;
        else if(c.a > 0.25) isInnerUpperEdge = 1;
        else {

          if(neq(c, r) && nedg(r)) {
            isInnerRightEdge = 1;
          }
          else {
            isFace = 1;
          }

        }

        if(_Dithering) {

          if(isBg) c = _White;
          else {

            int x = int(fmod(i.uv.x / _MainTex_TexelSize.x, 8));
            int y = int(fmod(i.uv.y / _MainTex_TexelSize.y, 8));

            // ordered dithering using bayer matrix
            // http://devlog-martinsh.blogspot.de/2011/03/glsl-8x8-bayer-matrix-dithering.html
            int dither[8][8] = {
              { 0, 32, 8, 40, 2, 34, 10, 42},   /* 8x8 Bayer ordered dithering */
              {48, 16, 56, 24, 50, 18, 58, 26}, /* pattern. Each input pixel */
              {12, 44, 4, 36, 14, 46, 6, 38},   /* is scaled to the 0..63 range */
              {60, 28, 52, 20, 62, 30, 54, 22}, /* before looking in this table */
              { 3, 35, 11, 43, 1, 33, 9, 41},   /* to determine the action. */
              {51, 19, 59, 27, 49, 17, 57, 25},
              {15, 47, 7, 39, 13, 45, 5, 37},
              {63, 31, 55, 23, 61, 29, 53, 21} }; 

            float limit = 0.0;
            if(x < 8) {
              limit = (dither[x][y]+1)/64.0;
            }

            if(c.g < 0.5) c.g = 0;
            else c.g *= 1.5;

            if(isOuterEdge) {

              c = _Black;

            } else if(isInnerUpperEdge) {

              if(c.g < limit) {
                // if(neq(c, u) && edg(u) && eq(c, r) && nedg(r) && eq(c, d) && nedg(d) && neq(c, l) && l.g < limit) {
                  c = _White;
                // } else {
                  // c = _Black;
                // }
              } else {
                c = _Black;
              }

            } else if(isInnerRightEdge) {

              if(c.g < limit) {
                c = _White;
              } else {
                c = _Black;
              }

            } else if(isFace) {

              if(c.g < limit) c = _Black;
              else c = _White;

            }
          }
        } else {
          if(isBg) {
            c = _White;
          } else if(isOuterEdge) {
            c = _Black;
          } else if(isInnerUpperEdge) {
            c = _Black;
          } else if(isInnerRightEdge) {
            c = _Black;
          } else if(isFace) {
            c = _White;
          }
        }

        return c;
      }

      ENDCG
    }
  }

  FallBack "Diffuse"
}

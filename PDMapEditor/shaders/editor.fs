// EDITOR
#version 330

uniform mat4 inMatM;

smooth in vec2 outUV0;
smooth in vec3 outNorm;
smooth in vec3 outColor;
smooth in vec3 outPos_W;
smooth in vec4 vEyeSpacePos;

// material settings
uniform sampler2D inTexMat;

uniform vec4 matDiffuse;
uniform vec4 matSpecular;
uniform float shininess;

uniform bool isTextured;
uniform bool shaded;
uniform bool vertexColored;
uniform bool blackIsTransparent;
uniform float textureFactor;

uniform struct FogParameters 
{ 
	bool active;
	vec4 color; // Fog color
	float start; // This is only for linear fog
	float end; // This is only for linear fog
	float density;
	int equation; // 0 = linear, 1 = exp, 2 = exp2
} fogParams;

out vec4 finalColor;

float getFogFactor(vec4 color, float start, float end, float density, int equation, float coord) 
{ 
   float result = 0.0; 
   if(equation == 0) 
      result = (end - coord)/(end - start); 
   else if(equation == 1) 
      result = exp(-density * coord); 
   else if(equation == 2) 
      result = exp(-pow(density * coord, 2.0)); 
       
   result = 1.0 - clamp(result, 0.0, 1.0); 
    
   return result * color.a * density; 
}

void main() 
{
	finalColor = matDiffuse;
    
	if (isTextured)
	{
		vec4 texColor = texture(inTexMat, outUV0);
		
		
		vec3 color = mix(matDiffuse.xyz, texColor.xyz, textureFactor);

		//vec3 color = texColor.xyz;
		finalColor = vec4(color, 1);
		//finalColor = vec4(texColor.xyz, 1);
		
		//For navlights (the in-game sprite)
		if(blackIsTransparent)
		{
			float alpha = (finalColor.x + finalColor.y + finalColor.z) / 3.0;
		}
	}
	else if(vertexColored)
	{
		finalColor = vec4(outColor * matDiffuse.xyz, matDiffuse.w);
	}
	
	//final color (after gamma correction)
	vec3 gamma = vec3(1.0/2.2);
	finalColor = vec4(pow(finalColor.xyz, gamma), finalColor.a);
	
	//Add fog
	if(fogParams.active)
	{
		float fogCoord = abs(vEyeSpacePos.z / vEyeSpacePos.w); 
		finalColor.xyz = mix(finalColor.xyz, fogParams.color.xyz, getFogFactor(fogParams.color, fogParams.start, fogParams.end, fogParams.density, fogParams.equation, fogCoord));
	}
}
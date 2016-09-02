#version 330

// Values that stay constant for the whole mesh.
uniform mat4 inMatM;
uniform mat4 inMatV;
uniform mat4 inMatP;

layout (location = 0) in vec3 inPos;

void main()
{
    vec4 usePos = vec4(inPos, 1.0);

	mat4 invV = inverse(inMatV);
	vec4 posW4 = inMatM * usePos;
    
	mat4 VP = inMatP * inMatV;
	gl_Position = VP * posW4;			// Clip-space vert for geo/rasterize
}
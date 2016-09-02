#version 330

// Ouput data
out vec4 color;

// Values that stay constant for the whole mesh.
uniform vec3 pickingColor;

void main()
{
    color = vec4(pickingColor, 1);
}
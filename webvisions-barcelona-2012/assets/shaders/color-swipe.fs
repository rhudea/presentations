precision mediump float;
uniform float amount;
varying vec2 v_texCoord;
vec2 pos = v_texCoord;

mat4 transposeMatrix(mat4 m)
{
    return mat4(
        m[0].x, m[1].x, m[2].x, m[3].x,
        m[0].y, m[1].y, m[2].y, m[3].y,
        m[0].z, m[1].z, m[2].z, m[3].z,
        m[0].w, m[1].w, m[2].w, m[3].w
    );
}

void main()
{
    const vec3 grayFactor = vec3(0.2127, 0.7152, 0.0722);

    float p = 1.0 - pos.y;
    
    float threshold = amount * 1.2;
    float a = amount;
    if (p < threshold) {
        a = min(abs(threshold - p) / 0.2, 1.0);
        a = 1.0 - a;
    } else {
        a = min(abs(threshold - p) / 0.005, 1.0);
    }

    vec3 m1 = mix(vec3(1.0), grayFactor, a);
    vec3 m0 = mix(vec3(0.0), grayFactor, a);

    mat4 colorMatrix = mat4(
        m1.r, m0.r, m0.r, 0.0,
        m0.g, m1.g, m0.g, 0.0,
        m0.b, m0.b, m1.b, 0.0,
        0.0, 0.0, 0.0, 1.0
    );

    css_ColorMatrix = transposeMatrix(colorMatrix);
}

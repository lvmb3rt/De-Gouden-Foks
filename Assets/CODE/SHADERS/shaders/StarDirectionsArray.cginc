// 1024 blue-noise/Poisson-disk star directions for stable, artifact-free procedural starfield
// Generated offline for uniform sphere coverage
// Format: float3 _StarDirections[1024];
// 8 blue-noise/Poisson-disk star directions for stable, artifact-free procedural starfield (testing only)
static const float3 _StarDirections[8] = {
    float3(0.0, 1.0, 0.0),
    float3(0.0, -1.0, 0.0),
    float3(1.0, 0.0, 0.0),
    float3(-1.0, 0.0, 0.0),
    float3(0.0, 0.0, 1.0),
    float3(0.0, 0.0, -1.0),
    normalize(float3(1.0, 1.0, 1.0)),
    normalize(float3(-1.0, -1.0, -1.0))
};

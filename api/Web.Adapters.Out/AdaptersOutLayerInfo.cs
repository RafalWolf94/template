using System.Reflection;
using P3Model.Annotations.Technology.CleanArchitecture;

[assembly: AdaptersLayer]
namespace Web.Adapters.Out;

public static class AdaptersOutLayerInfo
{
    public static Assembly Assembly => typeof(AdaptersLayerAttribute).Assembly;
}
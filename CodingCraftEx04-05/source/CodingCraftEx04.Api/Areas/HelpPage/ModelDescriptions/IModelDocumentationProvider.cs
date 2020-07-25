using System;
using System.Reflection;

namespace CodingCraftEx04.Api.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);
        string GetDocumentation(Type type);
    }
}
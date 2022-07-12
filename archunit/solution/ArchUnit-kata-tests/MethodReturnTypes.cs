using ArchUnit.Kata.Examples;
using ArchUnit.Kata.Examples.Models;
using ArchUnitNET.Fluent.Syntax.Elements.Types.Classes;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchUnit.Kata.Tests
{
    public class MethodReturnTypes
    {
        [Fact]
        public void CommandHandlersShouldOnlyReturnInt() =>
            MethodMembers().That()
                .AreDeclaredIn(typeof(ICommandHandler<>)).Should()
                .HaveReturnType(typeof(int))
                .Check();

        [Fact]
        public void ControllersPublicMethodShouldOnlyReturnApiResponse() =>
            MethodMembers().That()
                .ArePublic().And()
                .AreNoConstructors().And()
                .AreDeclaredIn(Controllers()).Should()
                .HaveReturnType(typeof(ApiResponse<>))
                .Check();

        private static GivenClassesConjunction Controllers() =>
            Classes().That().HaveAnyAttributes(typeof(ApiControllerAttribute));
    }
}
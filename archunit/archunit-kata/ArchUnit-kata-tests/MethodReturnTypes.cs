﻿using ArchUnitNET.Domain;
using ArchUnitNET.Fluent.Syntax.Elements.Members.MethodMembers;

using ArchUnit.Kata.Layered.Controllers;
using ArchUnit.Kata.Layered.Models;
using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Syntax.Elements.Types;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

using Xunit;
using static ArchUnit.Kata.Tests.ArchUnitExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArchUnit.Kata.Tests
{
    public class MethodReturnTypes
    {
        [Fact]
        public void CommandHandlersShouldOnlyReturnInt() => MethodMembers()
            .That().AreDeclaredIn(typeof(SuperHeroController))
            .Should().HaveReturnType(typeof(int))
            .Because("Command handler should only return ints")
                .Check();

        [Fact]
        public void ControllersPublicMethodShouldOnlyReturnApiResponse() => MethodMembers()
            .That().AreDeclaredIn(typeof(SuperHeroController))
            .And().ArePublic()
            .And().AreNoConstructors()
            .Should().HaveReturnType(typeof(ActionResult))
            .Because("Controllers public methods should only return ActionResult")
            .Check();
    }
}
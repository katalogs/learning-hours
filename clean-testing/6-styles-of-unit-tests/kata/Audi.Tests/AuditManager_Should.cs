using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Audit;
using Moq;
using Xunit;

namespace Audi.Tests;

public class AuditManager_Should
{
    private const string DirectoryName = "audits";
    
    [Fact]
    public void A_new_file_is_created_when_the_current_file_overflows()
    {
        // Arrange
        var fileSystemMock = new Mock<IFileSystem>();
        fileSystemMock
            .Setup(x => x.GetFiles(DirectoryName))
            .Returns(new string[]
            {
                Path.Combine(DirectoryName, "audit_1.txt"),
                //Path.Combine(DirectoryName, "audit_2.txt")
            });

        //fileSystemMock
        //    .Setup(x => x.ReadAllLines(Path.Combine(DirectoryName, "audit_2.txt")))
        //    .Returns(new List<string>
        //    {
        //        "Peter;2019-04-06 16:30:00",
        //        "Jane;2019-04-06 16:40:00",
        //        "Jack;2019-04-06 17:00:00"
        //    });
        InMemoryFileSystem mock = new InMemoryFileSystem();
        var sut = new AuditManager(3, DirectoryName, mock);
        
        // Act
        sut.AddRecord("Alice", DateTime.Parse("2019-04-06T18:00:00"));

        // Assert
        var result = mock.ListFiles.TryGetValue(Path.Combine(DirectoryName, "audit_3.txt"), out var fileContent);
        Assert.True(result);
        Assert.Equal("Alice;2019-04-06 18:00:00", fileContent.FirstOrDefault());
    }

    [Fact]
    public void it_should_add_record()
    {
        // Arrange
        var fileSystemMock = new Mock<IFileSystem>();
        fileSystemMock
            .Setup(x => x.GetFiles(DirectoryName))
            .Returns(new string[]
            {
                Path.Combine(DirectoryName, "audit_1.txt"),
                Path.Combine(DirectoryName, "audit_2.txt")
            });

        fileSystemMock
            .Setup(x => x.ReadAllLines(Path.Combine(DirectoryName, "audit_2.txt")))
            .Returns(new List<string>
            {
                "Peter;2019-04-06 16:30:00",
                "Jane;2019-04-06 16:40:00",
                "Jack;2019-04-06 17:00:00"
            });
        var sut = new AuditManager(3, DirectoryName, fileSystemMock.Object);

        // Act
        sut.AddRecord("Alice", DateTime.Parse("2019-04-06T18:00:00"));

        // Assert
        fileSystemMock.Verify(x => x.WriteAllText(
            Path.Combine(DirectoryName, "audit_3.txt"),
            "Alice;2019-04-06 18:00:00"));
    }
}

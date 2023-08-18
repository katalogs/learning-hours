import com.tngtech.archunit.core.domain.JavaClasses;
import com.tngtech.archunit.core.importer.ClassFileImporter;
import org.junit.jupiter.api.Test;

import java.util.UUID;

import static com.tngtech.archunit.lang.syntax.ArchRuleDefinition.*;

public class NamingConvention {
    private final JavaClasses classes =new ClassFileImporter().importPackages("archunit");

    @Test
    public void servicesShouldBeSuffixedByService() {
        classes().that().resideInAPackage("..service..")
                .should().haveSimpleNameEndingWith("Service")
                .check(classes);
    }

    @Test
    public void InterfacesShouldNotEndWithInterface() {
        classes().that().areInterfaces()
                .should().haveSimpleNameNotEndingWith("Interface")
                .check(classes);
    }

    @Test
    public void UUIDAttributesShouldNotBeNamedUUID() {
        noFields().that().haveRawType(UUID.class)
                .should().haveName("uuid")
                .because("We agreed that we should use id or a domain specific naming")
                .check(classes);
    }
}

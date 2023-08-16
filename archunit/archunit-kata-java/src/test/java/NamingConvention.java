import com.tngtech.archunit.core.domain.JavaClasses;
import com.tngtech.archunit.core.importer.ClassFileImporter;
import org.junit.jupiter.api.Test;

public class NamingConvention {
    private final JavaClasses classes =new ClassFileImporter().importPackages("archunit");

    @Test
    public void servicesShouldBeSuffixedByService() {
    }

    @Test
    public void InterfacesShouldNotEndWithInterface() {
    }

    @Test
    public void UUIDAttributesShouldNotBeNamedUUID() {
    }
}

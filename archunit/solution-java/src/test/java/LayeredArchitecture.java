import com.tngtech.archunit.core.domain.JavaClasses;
import com.tngtech.archunit.core.importer.ClassFileImporter;
import org.junit.jupiter.api.Test;

import static com.tngtech.archunit.lang.syntax.ArchRuleDefinition.*;

public class LayeredArchitecture {
    private final JavaClasses classes =new ClassFileImporter().importPackages("archunit.layered");

    @Test
    public void domainLayerRules() {
        noClasses().that().resideInAPackage("..domain..")
                .should().dependOnClassesThat().resideInAnyPackage("..api..","..infrastructure..","..service..")
                .check(classes);
    }

    @Test
    public void ServiceLayerRules() {
        noClasses().that().resideInAPackage("..service..")
                .should().dependOnClassesThat().resideInAnyPackage("..api..","..infrastructure..")
                .check(classes);
    }

    @Test
    public void APILayerRules() {
        noClasses().that().resideInAPackage("..api..")
                .should().dependOnClassesThat().resideInAPackage("..infrastructure..")
                .check(classes);
    }
}

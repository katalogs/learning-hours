import com.tngtech.archunit.core.domain.JavaClasses;
import com.tngtech.archunit.core.importer.ClassFileImporter;
import org.junit.jupiter.api.Test;

import java.util.concurrent.Future;

import static com.tngtech.archunit.lang.syntax.ArchRuleDefinition.methods;

public class MethodReturnType {
    private final JavaClasses classes = new ClassFileImporter().importPackages("archunit");

    @Test
    public void repositoriesShouldReturnFuture() {
        methods().that().areDeclaredInClassesThat().haveSimpleNameEndingWith("Repository")
                .should().haveRawReturnType(Future.class)
                .check(classes);
    }
}

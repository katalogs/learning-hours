import com.tngtech.archunit.core.domain.JavaClasses;
import com.tngtech.archunit.core.importer.ClassFileImporter;
import org.junit.jupiter.api.Test;

import static com.tngtech.archunit.lang.syntax.ArchRuleDefinition.methods;
import static com.tngtech.archunit.lang.syntax.ArchRuleDefinition.noMethods;

public class LinguisticAntiPatterns {
    private final JavaClasses classes =new ClassFileImporter().importPackages("archunit");

    @Test
    public void NoGetMethodShouldReturnVoid() {
        noMethods().that().haveNameStartingWith("get")
                .should().haveRawReturnType(void.class)
                .because("C'est pas bien")
                .check(classes);
    }

    @Test
    public void IserAndHaserShouldReturnBooleans() {
        methods().that().haveNameMatching("is[A-Z].*").or().haveNameMatching("has[A-Z].*")
                .should().haveRawReturnType(boolean.class)
                .check(classes);
    }

    @Test
    public void SettersShouldNotReturnSomething() {
        methods().that().haveNameStartingWith("set")
                .should().haveRawReturnType(void.class)
                .allowEmptyShould(true)
                .check(classes);
    }
}

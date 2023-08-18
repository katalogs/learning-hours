import com.tngtech.archunit.core.domain.JavaClasses;
import com.tngtech.archunit.core.importer.ClassFileImporter;
import org.junit.jupiter.api.Test;

public class MethodReturnType {
    private final JavaClasses classes = new ClassFileImporter().importPackages("archunit");

    @Test
    public void repositoriesShouldReturnFuture() {
    }
}

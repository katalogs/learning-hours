import com.tngtech.archunit.core.domain.JavaClasses;
import com.tngtech.archunit.core.importer.ClassFileImporter;
import org.junit.jupiter.api.Test;

public class LayeredArchitecture {
    private final JavaClasses classes = new ClassFileImporter().importPackages("archunit.layered");

    @Test
    public void domainLayerRules() {
    }

    @Test
    public void ServiceLayerRules() {
    }

    @Test
    public void APILayerRules() {
    }
}

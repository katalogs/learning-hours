package archunit.layered.infrastructure;

import java.util.List;
import java.util.UUID;

public record SuperHeroEntity(UUID id, String name, List<String> powers) {
}

package archunit.layered.infrastructure;

import java.util.List;
import java.util.UUID;

public record SuperHeroEntity(UUID uuid, String name, List<String> powers) {
}

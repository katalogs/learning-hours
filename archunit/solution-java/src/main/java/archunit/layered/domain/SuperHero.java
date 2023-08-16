package archunit.layered.domain;

import java.util.List;
import java.util.UUID;

public record SuperHero(UUID id, String name, List<String> powers) {
    public boolean hasPower() {
        return !powers.isEmpty();
    }
}

package archunit.layered.infrastructure;

import java.util.concurrent.Future;

public interface SuperHeroRepository {
    Future<SuperHeroEntity> save(SuperHeroEntity superHero);
}

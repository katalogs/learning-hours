package archunit.layered.domain;

import archunit.layered.infrastructure.SuperHeroEntity;

import java.util.concurrent.Future;

public interface SuperHeroRepository {
    Future<SuperHero> save(SuperHero superHero);
}

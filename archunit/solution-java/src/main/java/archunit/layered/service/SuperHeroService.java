package archunit.layered.service;

import archunit.layered.domain.SuperHeroRepository;

public class SuperHeroService {
    private final SuperHeroRepository superHeroRepository;

    public SuperHeroService(SuperHeroRepository superHeroRepository) {
        this.superHeroRepository = superHeroRepository;
    }

    public SuperHeroRepository getSuperHeroRepository() {
        return superHeroRepository;
    }
}
